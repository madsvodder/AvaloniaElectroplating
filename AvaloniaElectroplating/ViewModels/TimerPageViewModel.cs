using System;
using System.Timers;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class TimerPageViewModel : ViewModelBase
{

    // Default to 20 minutes
    [ObservableProperty] private int _selectedTime = 20;
    [ObservableProperty] private int _currentTime;
    [ObservableProperty] private string _timeString = "";
    [ObservableProperty] private bool _isTimerEnabled = false;
    [ObservableProperty] private string _buttonString = "Start";
    
    // Time that is counting down
    private TimeSpan _time;
    
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
            _time = TimeSpan.FromMinutes(SelectedTime);
            IsTimerEnabled = true;
            ButtonString = "Stop";
            TimeString = SelectedTime.ToString("00") + ":00";
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
        _time = TimeSpan.FromMinutes(SelectedTime);
        CurrentTime = 0;
        TimeString = "";
        IsTimerEnabled = false;
    }
    
}