using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;

namespace AvaloniaElectroplating.Factories;

public class FastenerFactory
{

    public Fastener CreateFastener(double? value, FastenerSize selectedFastenerSize, FastenerType selectedFastenerType)
    {
        switch (selectedFastenerType)
        {
            case FastenerType.Bolt:
                return CreateBolt(selectedFastenerSize, value.Value);
            case FastenerType.UmbracoBolt:
                return CreateUmbracoBolt(selectedFastenerSize, value.Value);
            case FastenerType.Washer:
                return CreateWasher(selectedFastenerSize);
            case FastenerType.Nut:
                return CreateNut(selectedFastenerSize);
            default:
                return new Washer(FastenerSize.Undefined);
        }
    }
    
    public Bolt CreateBolt(FastenerSize selectedFastenerSize, double value)
    {
        try
        {
            var b = new Bolt(selectedFastenerSize, value);
            b.DisplayName = string.Format("{0} {1} {2}{3}", b.Type, b.Size, b.ThreadLength, "mm");
            return b;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Bolt(FastenerSize.Undefined, 0);
        }
    }
    
    public UmbracoBolt CreateUmbracoBolt(FastenerSize selectedFastenerSize, double value)
    {
        try
        {
            var b = new UmbracoBolt(selectedFastenerSize, value);
            b.DisplayName = string.Format("{0} {1} {2}{3}", b.Type, b.Size, b.ThreadLength, "mm");
            return b;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new UmbracoBolt(FastenerSize.Undefined, 0);
        }
    }
    
    public Washer CreateWasher(FastenerSize selectedFastenerSize)
    {
        try
        {
            var b = new Washer(selectedFastenerSize);
            b.DisplayName = string.Format("{0} {1}", b.Type, b.Size);
            return b;
        }
        catch
        {
            Console.WriteLine("Failed to create new washer!!!");
            return null;
        }
    }
    
    public Nut CreateNut(FastenerSize selectedFastenerSize)
    {
        try
        {
            var b = new Nut(selectedFastenerSize);
            b.DisplayName = string.Format("{0} {1}", b.Type, b.Size);
            return b;
        }
        catch
        {
            Console.WriteLine("Failed to create new nut!!!");
            return null;
        }
    }
}