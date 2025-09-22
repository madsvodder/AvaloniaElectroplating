using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class ConverterPageViewModel : ViewModelBase
{

    [ObservableProperty] private List<Units> _unitsList = Enum.GetValues<Units>().ToList();

    [ObservableProperty] private double _value;
    [ObservableProperty] private double _result;

    [ObservableProperty] private Units _selectedFromUnit = Units.inches;
    [ObservableProperty] private Units _selectedToUnit = Units.mm;
    
    [RelayCommand]
    private void Convert()
    {
        try
        {
            Console.WriteLine($"Converting from: {SelectedFromUnit} to: {SelectedToUnit}");
            Result = UnitConverter.Convert(Value, SelectedFromUnit, SelectedToUnit);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error converting value! {e.Message}");
        }
    }

    partial void OnValueChanged(double oldValue, double newValue)
    {
        Convert();
    }

    partial void OnSelectedFromUnitChanged(Units oldValue, Units newValue)
    {
        Convert();
    }

    partial void OnSelectedToUnitChanged(Units oldValue, Units newValue)
    {
        Convert();
    }
}