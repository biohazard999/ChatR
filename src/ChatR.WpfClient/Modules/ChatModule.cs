using System.ComponentModel.Composition;
using ChatR.WpfClient.Contracts;
using ChatR.WpfClient.Views;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace ChatR.WpfClient.Modules
{
    [ModuleExport(typeof(ChatModule))]
    public class ChatModule : IModule
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IRegionManager _manager;

        public ChatModule(IServiceLocator serviceLocator, IRegionManager manager)
        {
            _serviceLocator = serviceLocator;
            _manager = manager;
        }

        public void Initialize()
        {
            _manager.RegisterViewWithRegion(RegionNames.MainRegion, () => _serviceLocator.GetInstance<LoginView>());
        }
    }
}