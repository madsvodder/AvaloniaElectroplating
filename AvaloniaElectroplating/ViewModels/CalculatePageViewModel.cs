using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Factories;
using AvaloniaElectroplating.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class CalculatePageViewModel : PageViewModel
{
    // Calculator classes - Change to dependency injection?
    FastenerFactory fc = new();
    BoltCalculator bc = new();
    WasherCalculator wc = new();
    NutCalculator nc = new();
    
    [ObservableProperty] private List<FastenerType> _fastenerTypes;
    [ObservableProperty] private FastenerType _selectedFastenerType;
    [ObservableProperty] private FastenerSize? _selectedFastenerSize;
    [ObservableProperty] private double? _value;
    [ObservableProperty] private string _totalString = "";
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
        if (!SelectedFastenerSize.HasValue)
        {
            Console.WriteLine("No size selected!");
            return;
        }

        switch (SelectedFastenerType)
        {
            case FastenerType.Bolt:
                if (!Value.HasValue)
                {
                    Console.WriteLine("Bolt requires a value!");
                    return;
                }
                FastenersToCalc.Add(fc.CreateFastener(Value.Value, SelectedFastenerSize.Value, SelectedFastenerType));
                break;
            
            case FastenerType.Nut:
            case FastenerType.Washer:
                FastenersToCalc.Add(fc.CreateFastener(null, SelectedFastenerSize.Value, SelectedFastenerType));
                break;
            
            default:
                Console.WriteLine("Default");
                break;
        }

        // Finally calculate everything
        CalculateAll();
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
                foreach (var size in FastenersDatabase.NutsDictionary.Keys)
                    AvailableSizes.Add(size);
                break;
            
            default:
                AvailableSizes.Add(FastenerSize.Undefined);
                Console.WriteLine("None selected...");
                break;
        }
        
        SelectedFastenerSize = AvailableSizes.FirstOrDefault();
        Console.WriteLine(SelectedFastenerSize);
    }
    
    private void CalculateAll()
    {
        // Reset total string
        TotalString = "";
        
        if (FastenersToCalc.Count > 0)
        {
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
                    case FastenerType.Nut:
                        nc.CalculateSurfaceArea((Nut)fa);
                        break;
                }

                count += fa.SurfaceArea;
            }

            CurrentCalculator cc = new();

            TotalString = $"You need: {cc.CalculateCurrent(count)}A";
        }
        else
        {
            Console.WriteLine("No fasteners to calculate!");
        }
    }

    [RelayCommand]
    private void RemoveFastener(Fastener fastener)
    {
        FastenersToCalc.Remove(fastener);
        CalculateAll();
    } 
    
    [RelayCommand]
    private void ClearFasteners()
    {
        FastenersToCalc.Clear();
        CalculateAll();
    }
}