using System.ComponentModel.Composition;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ChatR.WinClient.Views
{

    [Export(typeof(IShell))]
    public partial class Shell : XtraForm, IShell
    {
        public Shell()
        {
            InitializeComponent();
        }

        public void SetContent(Control control)
        {
            Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.Parent = this;
        }
    }
}