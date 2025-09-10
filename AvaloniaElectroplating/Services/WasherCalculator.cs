using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;

namespace AvaloniaElectroplating;

public class WasherCalculator
{
    public double CalculateSurfaceArea(Washer washer)
    {
        if (!FastenersDatabase.WashersDictionary.ContainsKey(washer.Size))
        {
            Console.WriteLine("Washer size not found or supported!");
            return 0;
        }

        WasherDimensions wd = FastenersDatabase.WashersDictionary[washer.Size];

        double id = wd.InternalDiameter;
        double ed = wd.ExternalDiameter;
        double t = wd.Thickness;

        double rInner = id / 2.0;
        double rOuter = ed / 2.0;

        // Top + Bottom faces
        double faceArea = 2 * Math.PI * (rOuter * rOuter - rInner * rInner);

        // Outer cylindrical edge
        double outerEdge = 2 * Math.PI * rOuter * t;

        // Inner cylindrical edge
        double innerEdge = 2 * Math.PI * rInner * t;

        double totalSurfaceArea = faceArea + outerEdge + innerEdge;

        return totalSurfaceArea;
    }
}