using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            if (App.PriorProcess() != null)
            {
                int num = (int)MessageBox.Show("Another instance of the app is already running.");
            }
            else
            {
                //Application.Run((Window)new MainWindow());
                MainWindow main = new MainWindow();
                main.Show();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        public static Process PriorProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (process.Id != currentProcess.Id && process.MainModule.FileName == currentProcess.MainModule.FileName)
                    return process;
            }
            return (Process)null;
        }
    }
}
