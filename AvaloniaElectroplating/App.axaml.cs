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
        try
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                // Dependency injection
                ServiceCollection collection = new();
                collection.AddSingleton<MainViewModel>();
                collection.AddSingleton<PageFactory>();
                collection.AddSingleton<CalculatePageViewModel>();
                collection.AddTransient<AboutPageViewModel>();
                collection.AddTransient<SettingsPageViewModel>();
                collection.AddSingleton<UserSettingsService>();

                // "Widgets" in calculator page - Dependency injection
                collection.AddSingleton<TimerPageViewModel>();
                collection.AddSingleton<CustomCalcViewModel>();
                collection.AddSingleton<ConverterPageViewModel>();

                // Current calculator
                collection.AddSingleton<CurrentCalculator>();

                // Update Service
                collection.AddSingleton<UpdateService>();

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

                    splashScreenVm.StartupMessage = "Checking for updates...";
                    await services.GetRequiredService<UpdateService>().CheckForUpdates();

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
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }
}