using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public abstract class Fastener
{
    public FastenerType Type { get; set; }
    
    public FastenerSize Size { get; set; }
    public string DisplayName { get; set; } = "NO DISPLAY NAME";

    // Surface area in Mm2
    public double SurfaceArea { get; set; } = 0;
}