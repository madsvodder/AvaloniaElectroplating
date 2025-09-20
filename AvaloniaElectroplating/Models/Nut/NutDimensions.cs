namespace AvaloniaElectroplating.Models;

public class NutDimensions
{
    public double Diameter { get; } // thread diameter
    public double WrenchSize { get; } // flat to flat
    public double Thickness { get; } // Nut height

    public NutDimensions(double diameter, double wrenchSize, double thickness)
    {
        Diameter = diameter;
        WrenchSize = wrenchSize;
        Thickness = thickness;
    }
}