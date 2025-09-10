namespace AvaloniaElectroplating.Models;

public class WasherDimensions
{
    public double InternalDiameter;
    public double ExternalDiameter;
    public double Thickness;

    public WasherDimensions(double internalDiameter, double externalDiameter, double thickness)
    {
        InternalDiameter = internalDiameter;
        ExternalDiameter = externalDiameter;
        Thickness = thickness;
    }
}