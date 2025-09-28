using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class PlateModel : ICalculable
{
    // Surface area in Mm2
    public double SurfaceArea { get; set; }
    public ModelType Type { get; set; }

    public PlateModel(double surfaceArea)
    {
        SurfaceArea = surfaceArea;
        Type = ModelType.Custom;
    }

    public override string ToString()
    {
        return $"Custom model - {SurfaceArea}mm2";
    }
}