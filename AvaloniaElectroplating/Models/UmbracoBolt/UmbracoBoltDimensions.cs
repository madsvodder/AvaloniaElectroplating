namespace AvaloniaElectroplating.Models;

public class UmbracoBoltDimensions
{
    public double Diameter { get; }
    public double HeadDiameter { get; }
    public double HeadHeight { get; }
    public double HexSocketSize { get; }

    public UmbracoBoltDimensions(double diameter, double headDiameter, double headHeight, double hexSocketSize)
    {
        Diameter = diameter;
        HeadDiameter = headDiameter;
        HeadHeight = headHeight;
        HexSocketSize = hexSocketSize;
    }
}