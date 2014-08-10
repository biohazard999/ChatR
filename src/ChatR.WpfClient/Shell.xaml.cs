using System.ComponentModel.Composition;
using DevExpress.Xpf.Core;

namespace ChatR.WpfClient
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : DXWindow
    {
        [ImportingConstructor]
        public Shell(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
