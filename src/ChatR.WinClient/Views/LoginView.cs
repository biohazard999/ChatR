using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ChatR.WinClient.Annotations;
using DevExpress.XtraEditors;

namespace ChatR.WinClient.Views
{
    [Export(typeof(ILoginView))]
    public partial class LoginView : DevExpress.XtraEditors.XtraUserControl, ILoginView, INotifyPropertyChanged
    {
        public LoginView()
        {
            InitializeComponent();
            SetLoginEnabled(false);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Login(this, EventArgs.Empty);
        }

 

        public void SetLoginEnabled(bool enabled)
        {
            loginButton.Enabled = enabled;
        }

        public string UserName { get { return (string)userNameTextEdit.EditValue; } }

        public event EventHandler<EventArgs> Login = (s, e) => { };

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void userNameTextEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            OnPropertyChanged("UserName");
        }

        private void userNameTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("UserName");
        }
    }

    public interface ILoginView : INotifyPropertyChanged
    {
        string UserName { get; }

        event EventHandler<EventArgs> Login;
        void SetLoginEnabled(bool enabled);
    }
}
