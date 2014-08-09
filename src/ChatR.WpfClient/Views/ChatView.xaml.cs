﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatR.WpfClient.ViewModels;

namespace ChatR.WpfClient.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    [Export("ChatView")]
    public partial class ChatView : UserControl
    {
        public ChatView(ChatViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
