using System;
using System.Timers;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class TimerPageViewModel : ViewModelBase
{

    [ObservableProperty] private int _currentTime;
    [ObservableProperty] private string _timeString = "20:00";
    [ObservableProperty] private bool _isTimerEnabled = false;
    [ObservableProperty] private string _buttonString = "Start";
    
    // 20 minutes in seconds
    private TimeSpan _time = TimeSpan.FromMinutes(20);
    
    // Timer
    private DispatcherTimer timer = new();
    
    [RelayCommand]
    private void SetTimer()
    {
        if (IsTimerEnabled)
        {
            StopTimer();
        }
        else
        {
            IsTimerEnabled = true;
            ButtonString = "Stop";
            timer.Tick += TimerOnTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        if (_time == TimeSpan.Zero)
        {
            StopTimer();
        }

        _time = _time.Add(TimeSpan.FromSeconds(-1));
        //TimeString = _time.ToString("c");
        string formatted = _time.ToString(@"mm\:ss");
        TimeString = formatted;
    }

    private void StopTimer()
    {
        timer.Stop();
        timer.Tick -= TimerOnTick; // prevents duplicate firing
        ResetTimer();
        ButtonString = "Start";
    }


    private void ResetTimer()
    {
        _time = TimeSpan.FromMinutes(20);
        CurrentTime = 0;
        TimeString = "20:00";
        IsTimerEnabled = false;
    }
    
}