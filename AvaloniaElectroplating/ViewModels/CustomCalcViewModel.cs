using AvaloniaElectroplating.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaElectroplating.ViewModels;

public partial class CustomCalcViewModel : ViewModelBase
{

    private readonly CurrentCalculator _currentCalculator;

    [ObservableProperty] private double _mm2Value;

    [ObservableProperty] private string _resultString = "";

    public CustomCalcViewModel(CurrentCalculator currentCalculator)
    {
        _currentCalculator = currentCalculator;
    }

    partial void OnMm2ValueChanged(double value)
    {
        var result = _currentCalculator.CalculateCurrent(value);
        ResultString = $"{result} mA needed";
    }
}