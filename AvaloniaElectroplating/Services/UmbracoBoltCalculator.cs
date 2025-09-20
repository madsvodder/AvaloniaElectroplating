using System;

namespace AvaloniaElectroplating.Models;

public class UmbracoBoltCalculator : ICalculator
{
    public double CalculateSurfaceArea(Fastener fastener)
    {
        if (fastener is not UmbracoBolt bolt)
            throw new ArgumentException("Expected a Umbraco Bolt", nameof(UmbracoBolt));

        
        if (!FastenersDatabase.UmbracoBoltsDictionary.ContainsKey(bolt.Size))
            return 0;

        var dim = FastenersDatabase.UmbracoBoltsDictionary[bolt.Size];

        double d = dim.Diameter;
        double hd = dim.HeadDiameter;
        double hh = dim.HeadHeight;
        double hex = dim.HexSocketSize;

        // Shank
        double shank = Math.PI * d * bolt.ThreadLength;

        // Head side (cylindrical)
        double headSide = Math.PI * hd * hh;

        // Head top (subtract hex hole)
        double headTop = Math.PI * Math.Pow(hd / 2, 2) - Math.PI * Math.Pow(hex / 2, 2);

        // Bottom under head (subtract shank hole)
        double headBottom = Math.PI * Math.Pow(hd / 2, 2) - Math.PI * Math.Pow(d / 2, 2);

        double total = (shank + headSide + headTop + headBottom) * 1.187;

        bolt.SurfaceArea = total;
        return total;
    }
}