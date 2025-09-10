using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class CalculatePageViewModel : PageViewModel
{
    
    [ObservableProperty] private List<FastenerType> _fastenerTypes;
    [ObservableProperty] private FastenerType _selectedFastenerType;
    [ObservableProperty] private FastenerSize _selectedFastenerSize;
    [ObservableProperty] private string _value;
    [ObservableProperty] private string _total;
    
    public ObservableCollection<FastenerSize> AvailableSizes { get; } = new();
    public ObservableCollection<Fastener> FastenersToCalc { get; } = new();
    
    public CalculatePageViewModel()
    {
        PageName = ApplicationPageNames.Calculate;
        
        // Fill first combo box with fastener types
        FastenerTypes = Enum.GetValues<FastenerType>().ToList();
        
        // Populates the list of sizes at the start
        OnSelectedFastenerTypeChanged(SelectedFastenerType);
    }

    [RelayCommand]
    private void AddFastenerToList()
    {
        switch (SelectedFastenerType)
        {
            case FastenerType.Bolt:
                FastenersToCalc.Add(CreateBolt());
                break;
            case FastenerType.Washer:
                FastenersToCalc.Add(CreateWasher());
                break;
        }
    }

    private Bolt CreateBolt()
    {
        try
        {
            var doubleValue = double.Parse(Value);
            var b = new Bolt(SelectedFastenerSize, doubleValue);
            b.DisplayName = string.Format("{0} {1} {2}{3}", b.Type, b.Size, b.ThreadLength, "mm");
            return b;
        }
        catch
        {
            Console.WriteLine("Failed to create new bolt!!!");
            return null;
        }
    }
    
    private Washer CreateWasher()
    {
        try
        {
            var b = new Washer(SelectedFastenerSize);
            b.DisplayName = string.Format("{0} {1}", b.Type, b.Size);
            return b;
        }
        catch
        {
            Console.WriteLine("Failed to create new washer!!!");
            return null;
        }
    }

    // When the first combo box selection is changed.
    partial void OnSelectedFastenerTypeChanged(FastenerType value)
    {
        AvailableSizes.Clear();

        switch (value)
        {
            case FastenerType.Bolt:
                foreach (var size in FastenersDatabase.BoltsDictionary.Keys)
                    AvailableSizes.Add(size);
                break;
            
            case FastenerType.Washer:
                foreach (var size in FastenersDatabase.WashersDictionary.Keys)
                    AvailableSizes.Add(size);
                break;
            case FastenerType.Nut:
                Console.WriteLine("To Do...");
                break;
            default:
                Console.WriteLine("None selected...");
                break;
        }
        
        SelectedFastenerSize = AvailableSizes.FirstOrDefault();
    }

    [RelayCommand]
    private void CalculateAll()
    {
        BoltCalculator bc = new();
        WasherCalculator wc = new();

        

        if (FastenersToCalc.Count > 0)
        {
            // Reset total string
            Total = "0.0";
        
            // Count
            double count = 0;
            
            foreach (var fa in FastenersToCalc)
            {
                switch (fa.Type)
                {
                    case FastenerType.Bolt:
                        bc.CalculateSurfaceArea((Bolt) fa);
                        break;
                    case FastenerType.Washer:
                        wc.CalculateSurfaceArea((Washer)fa);
                        break;
                }

                count += fa.SurfaceArea;
            }

            CurrentCalculator cc = new();

            Total = $"You need: {cc.CalculateCurrent(count)}mA";
        }
        else
        {
            Console.WriteLine("No fasteners to calculate!");
        }
    }
}