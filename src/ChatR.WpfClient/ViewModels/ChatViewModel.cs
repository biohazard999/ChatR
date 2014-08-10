using System;
using System.ComponentModel.Composition;
using ChatR.WpfClient.Contracts;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace ChatR.WpfClient.ViewModels
{
    public class ChatViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _rm;

        public ChatViewModel()
        {
            
        }

        [ImportingConstructor]
        public ChatViewModel(IRegionManager rm)
        {
            _rm = rm;
            NavigateTo = new DelegateCommand(() => _rm.RequestNavigate(RegionNames.MainRegion, new Uri("LoginView", UriKind.Relative)));
        }

        public DelegateCommand NavigateTo { get; private set; }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}

