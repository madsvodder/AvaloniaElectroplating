using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.ViewModels;

namespace AvaloniaElectroplating.Factories;

public class PageFactory
{
    
    // Here we create the singletons.
    // The switch statement creates the transients / always new pages.
    
    // Singletons
    private CalculatePageViewModel _calculateVm;

    public PageFactory()
    {
        // Initialize singletons
        _calculateVm = new CalculatePageViewModel();
    }

    public PageViewModel CreatePage(ApplicationPageNames names)
    {
        switch (names)
        {
            case ApplicationPageNames.Calculate:
                return _calculateVm;
            
            case ApplicationPageNames.Settings:
                return new SettingsPageViewModel();
            
            default:
                Console.WriteLine("No page inserted in factory method?");
                return null;
        }
    }
}