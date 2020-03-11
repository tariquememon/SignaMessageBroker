using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using ZetaIpc.Runtime.Client;

namespace SignaMessageListener
{
    class Program
    {   
        static void Main(string[] args)
        {
            InitializeProcess();

            var arguments = GetArgsString(args);
            SendMessage(arguments);
        }

          private static void SendMessage(string arguments)
        {
            if (string.IsNullOrEmpty(arguments)) return;

            var c = new IpcClient();
            c.Initialize(12345);

            c.Send(arguments);
        }

        static void InitializeProcess()
        {
            Process[] p;
            p = Process.GetProcessesByName("SIGNA.WPF");

            if (p.Count() == 0)
            {
                using (var newProcess = new Process())
                {
                    newProcess.StartInfo.FileName = "C:\\Development\\CCWA\\Signa-UnifiedLab-OneLIMS Demo\\WPF\\bin\\Debug\\SIGNA.WPF.exe";
                    newProcess.StartInfo.CreateNoWindow = true;
                    newProcess.StartInfo.UseShellExecute = false;
                    newProcess.Start();
                    Thread.Sleep(10000);
                }
            }
        }

        static string GetArgsString(string[] args)
        {
            string arguments = String.Empty;

            foreach (var arg in args)
            {
                arguments += arg + " ";
            }

            return arguments.TrimEnd();
        }
    }
}
