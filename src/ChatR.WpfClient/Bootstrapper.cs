using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;

namespace ChatR.WpfClient
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;
        }
        protected override CompositionContainer CreateContainer()
        {
            return new CompositionContainer(AggregateCatalog, CompositionOptions.DisableSilentRejection, new ExportProvider[0]);
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
        }
        public override void Run(bool runWithDefaultConfiguration)
        {
            //SplashScreenHelper.Instance.ShowSplashScreen();
            base.Run(runWithDefaultConfiguration);
            //SplashScreenHelper.Instance.HideSplashScreen();

            //App.Current.MainWindow.Show();
            //App.Current.MainWindow.Activate();
        }

    }
}