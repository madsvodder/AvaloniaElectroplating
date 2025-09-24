using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class SplashScreenViewModel : ViewModelBase
{
    [ObservableProperty] private string _startupMessage = "Starting application...";

    [RelayCommand]
    public void Cancel()
    {
        StartupMessage = "Cancelling application...";
        _cts.Cancel();
    }

    private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    public CancellationToken Token => _cts.Token;
}