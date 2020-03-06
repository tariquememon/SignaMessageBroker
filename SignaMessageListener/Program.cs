using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
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
            var c = new IpcClient();
            c.Initialize(12345);

            c.Send(arguments);            
        }

        static void InitializeProcess()
        {
            Process[] p;
            p = Process.GetProcessesByName("SignaShell");

            if (p.Count() == 0)
            {
                using (var newProcess = new Process())
                {
                    newProcess.StartInfo.FileName = "C:\\Development\\Training\\SignaShell\\SignaShell\\bin\\Debug\\SignaShell.exe";
                    newProcess.StartInfo.CreateNoWindow = true;
                    newProcess.StartInfo.UseShellExecute = false;
                    newProcess.Start();
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
