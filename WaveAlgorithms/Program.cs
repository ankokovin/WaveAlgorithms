using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveAlgorithms
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
        private const int ATTACH_PARENT_PROCESS = -1;

        public static List<Process> ChildProcesses;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                bool result = AllocConsole();                
                string my_pipe = args[0];
                string[] neighbours = args.Skip(1).ToArray();
                Console.WriteLine("My pipe:" + my_pipe);
                Console.WriteLine("My neigbours:"
                    +neighbours.Aggregate((x, y)=>x+","+y)
                );
                Console.ReadLine();
            }
            else
            {
                ChildProcesses = new List<Process>();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }


        public static void StartChild(string arg)
        {
            var res = System.Diagnostics.Process.Start("WaveAlgorithms.exe", arg);
            ChildProcesses.Add(res);
        }
    }
}
