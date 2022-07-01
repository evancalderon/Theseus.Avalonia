using System;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Diagnostics;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using Avalonia.Utilities;
using ReactiveUI;
using Theseus.Avalonia.ViewModels;

namespace Theseus.Avalonia.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        private double _searchBorderHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(_ =>
            {
            });

            Opened += delegate
            {
                _searchBorderHeight = SearchBorder.Bounds.Height;

                SearchBox.PropertyChanged += SearchBoxOnPropertyChanged;

                //((ExperimentalAcrylicBorder)SearchBox.GetLogicalParent()).Height = _searchBorderHeight - 8;
                if (Design.IsDesignMode)
                    return;
                SearchBorder.Height = 0;
            };

            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                await ViewModel!.LoadOnline();
            });

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void SearchBoxOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == "Text")
            {
                ViewModel?.DoSearch(SearchBox.Text);
            }
        }

        private void ToggleSearchBox(object? sender, PointerPressedEventArgs e)
        {
            SearchBorder.SetValue(HeightProperty,
                SearchBorder.GetValue(HeightProperty) == 0
                    ? _searchBorderHeight
                    : 0);
        }

        private void ShowRealSearch(object? sender, GotFocusEventArgs e)
        {
            SearchBox.Focus();
        }
    }
}