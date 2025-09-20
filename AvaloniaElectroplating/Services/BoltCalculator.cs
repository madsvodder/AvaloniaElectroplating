using System;
using AvaloniaElectroplating.Models;

namespace AvaloniaElectroplating.Services;

public class BoltCalculator : ICalculator
{
    public double CalculateSurfaceArea(Fastener fastener)
    {
        if (fastener is not Bolt bolt)
            throw new ArgumentException("Expected a bolt", nameof(fastener));
        
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