using Avalonia.ReactiveUI;
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
        }
    }
}