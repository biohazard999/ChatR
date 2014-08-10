using System.ComponentModel.Composition;
using ChatR.WpfClient.ViewModels;

namespace ChatR.WpfClient.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    [Export("ChatView")]
    public partial class ChatView
    {
        public ChatView(ChatViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
