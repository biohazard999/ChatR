using System;
using System.ComponentModel.Composition;
using DevExpress.XtraEditors;

namespace ChatR.WinClient.Views
{
    [Export(typeof(IChatView))]
    public partial class ChatView : XtraUserControl, IChatView
    {
        public ChatView()
        {
            InitializeComponent();
        }

        private void backToLoginButton_Click(object sender, EventArgs e)
        {
            Login(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Login =(s,e)=> {};
    }

    public interface IChatView
    {
        event EventHandler<EventArgs> Login;
    }
}
