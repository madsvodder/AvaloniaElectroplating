using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Factories;
using AvaloniaElectroplating.Services;
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

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {

            // Dependency injection
            ServiceCollection collection = new();
            collection.AddSingleton<PageFactory>();
            collection.AddSingleton<UserSettingsService>();

            collection.AddSingleton<MainViewModel>();

            collection.AddSingleton<CalculatePageViewModel>();
            collection.AddTransient<SettingsPageViewModel>();
            collection.AddTransient<AboutPageViewModel>();

            var services = collection.BuildServiceProvider();

            var splashScreenVm = new SplashScreenViewModel();
            var splashScreen = new SplashScreenPageView
            {
                DataContext = splashScreenVm
            };
            desktop.MainWindow = splashScreen;
            splashScreen.Show();

            try
            {
                splashScreenVm.StartupMessage = "Starting application...";
                await Task.Delay(TimeSpan.FromSeconds(1), splashScreenVm.Token);

                splashScreenVm.StartupMessage = "Loading user settings...";
                await services.GetRequiredService<UserSettingsService>().LoadSettingsFromJson();

                splashScreenVm.StartupMessage = "Settings loaded successfully";
            }
            catch (TaskCanceledException)
            {
                splashScreen.Close();
                return;
            }
            

            
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            var mainWindow = new MainView
            {
                DataContext = services.GetRequiredService<MainViewModel>()
            };

            desktop.MainWindow = mainWindow;
            mainWindow.Show();
            splashScreen.Close();
        }

        base.OnFrameworkInitializationCompleted();
    }
}