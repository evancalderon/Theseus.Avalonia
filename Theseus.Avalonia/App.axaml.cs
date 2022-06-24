using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Theseus.Avalonia.ViewModels;
using Theseus.Avalonia.Views;

namespace Theseus.Avalonia
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
            if (!OperatingSystem.IsLinux()) return;
            foreach (var resource in Resources)
            {
                if (resource.Value is not ExperimentalAcrylicMaterial mat) continue;
                
                mat.TintColor = mat.FallbackColor;
                mat.MaterialOpacity = 1;
                mat.TintOpacity = 1;
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}