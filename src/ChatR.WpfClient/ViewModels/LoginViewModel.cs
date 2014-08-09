using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatR.SignalRClient;
using ChatR.WpfClient.Contracts;

using Microsoft.Practices.Prism.Regions;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace ChatR.WpfClient.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IChatHubProxy _proxy;
        private readonly IRegionManager _rm;
        private string _username;
        private bool _isConnected;
        private bool _isLoggingIn;

        public string Username
        {
            get { return _username; }
            set
            {
                if(SetProperty(ref _username, value))
                    Login.RaiseCanExecuteChanged();
            }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                if(SetProperty(ref _isConnected, value))
                    Login.RaiseCanExecuteChanged();
            }
        }

        public bool IsLoggingIn
        {
            get { return _isLoggingIn; }
            set
            {
                if(SetProperty(ref _isLoggingIn, value))
                    Login.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand Login { get; set; }


        public LoginViewModel()
        {

        }

        [ImportingConstructor]
        public LoginViewModel(IChatHubProxy proxy, IRegionManager rm)
        {
            _proxy = proxy;
            _rm = rm;

            Action action = async () => await Connect();

            action.Invoke();

            Login = new DelegateCommand(async () =>
            {
                IsLoggingIn = true;
                try
                {
                    await proxy.Login(Username);
                }
                finally
                {
                    IsLoggingIn = false;
                }

            }, () => IsConnected && !IsLoggingIn && !String.IsNullOrWhiteSpace(Username));
            
            proxy.Connected = (detail, details) => rm.RequestNavigate(RegionNames.MainRegion, "ChatView");
        }

        private async Task Connect()
        {
            if (!_proxy.IsConnected)
            {
                IsConnected = await _proxy.Connect();
            }
            else
                IsConnected = _proxy.IsConnected;
        }
    }
}