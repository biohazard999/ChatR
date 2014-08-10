using System;
using ChatR.WpfClient.Contracts;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace ChatR.WpfClient.Modules
{
    [ModuleExport(typeof(ChatModule))]
    public class ChatModule : IModule
    {
        private readonly IRegionManager _manager;

        public ChatModule(IRegionManager manager)
        {
            _manager = manager;
        }

        public void Initialize()
        {
            _manager.RequestNavigate(RegionNames.MainRegion, new Uri("LoginView", UriKind.Relative));
        }
    }
}