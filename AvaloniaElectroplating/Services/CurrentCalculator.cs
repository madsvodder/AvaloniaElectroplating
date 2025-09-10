using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;

namespace AvaloniaElectroplating;

public class CurrentCalculator
{
    public double CalculateCurrent(double areaMm2)
    {
        // Convert to inches
        double areaIn2 = areaMm2 / 645.16;

        // Gateros kit: 0.1 A per square inch
        double current = Math.Round(areaIn2 * 0.1, 3);

        return current;
    }
}