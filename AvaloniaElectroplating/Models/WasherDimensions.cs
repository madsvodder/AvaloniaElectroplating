namespace AvaloniaElectroplating.Models;

public class WasherDimensions
{
    public readonly double InternalDiameter;
    public readonly double ExternalDiameter;
    public readonly double Thickness;

    public WasherDimensions(double internalDiameter, double externalDiameter, double thickness)
    {
        InternalDiameter = internalDiameter;
        ExternalDiameter = externalDiameter;
        Thickness = thickness;
    }
}