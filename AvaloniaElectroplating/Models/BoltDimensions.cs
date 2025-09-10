namespace AvaloniaElectroplating.Models;

public class BoltDimensions
{
    public double Diameter { get;  }
    public double WrenchSize { get; }
    public double HeadHeight { get; }

    public BoltDimensions(double diameter, double wrenchSize, double headHeight)
    {
        Diameter = diameter;
        WrenchSize = wrenchSize;
        HeadHeight = headHeight;
    }
}