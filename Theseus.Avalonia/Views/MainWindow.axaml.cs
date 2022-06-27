using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.ReactiveUI;
using Avalonia.Utilities;
using ReactiveUI;
using Theseus.Avalonia.ViewModels;

namespace Theseus.Avalonia.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(disposable => { });

#if DEBUG
            this.AttachDevTools();
#endif
        }
    }
}