using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ChatR.WinClient.Views
{

    [Export(typeof(IShell))]
    public partial class Shell : DevExpress.XtraEditors.XtraForm, IShell
    {
        public Shell()
        {
            InitializeComponent();
        }

        public void SetContent(Control control)
        {
            this.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.Parent = this;
        }
    }
}