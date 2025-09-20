using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.ViewModels;

namespace AvaloniaElectroplating.Factories;

public class PageFactory
{
    
    // Here we create the singletons.
    // The switch statement creates the transients / always new pages.
    private CalculatePageViewModel _calculateVm = new();

    public PageViewModel CreatePage(ApplicationPageNames names)
    {
        switch (names)
        {
            case ApplicationPageNames.Calculate:
                return _calculateVm;
            
            case ApplicationPageNames.Settings:
                return new SettingsPageViewModel();
            
            case ApplicationPageNames.About:
                return new AboutPageViewModel();
            
            default:
                Console.WriteLine("No page inserted in factory method?");
                return null;
        }
    }
}