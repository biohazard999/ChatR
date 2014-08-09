using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ChatR.WinClient.Views
{
    [Export(typeof(IChatView))]
    public partial class ChatView : DevExpress.XtraEditors.XtraUserControl, IChatView
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
