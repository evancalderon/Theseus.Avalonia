using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace Theseus.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            void opened(object? sender, EventArgs e)
            {
                TabClick(this.FindControl<Border>("HomeTab"), null!);
                Opened -= opened;
            };
            Opened += opened;
            InitializeComponent();
        }

        private void TabClick(object? sender, PointerPressedEventArgs e)
        {
            if (sender is not Border tab) return;

            var point = tab.TranslatePoint(
                new Point(tab.Bounds.Width / 2 - 8, 0),
                this.FindControl<Canvas>("Canvas"));
            Canvas.SetLeft(this.FindControl<Border>("TabPointer"), point?.X ?? 0);
        }
    }
}