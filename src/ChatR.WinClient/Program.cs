using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading;
using System.Windows.Forms;
using ChatR.SignalRClient;
using ChatR.WinClient.Presenters;
using ChatR.WinClient.Services;
using ChatR.WinClient.Utils;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Practices.ServiceLocation;

namespace ChatR.WinClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            var container = ConfigureContainer();

            var mainFormPresenter = container.GetExportedValue<ShellPresenter>();

            var navigationService = container.GetExportedValue<INavigationService>();

            navigationService.NavigateTo("Login");

            mainFormPresenter.Run();
        }

        private static CompositionContainer ConfigureContainer()
        {
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof (Program).Assembly));
            

            var container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);


            if(SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());

            var hubProxy = new ChatHubProxy(new HubConnection("http://localhost:5000"), SynchronizationContext.Current);

            container.ComposeExportedValue<IChatHubProxy>(hubProxy);


            var adapter = new MefServiceLocatorAdapter(container);

            container.ComposeExportedValue<IServiceLocator>(adapter);

            ServiceLocator.SetLocatorProvider(() => adapter);

            return container;
        }
    }
}
