using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Factories;
using AvaloniaElectroplating.ViewModels;
using AvaloniaElectroplating.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaElectroplating;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            
            // Page Factory
            PageFactory pageFactory = new();
            
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainView
            {
                DataContext = new MainViewModel(pageFactory)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}