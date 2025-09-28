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

    public ObservableCollection<ICalculable> AllItemsToCalc { get; } = new();


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

    // REMOVED FASTENER FACTORY
    [RelayCommand]
    private void AddFastenerToList()
    {
        try
        {
            ICalculable item = SelectedFastenerType switch
            {
                FastenerType.Bolt when Value.HasValue => new Bolt(SelectedFastenerSize.Value, Value.Value),
                FastenerType.UmbracoBolt when Value.HasValue => new UmbracoBolt(SelectedFastenerSize.Value, Value.Value),
                FastenerType.Nut => new Nut(SelectedFastenerSize.Value),
                FastenerType.Washer => new Washer(SelectedFastenerSize.Value),
                FastenerType.CustomMm2 when Value.HasValue => new PlateModel(Value.Value),
                _ => null
            };

            if (item == null) return;

            AllItemsToCalc.Add(item);
            CalculateAll();
        }
        catch
        {
            Console.WriteLine("Error adding to list! Catched error!!!");
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

    private double CalculateFasteners()
    {
        double count = 0;

        foreach (var item in AllItemsToCalc)
        {
            if (item is Fastener fa)
            {
                var calc = _calculatorFactory.CreateCalculator(fa.FastenerType);
                calc.CalculateSurfaceArea(fa);
            }

            count += item.SurfaceArea;
        }

        return count;
    }

    [RelayCommand]
    private void RemoveFastener(ICalculable item)
    {
        AllItemsToCalc.Remove(item);
        CalculateAll();
    }

    [RelayCommand]
    private void ClearFasteners()
    {
        AllItemsToCalc.Clear();
        CalculateAll();
    }

    private void CalculateAll()
    {
        // Reset total string
        TotalCurrentString = "";
        TotalAreaString = "";

        // Count
        double count = 0;

        count += CalculateFasteners();

        CurrentCalculator cc = new();

        // Times with the user settings current density - THIS SHOULD BE MOVED TO THE CURRENT CALCULATOR!?
        double currentNeeded = Math.Round(cc.CalculateCurrent(count) * _settingsService.Settings.CurrentDensity, 3);
        double roundedSurfaceArea = Math.Round(count, 0);

        TotalCurrentString = $"⚡ Current needed: {currentNeeded} A";
        TotalAreaString = $"📐 Surface area: {roundedSurfaceArea} mm2";
    }
}