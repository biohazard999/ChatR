using System.ComponentModel.Composition;
using ChatR.WpfClient.ViewModels;

namespace ChatR.WpfClient.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    [Export("LoginView")]
    public partial class LoginView 
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
