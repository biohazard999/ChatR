using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using ChatR.SignalRClient;
using ChatR.WpfClient.Contracts;

using Microsoft.Practices.Prism.Regions;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace ChatR.WpfClient.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        private readonly IChatHubProxy _proxy;
        private string _username;
        private bool _isConnected;
        private bool _isLoggingIn;
        private string _errorMessage;
        private bool _isConnecting;

        public string Username
        {
            get { return _username; }
            set
            {
                if (SetProperty(ref _username, value))
                    Login.RaiseCanExecuteChanged();
            }
        }

        private bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                if (SetProperty(ref _isConnected, value))
                {
                    if (_isConnected)
                        ErrorMessage = null;
                    Login.RaiseCanExecuteChanged();
                    Reconnect.RaiseCanExecuteChanged();
                    OnPropertyChanged(() => IsSpinnerVisible);
                }
            }
        }

        private bool IsLoggingIn
        {
            get { return _isLoggingIn; }
            set
            {
                if (SetProperty(ref _isLoggingIn, value))
                {
                    Reconnect.RaiseCanExecuteChanged();
                    Login.RaiseCanExecuteChanged();
                    OnPropertyChanged(() => IsSpinnerVisible);
                    OnPropertyChanged(() => IsErrorVisible);
                }
            }
        }
        private bool IsConnecting
        {
            get { return _isConnecting; }
            set
            {
                if (SetProperty(ref _isConnecting, value))
                {
                    Reconnect.RaiseCanExecuteChanged();

                    OnPropertyChanged(() => IsSpinnerVisible);
                    OnPropertyChanged(() => IsErrorVisible);
                }
            }
        }
        
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (SetProperty(ref _errorMessage, value))
                {
                    OnPropertyChanged(() => IsErrorVisible);
                    Reconnect.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand Login { get; private set; }

        public bool IsErrorVisible
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ErrorMessage))
                {
                    if (!IsConnecting || !IsLoggingIn)
                        return true;
                    return false;
                }
                return false;
            }
        }

        public bool IsSpinnerVisible
        {
            get { return IsLoggingIn || IsConnecting; }
        }

        public DelegateCommand Reconnect { get; private set; }


        public LoginViewModel()
        {

        }

        [ImportingConstructor]
        public LoginViewModel(IChatHubProxy proxy, IRegionManager rm)
        {
            _proxy = proxy;

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

            proxy.Connected = (detail, details) => rm.RequestNavigate(RegionNames.MainRegion, new Uri("ChatView", UriKind.Relative));


            Reconnect = new DelegateCommand(async () => await Connect(), () => !IsConnected && !IsConnecting);

            Reconnect.Execute();
        }

        private async Task Connect()
        {
            if (!_proxy.IsConnected)
            {
                try
                {
                    ErrorMessage = null;
                    IsConnecting = true;
                    IsConnected = await _proxy.Connect();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    IsConnecting = false;
                }
            }
            else
                IsConnected = _proxy.IsConnected;
        }


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