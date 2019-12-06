using System;
using System.Collections.Generic;
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
        public static List<Process> ChildProcesses;
        public static List<string> Neigbours;
        public static int PipeHandle;
        public static string MyPipeName;
        public static void ChildMain(string[] args)
        {
            bool result = Imports.AllocConsole();
            MyPipeName = args[0];
            Console.Title = MyPipeName;
            Neigbours = args.Skip(1).ToList();
            Console.WriteLine("My pipe:" + MyPipeName);
            Console.WriteLine("My neigbours:"
                + Neigbours.Aggregate((x, y) => x + "," + y)
            );
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

        public static void MainLoop()
        {
            bool receviedFirst = false;
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
                    //TODO: spawn thread with algorithm?
                    Imports.DisconnectNamedPipe(PipeHandle);                             // отключаемся от канала клиента 
                }
                Thread.Sleep(500);                                                      // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
            }
        }


        static void Send(string pipename, string msg)
        {
            uint BytesWritten = 0;
            byte[] buff = Encoding.Unicode.GetBytes(msg);    
            var PipeHandleO = Imports.CreateFile(pipename, Types.EFileAccess.GenericWrite, Types.EFileShare.Read, 0, Types.ECreationDisposition.OpenExisting, 0, 0);
            Imports.WriteFile(PipeHandleO, buff, Convert.ToUInt32(buff.Length), ref BytesWritten, 0);         // выполняем запись последовательности байт в канал
            Imports.CloseHandle(PipeHandleO);                                                                 // закрываем дескриптор канала
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
