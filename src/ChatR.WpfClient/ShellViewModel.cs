using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using ChatR.WpfClient.Contracts;
using ChatR.WpfClient.ViewModels;
using Microsoft.Practices.Prism.Commands;
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