using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveAlgorithms
{
    static class Program
    {
        public static double[] realPriorities;
        public static Random rnd;
        public static List<Process> ChildProcesses;
        public static List<string> Neigbours;
        public static int PipeHandle;
        public static string MyPipeName;
        public static decimal Productivity;
        static decimal[] WaveReturns;
        static public double[] priorities;
        public static bool IsInitiator;
        public static int rec;
        public static decimal sum;
        public static string Parent;
        public static void ChildMain(string[] args)
        {
            rec = 0;
            bool result = Imports.AllocConsole();
            MyPipeName = args[0];
            Console.Title = MyPipeName;
            Productivity = decimal.Parse(args[1]);
            Neigbours = args.Skip(2).ToList();
            Console.WriteLine("My pipe:" + MyPipeName);
            Console.WriteLine("My neigbours:"
                + Neigbours.Aggregate((x, y) => x + "," + y)
            );
            realPriorities = new double[Neigbours.Count];
            priorities = new double[Neigbours.Count];
            for (int i = 0; i < Neigbours.Count; i++)
            {
                priorities[i] = 1.0 / Neigbours.Count;
                realPriorities[i] = 1.0 / Neigbours.Count;
            }
            rnd = new Random(MyPipeName.GetHashCode());
            CreatePipe();
            MainLoop();
        }

        private static void  CreatePipe()
        {
            PipeHandle = Imports.CreateNamedPipe("\\\\.\\pipe\\" + MyPipeName, Types.PIPE_ACCESS_DUPLEX, Types.PIPE_TYPE_BYTE | Types.PIPE_WAIT, Types.PIPE_UNLIMITED_INSTANCES, 0, 1024, Types.NMPWAIT_WAIT_FOREVER, (uint)0);
            if (PipeHandle != -1)
            {
                Console.WriteLine("Named pipe opened successfully");
            }
            else
            {
                Console.WriteLine("There was an error opening named pipe");
            }
        }
        public static bool receviedFirst = false;
        public static void MainLoop()
        {
          
            string reseviedMessage;
            uint realBytesReaded = 0;
            while (true)
            {
               
                if (Imports.ConnectNamedPipe(PipeHandle, 0))
                {
                    byte[] buff = new byte[1024];                                           // буфер прочитанных из канала байтов
                    Imports.FlushFileBuffers(PipeHandle);                                // "принудительная" запись данных, расположенные в буфере операционной системы, в файл именованного канала
                    Imports.ReadFile(PipeHandle, buff, 1024, ref realBytesReaded, 0);    // считываем последовательность байтов из канала в буфер buff
                    reseviedMessage = Encoding.Unicode.GetString(buff);
                    Console.WriteLine(reseviedMessage);
                    if (reseviedMessage.StartsWith("set"))
                    {
                        Productivity = decimal.Parse(reseviedMessage.Substring(4));
                    }
                    if (reseviedMessage.StartsWith("start"))
                    {
                        EchoInitiator();
                    }
                    if (reseviedMessage.StartsWith("tok"))
                    {
                        ReceiveTok(reseviedMessage.Substring(4));
                        
                    }
                    bool res = Imports.DisconnectNamedPipe(PipeHandle);// отключаемся от канала клиента 
                    if (!res) Console.WriteLine("COULD NOT DISCONNECT PIPE");
                }
                Thread.Sleep(20);                                                      // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
            }
        }

        public static void EchoInitiator()
        {
            Console.WriteLine("Initiator");
            WaveReturns = new decimal[Neigbours.Count];
            rec = 0;
            IsInitiator = true;
            foreach (var n in Neigbours)
            {
                Task.Run(()=> Send(n, "tok "+MyPipeName));
            }
        }

        public static void EchoNonInitiator(string parent)
        {
            
            receviedFirst = true;
            Console.WriteLine("Non initiator");
            Parent = parent.Replace("\0",string.Empty);
            Console.WriteLine("Parent:" + Parent);
            sum = Productivity;
            rec = 1;
            foreach(var n in Neigbours)
            {
                if (!Parent.StartsWith(n))
                    Task.Run(() => Send(n, "tok " + MyPipeName));
            }
        }

        public static void ReceiveTok(string str)
        {
            if (rec == Neigbours.Count) return;
            if (IsInitiator)
            {
                ReceiveTokInit(str);
            }
            else if (!receviedFirst)
            {
                EchoNonInitiator(str);
            }
            else
            {
                Console.WriteLine("Received tok:");
                Console.WriteLine(str);
                var sp = str.Split(' ');
                string parent = sp[0].Replace("\0", string.Empty);
                rec++;
                if (sp.Length < 2)
                {
                    Task.Run(() => Send(parent, "tok " + MyPipeName + " " + Productivity));
                }
                else
                {
                    decimal val = decimal.Parse(sp[1]);
                    sum += val;
                  
                }
            }
            if (rec == Neigbours.Count)
            {

                Task.Run(() => Send(Parent, "tok " + MyPipeName + " " + sum));
                Task.Run(() => Revert());
            }


        }

        public static void ReceiveTokInit(string str)
        {
            var sp = str.Split(' ');
            if (sp.Length < 2)
            {

            }
            else
            {
                decimal val = decimal.Parse(sp[1]);
                string from = sp[0];
                sum += val;
                for (int i = 0; i < Neigbours.Count; i++)
                {
                    if (Neigbours[i] == from) WaveReturns[i] = val;
                }                rec++;
                if (rec == Neigbours.Count)
                    decide();
            }
        }
        public static double eps = 1e-1;
        public static void decide()
        {
            Console.WriteLine("Decide");
            for (int i = 0; i < Neigbours.Count; i++)
            {
                priorities[i] = (double)WaveReturns[i] / (double)sum;
                Console.WriteLine("{0} - {1}", Neigbours[i], priorities[i]);
            }
            for (int i = 0; i < Neigbours.Count; i++)
            {
                if (Math.Abs(priorities[i] - realPriorities[i]) > eps)
                {
                    Console.WriteLine("Rebalance");
                    for (int j = 0; j < Neigbours.Count; j++)
                    {
                        realPriorities[j] = realPriorities[j];
                    }
                    break;
                }
            }
            Revert();
        }

        public static void Revert()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Reverted");
            IsInitiator = false;
            sum = 0;
            receviedFirst = false;
            rec = 0;
            Parent = null;

        }

        public static void Send(string pipename, string msg)
        {
            pipename = pipename.Replace("\0", "");
            Console.WriteLine("Sending to \"" + pipename + "\" message:\"" + msg + "\"");
            uint BytesWritten = 0;
            byte[] buff = Encoding.Unicode.GetBytes(msg);
            var PipeHandleO = Imports.CreateFile("\\\\.\\pipe\\" + pipename, Types.EFileAccess.GenericWrite, Types.EFileShare.Read, 0, Types.ECreationDisposition.OpenExisting, 0, 0);
            while (PipeHandleO == -1)
            {
                Console.WriteLine(new Win32Exception(Marshal.GetLastWin32Error()).Message);
                Thread.Sleep(rnd.Next(0,1000));
                PipeHandleO  = Imports.CreateFile("\\\\.\\pipe\\" + pipename, Types.EFileAccess.GenericWrite, Types.EFileShare.Read, 0, Types.ECreationDisposition.OpenExisting, 0, 0);
                
            }
            bool res = Imports.WriteFile(PipeHandleO, buff, Convert.ToUInt32(buff.Length), ref BytesWritten, 0);         // выполняем запись последовательности байт в канал
            
            if (!res)
            {
                Console.WriteLine("COULD NOT WRITE IN PIPE");
            }

            res = Imports.CloseHandle(PipeHandleO);                                                                 // закрываем дескриптор канала

            if (!res)
            {
                Console.WriteLine("COULD NOT CLOSE PIPE HANDLE");
            }

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                ChildMain(args);
            }
            else
            {
                MyPipeName = "ControlServer";
                CreatePipe();
                ChildProcesses = new List<Process>();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, e) =>
                {
                    foreach (var process in ChildProcesses)
                    {
                        if (!process.HasExited)
                            process.Kill();
                    }
                });
            }
        }


        public static void StartChild(string arg)
        {
            var res = System.Diagnostics.Process.Start("WaveAlgorithms.exe", arg);

            ChildProcesses.Add(res);
        }
    }
}
