using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaElectroplating.ViewModels;

public partial class CustomCalcViewModel : ViewModelBase
{

    private readonly CurrentCalculator _currentCalculator = new();

    [ObservableProperty] private double _mm2Value;

    [ObservableProperty] private string _resultString = "";

    partial void OnMm2ValueChanged(double value)
    {
        var result = _currentCalculator.CalculateCurrent(value);
        ResultString = $"{result} mA needed";
    }
}