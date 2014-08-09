using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using ChatR.WinClient.Contracts;
using ChatR.WinClient.Services;
using ChatR.WinClient.Views;

namespace ChatR.WinClient.Presenters
{
    [Export]
    [Export(typeof(INavigationAware))]
    public class ShellPresenter : IPresenter, INavigationAware
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public ShellPresenter(IShell shell)
        {
            _shell = shell;
        }

        public void Run()
        {
            Application.Run((Form)_shell);
        }

        public Control GetControl()
        {
            return (Control)_shell;
        }

        public void DoNavigate(Control controlToNavigateTo)
        {
            if (controlToNavigateTo == null) throw new ArgumentNullException("controlToNavigateTo");

            _shell.SetContent(controlToNavigateTo);
        }
    }
}