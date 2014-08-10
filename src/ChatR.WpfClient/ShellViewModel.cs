using System.ComponentModel.Composition;
using ChatR.WpfClient.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace ChatR.WpfClient
{
    [Export]
    public class ShellViewModel : BindableBase

    {
        public ShellViewModel()
        {

        }

        [ImportingConstructor]
        public ShellViewModel(IRegionManager manager)
        {

        }
    }
}