using System.ComponentModel.Composition;
using System.Windows.Forms;
using ChatR.WinClient.Contracts;
using ChatR.WinClient.Services;
using ChatR.WinClient.Views;

namespace ChatR.WinClient.Presenters
{
    [Export("Chat", typeof(IPresenter))]
    public class ChatPresenter : IPresenter
    {
        private readonly IChatView _view;

        [ImportingConstructor]
        public ChatPresenter(IChatView view, INavigationService navigationService)
        {
            _view = view;

            _view.Login += (s, e) => navigationService.NavigateTo("Login");
        }

        public Control GetControl()
        {
            return (Control) _view;
        }
    }
}
