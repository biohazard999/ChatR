using System.ComponentModel.Composition;
using ChatR.WinClient.Contracts;
using Microsoft.Practices.ServiceLocation;

namespace ChatR.WinClient.Services
{
    [Export(typeof(INavigationService))]
    public class NavigationService : INavigationService
    {
        private readonly INavigationAware _navigationAware;
        private readonly IServiceLocator _serviceLocator;

        [ImportingConstructor]

        public NavigationService(INavigationAware navigationAware, IServiceLocator serviceLocator)
        {
            _navigationAware = navigationAware;
            _serviceLocator = serviceLocator;
        }

        public void NavigateTo(IPresenter presenter)
        {
            _navigationAware.DoNavigate(presenter.GetControl());
        }

        public void NavigateTo<T>() where T : IPresenter
        {
            NavigateTo(_serviceLocator.GetInstance<T>());
        }

        public void NavigateTo(string name)
        {
            NavigateTo(_serviceLocator.GetInstance<IPresenter>(name));
        }
    }
}