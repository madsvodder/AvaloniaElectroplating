using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaElectroplating.Factories;

public class PageFactory
{

    private readonly IServiceProvider _services;
    
    // Here we create the singletons.
    // The switch statement creates the transients / always new pages.
    private CalculatePageViewModel _calculateVm;

    public PageFactory(IServiceProvider services)
    {
        _services = services;
        _calculateVm = services.GetRequiredService<CalculatePageViewModel>();
    }

    public PageViewModel CreatePage(ApplicationPageNames names)
    {
        return names switch
        {
            ApplicationPageNames.Calculate => _calculateVm,
            ApplicationPageNames.Settings => _services.GetRequiredService<SettingsPageViewModel>(),
            ApplicationPageNames.About => _services.GetRequiredService<AboutPageViewModel>(),
            _ => throw new InvalidOperationException($"No page registered for {names}")
        };
    }
}