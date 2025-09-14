using System.Collections.Generic;
using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class FastenersDatabase
{
    public static Dictionary<FastenerSize, BoltDimensions> BoltsDictionary = new()
    {
        { FastenerSize.M3,  new BoltDimensions(3, 5.5, 2) },
        { FastenerSize.M4,  new BoltDimensions(4, 7,   2.8) },
        { FastenerSize.M5,  new BoltDimensions(5, 8,   3.5) },
        { FastenerSize.M6,  new BoltDimensions(6, 10,  4) },
        { FastenerSize.M8,  new BoltDimensions(8, 13,  5.3) },
        { FastenerSize.M10, new BoltDimensions(10, 16, 6.4) },
        { FastenerSize.M12, new BoltDimensions(12, 18, 7.5) },
        { FastenerSize.M16, new BoltDimensions(16, 24, 10) },
        { FastenerSize.M20, new BoltDimensions(20, 30, 12.5) },
    };

    public static Dictionary<FastenerSize, WasherDimensions> WashersDictionary = new()
    {
        { FastenerSize.M3,  new WasherDimensions(3.2,  7.0,  0.5) },
        { FastenerSize.M4,  new WasherDimensions(4.3,  9.0,  0.8) },
        { FastenerSize.M5,  new WasherDimensions(5.3,  10.0, 1.0) },
        { FastenerSize.M6,  new WasherDimensions(6.4,  12.0, 1.6) },
        { FastenerSize.M8,  new WasherDimensions(8.4,  16.0, 1.6) },
        { FastenerSize.M10, new WasherDimensions(10.5, 20.0, 2.0) },
        { FastenerSize.M12, new WasherDimensions(13.0, 24.0, 2.5) },
        { FastenerSize.M16, new WasherDimensions(17.0, 30.0, 3.0) },
        { FastenerSize.M20, new WasherDimensions(21.0, 37.0, 3.0) },
    };

    public static Dictionary<FastenerSize, NutDimensions> NutsDictionary = new()
    {
        { FastenerSize.M3,  new NutDimensions(3,  5.5, 2.4) },
        { FastenerSize.M4,  new NutDimensions(4,  7,   3.2) },
        { FastenerSize.M5,  new NutDimensions(5,  8,   4.7) },
        { FastenerSize.M6,  new NutDimensions(6,  10,  5) },
        { FastenerSize.M8,  new NutDimensions(8,  13,  6.5) },
        { FastenerSize.M10, new NutDimensions(10, 17,  8) },
        { FastenerSize.M12, new NutDimensions(12, 19, 10) },
        { FastenerSize.M16, new NutDimensions(16, 24, 13) },
        { FastenerSize.M20, new NutDimensions(20, 30, 16) },
    };
}
