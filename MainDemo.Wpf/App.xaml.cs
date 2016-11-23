using MaterialDesignDemo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Semaphore sema;
        bool shouldRelease = false;


        public App()
        {
            Startup += App_Startup;
            Exit += Current_Exit;
        }
        private void App_Startup(object sender, StartupEventArgs e)
        {
            bool result = Semaphore.TryOpenExisting("Messenger", out sema);

            if (result) // we have another instance running
            {
                App.Current.Shutdown();
            }
            else
            {
                try
                {   // Single Instance program per user
                    sema = new Semaphore(1, 1, "Messenger");

                    LoginWindow.loginForm.Show();
                }
                catch
                {
                    App.Current.Shutdown();
                }
            }

            if (!sema.WaitOne(0))
            {
                App.Current.Shutdown();

            }
            else
            {
                shouldRelease = true;
            }
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            if (sema != null && shouldRelease)
            {
           
                sema.Release();
            }
        }
    }
}
