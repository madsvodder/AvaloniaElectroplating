using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;

namespace AvaloniaElectroplating;

public class BoltCalculator
{
    public double CalculateSurfaceArea(Bolt bolt)
    {
        if (!FastenersDatabase.BoltsDictionary.ContainsKey(bolt.Size))
        {
            Console.WriteLine("Bolt size not found or supported!");
            return 0;
        }
        
        BoltDimensions dim = FastenersDatabase.BoltsDictionary[bolt.Size];
        
        double d = dim.Diameter;      // Diameter
        double w = dim.WrenchSize;    // Nøglevidde (across flats)
        double h = dim.HeadHeight;    // Head height
    
        // Shank
        double shank = 2 * Math.PI * d / 2 * bolt.ThreadLength;
        
        // Flat sides
        double flats = d * h * 6;

        // Top of the head
        double head = (w * w * 2) - (Math.PI * (d / 2) * (d / 2));
        
        // Total * 1.187 because of threads
        double total = (shank + flats + head) * 1.187;

        // Total surface area in mm2
        bolt.SurfaceArea = total;
        
        return total;
    }
}