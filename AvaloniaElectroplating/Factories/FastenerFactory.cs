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
            return new Bolt(selectedFastenerSize, value);
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
            return new UmbracoBolt(selectedFastenerSize, value);
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
            return new Washer(selectedFastenerSize);
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
            return new Nut(selectedFastenerSize);
        }
        catch
        {
            Console.WriteLine("Failed to create new nut!!!");
            return null;
        }
    }
}