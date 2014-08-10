using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Threading;
using System.Windows;
using ChatR.SignalRClient;
using Microsoft.AspNet.SignalR.Client;
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
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
        protected override CompositionContainer CreateContainer()
        {
            return new CompositionContainer(AggregateCatalog, CompositionOptions.DisableSilentRejection, new ExportProvider[0]);
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            var registration = new RegistrationBuilder();
            
            registration.ForTypesMatching(t => t.Name.EndsWith("ViewModel"))
                .SelectConstructor(c => c.OrderBy(m => m.GetParameters().Count()).FirstOrDefault())
                .Export();

            registration.ForTypesMatching(t => t.Name.EndsWith("View"))
                .SelectConstructor(c => c.OrderBy(m => m.GetParameters().Count()).FirstOrDefault())
                .Export();

            registration.ForTypesMatching(t => t.Name.EndsWith("Repository"))
                .SelectConstructor(c => c.OrderBy(m => m.GetParameters().Count()).FirstOrDefault())
                .ExportInterfaces(i => i.IsPublic);

            registration.ForTypesMatching(t => t.Name.EndsWith("Module"))
                .SelectConstructor(c => c.OrderBy(m => m.GetParameters().Count()).FirstOrDefault())
                .ExportInterfaces(i => i.IsPublic);


            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly, registration));


            registration = new RegistrationBuilder();

            registration.ForTypesMatching(t => true)
                        .ExportInterfaces(i => i.IsPublic);


            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ChatHubProxy).Assembly, registration));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.ComposeExportedValue(new HubConnection("http://localhost:5000"));
            Container.ComposeExportedValue(SynchronizationContext.Current);
        }

        //public override void Run(bool runWithDefaultConfiguration)
        //{
        //    //SplashScreenHelper.Instance.ShowSplashScreen();
        //    base.Run(runWithDefaultConfiguration);
        //    //SplashScreenHelper.Instance.HideSplashScreen();


        //    //App.Current.MainWindow.Show();
        //    App.Current.MainWindow.Show();
        //    App.Current.MainWindow.Activate();
        //}

    }
}