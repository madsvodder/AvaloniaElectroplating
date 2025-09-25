using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Factories;
using AvaloniaElectroplating.Messages;
using AvaloniaElectroplating.Models;
using AvaloniaElectroplating.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaElectroplating.ViewModels;

public partial class CalculatePageViewModel : PageViewModel
{
    // Calculator classes - Change to dependency injection?
    private CalculatorFactory _calculatorFactory = new();
    private FastenerFactory _fc = new();
    
    // User settings
    private UserSettingsService _settingsService;

    // Timer view model - CHANGE THIS SO IT COMES FROM THE FACTORY!?
    [ObservableProperty] private TimerPageViewModel _timerPage = new();
    [ObservableProperty] private ConverterPageViewModel _converterPage = new();
    
    [ObservableProperty] private List<FastenerType> _fastenerTypes = Enum.GetValues<FastenerType>().ToList();
    [ObservableProperty] private FastenerType _selectedFastenerType;
    [ObservableProperty] private FastenerSize? _selectedFastenerSize;
    [ObservableProperty] private double? _value;
    [ObservableProperty] private string _totalCurrentString = "";
    [ObservableProperty] private string _totalAreaString = "";

    public ObservableCollection<FastenerSize> AvailableSizes { set; get; } = new(); 
    public ObservableCollection<Fastener> FastenersToCalc { get; } = new();
    
    public CalculatePageViewModel(UserSettingsService userSettingsService)
    {
        PageName = ApplicationPageNames.Calculate;
        
        // Populates the list of sizes at the start
        OnSelectedFastenerTypeChanged(SelectedFastenerType);
        
        // User settings service
        _settingsService = userSettingsService;
    }

    [RelayCommand]
    private void NavigateToSettings()
    {
        WeakReferenceMessenger.Default.Send(new NavigateToMessage(ApplicationPageNames.Settings));
    }
    [RelayCommand]
    private void NavigateToAbout()
    {
        WeakReferenceMessenger.Default.Send(new NavigateToMessage(ApplicationPageNames.About));

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
                FastenersToCalc.Add(_fc.CreateFastener(Value.Value, SelectedFastenerSize.Value, SelectedFastenerType));
                break;
            
            case FastenerType.UmbracoBolt:
                if (!Value.HasValue)
                {
                    Console.WriteLine("Umbraco bolt requires a value!");
                    return;
                }
                FastenersToCalc.Add(_fc.CreateFastener(Value.Value, SelectedFastenerSize.Value, SelectedFastenerType));
                break;
            
            case FastenerType.Nut:
                FastenersToCalc.Add(_fc.CreateFastener(null, SelectedFastenerSize.Value, SelectedFastenerType));
                break;
                
            case FastenerType.Washer:
                FastenersToCalc.Add(_fc.CreateFastener(null, SelectedFastenerSize.Value, SelectedFastenerType));
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
            
            case FastenerType.UmbracoBolt:
                foreach (var size in FastenersDatabase.UmbracoBoltsDictionary.Keys)
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
    }
    
    private void CalculateAll()
    {
        // Reset total string
        TotalCurrentString = "";
        TotalAreaString = "";
        
        if (FastenersToCalc.Count > 0)
        {
            // Count
            double count = 0;
            
            foreach (var fa in FastenersToCalc)
            {
                switch (fa.Type)
                {
                    case FastenerType.Bolt:
                        var bc = _calculatorFactory.CreateCalculator(FastenerType.Bolt);
                        bc.CalculateSurfaceArea(fa);
                        break;
                    case FastenerType.UmbracoBolt:
                        var ubc = _calculatorFactory.CreateCalculator(FastenerType.UmbracoBolt);
                        ubc.CalculateSurfaceArea(fa);
                        break;
                    case FastenerType.Washer:
                        var wc = _calculatorFactory.CreateCalculator(FastenerType.Washer);
                        wc.CalculateSurfaceArea(fa);
                        break;
                    case FastenerType.Nut:
                        var nc = _calculatorFactory.CreateCalculator(FastenerType.Nut);
                        nc.CalculateSurfaceArea(fa);
                        break;
                }

                count += fa.SurfaceArea;
            }

            CurrentCalculator cc = new();
            
            // Times with the user settings multiplier
            double currentNeeded = Math.Round(cc.CalculateCurrent(count) * _settingsService.Settings.FinalCurrentMultiplier, 3);
            double roundedSurfaceArea = Math.Round(count, 0);

            TotalCurrentString = $"⚡ Current needed: {currentNeeded} A";
            TotalAreaString = $"📐 Surface area: {roundedSurfaceArea} mm2";
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