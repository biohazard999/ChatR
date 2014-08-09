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
            NavigateTo = new DelegateCommand(() => _rm.RequestNavigate(RegionNames.MainRegion, new Uri("LoginView", UriKind.Relative),
                (NavigationResult nr) =>
    {
        var error = nr.Error;
        var result = nr.Result;
        // put a breakpoint here and checkout what NavigationResult contains
    }));
        }

        public DelegateCommand NavigateTo { get; set; }
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

