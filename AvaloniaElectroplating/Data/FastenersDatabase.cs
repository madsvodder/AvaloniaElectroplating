using System.Collections.Generic;
using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class FastenersDatabase
{
    public static Dictionary<FastenerSize, BoltDimensions> BoltsDictionary = new()
    {
        { FastenerSize.M6, new BoltDimensions(6, 10, 4)},
        { FastenerSize.M8, new BoltDimensions(8, 13, 5.3)},
        { FastenerSize.M10, new BoltDimensions(10, 16, 6.4)},
        { FastenerSize.M12, new BoltDimensions(12, 18, 7.5)},
        { FastenerSize.M16, new BoltDimensions(16, 24, 10)},
    };

    public static Dictionary<FastenerSize, WasherDimensions> WashersDictionary = new()
    {
        {FastenerSize.M4,  new WasherDimensions(4.30, 9.00, 0.80)},
        {FastenerSize.M6,  new WasherDimensions(6.40, 12.00, 1.60)},
        {FastenerSize.M8,  new WasherDimensions(8.40, 16.00, 1.60)},
        {FastenerSize.M10, new WasherDimensions(10.50, 20.00, 2.00)},
        {FastenerSize.M12, new WasherDimensions(13.00, 24.00, 2.50)},
    };
    
    public static Dictionary<FastenerSize, NutDimensions> NutsDictionary = new()
    {
        { FastenerSize.M6,  new NutDimensions(6, 10, 5)},
        { FastenerSize.M8,  new NutDimensions(8, 13, 6.5)},
        { FastenerSize.M10, new NutDimensions(10, 17, 8)},
        { FastenerSize.M12, new NutDimensions(12, 19, 10)},
        { FastenerSize.M16, new NutDimensions(16, 24, 13)},
    };
}