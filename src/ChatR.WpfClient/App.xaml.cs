#if DEBUG
using System.Windows;
#else
using System;
using System.Diagnostics;
using System.Windows.Threading;
#endif

namespace ChatR.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            DoRun();
            

            if (!Dispatcher.HasShutdownStarted)
                ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //if (MessagingClient.IsConnected)
            //    MessagingClient.Disconnect();
            //TriggerService.CloseTriggers();
        }

#if (DEBUG)
        private static void DoRun()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
#else
        private static void DoRun()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            try
            {
                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }


        static void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null) return;
            //ExceptionPolicy.HandleException(ex, "Policy");
            MessageBox.Show(ex.ToString(), "UnhandledException");
            //ExceptionReporter.Show(ex);
            Environment.Exit(1);
        }
#endif
    }
}
