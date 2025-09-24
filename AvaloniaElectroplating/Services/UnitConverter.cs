using System;
using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Services;

public static class UnitConverter
{

    public static double Convert(double value, Units fromUnit, Units toUnit)
    {
        if (fromUnit == toUnit)
            return value;

        return fromUnit switch
        {
            Units.mm => value / 25.4,  // mm -> inches
            Units.inches => value * 25.4, // inches -> mm
            _ => throw new ArgumentException("Unsupported unit", nameof(fromUnit))
        };
    }
}