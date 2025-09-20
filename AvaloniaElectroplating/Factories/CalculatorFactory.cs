using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;
using AvaloniaElectroplating.Services;

namespace AvaloniaElectroplating.Factories;

public class CalculatorFactory
{
    public ICalculator CreateCalculator (FastenerType type)
    {
        switch (type)
        {
            case FastenerType.Bolt:
                return new BoltCalculator();
            case FastenerType.UmbracoBolt:
                return new UmbracoBoltCalculator();
            case FastenerType.Nut:
                return new NutCalculator();
            case FastenerType.Washer:
                return new WasherCalculator();
            default:
                return null;
        }
    }
}