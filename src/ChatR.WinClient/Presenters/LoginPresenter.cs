using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using ChatR.SignalRClient;
using ChatR.WinClient.Contracts;
using ChatR.WinClient.Services;
using ChatR.WinClient.Views;

namespace ChatR.WinClient.Presenters
{
    [Export("Login", typeof(IPresenter))]
    public class LoginPresenter : IPresenter
    {
        private readonly ILoginView _view;
        private readonly IChatHubProxy _proxy;
        private readonly INavigationService _navigationService;
        private bool _isLoggingIn;

        [ImportingConstructor]
        public LoginPresenter(ILoginView view, IChatHubProxy proxy, INavigationService navigationService)
        {
            _view = view;
            _proxy = proxy;
            _navigationService = navigationService;
            _view.PropertyChanged += (s, e) => _view.SetLoginEnabled(proxy.IsConnected && !_isLoggingIn && !String.IsNullOrWhiteSpace(_view.UserName));

            _view.Login += _view_Login;


            _proxy.Connected = (detail, details) =>
            {

            };

            Action connect = async () =>
            {
                _isLoggingIn = true;
                try
                {
                    await proxy.Connect();
                }
                finally
                {
                    _isLoggingIn = false;
                }
            };

            connect.Invoke();

        }

        private async void _view_Login(object sender, EventArgs e)
        {
            await _proxy.Login(_view.UserName);

            _navigationService.NavigateTo("Chat");
        }

        public Control GetControl()
        {
            return (Control)_view;
        }
    }
}