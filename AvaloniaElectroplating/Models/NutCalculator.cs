using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;

namespace AvaloniaElectroplating;

public class NutCalculator
{
    public double CalculateSurfaceArea(Nut nut)
    {
        if (!FastenersDatabase.NutsDictionary.ContainsKey(nut.Size))
        {
            Console.WriteLine("Nut size not found or supported!");
            return 0;
        }

        NutDimensions nd = FastenersDatabase.NutsDictionary[nut.Size];

        double d = nd.Diameter;      // Thread diameter
        double s = nd.WrenchSize;    // Flat-to-flat
        double m = nd.Thickness;     // Thickness

        // Side length of hexagon given flat-to-flat distance
        double a = s / Math.Sqrt(3);

        // Hexagon area
        double hexArea = (3 * Math.Sqrt(3) / 2) * a * a;

        // Hole area
        double holeArea = Math.PI * Math.Pow(d / 2.0, 2);

        // Top + bottom faces
        double faceArea = 2 * (hexArea - holeArea);

        // Hexagonal side area
        double perimeter = 6 * a;
        double sideArea = perimeter * m;

        // Inner cylindrical surface (threaded hole)
        double innerArea = Math.PI * d * m;

        // Total - * 1,1 because of threads?
        double totalSurfaceArea = (faceArea + sideArea + innerArea) * 1.1;

        nut.SurfaceArea = totalSurfaceArea;

        return totalSurfaceArea;
    }
}