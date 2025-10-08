using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;
using AvaloniaElectroplating.Services;

namespace AvaloniaElectroplating;

public class CurrentCalculator
{

    private readonly UserSettingsService _settingsService;

    public CurrentCalculator(UserSettingsService userSettingsService)
    {
        _settingsService = userSettingsService;
    }

    public double CalculateCurrent(double areaMm2)
    {
        try
        {
            // Convert to square inches
            double areaIn2 = areaMm2 / 645.16;

            // Base current (Gateros kit: 0.1 A per in²)
            double baseCurrent = areaIn2 * 0.1;

            // Apply user current density setting
            double currentDensity = _settingsService.Settings.CurrentDensity;
            double adjustedCurrent = baseCurrent * currentDensity;

            return Math.Round(adjustedCurrent, 3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }
}