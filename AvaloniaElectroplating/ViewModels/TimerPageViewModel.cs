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
    private DispatcherTimer _timer = new();
    
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
            _timer.Tick += TimerOnTick;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
        }
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        if (_time == TimeSpan.Zero)
        {
            StopTimer();
            return;
        }

        _time = _time.Add(TimeSpan.FromSeconds(-1));
        UpdateTime();
    }

    private void UpdateTime()
    {
        TimeString = _time.ToString(@"mm\:ss");
        CurrentTime = (int)_time.TotalSeconds;
    }

    private void StopTimer()
    {
        _timer.Stop();
        _timer.Tick -= TimerOnTick; // prevents duplicate firing
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
    [RelayCommand]
    private void AddOneMinute()
    {
        _time = _time.Add(TimeSpan.FromMinutes(1));
        UpdateTime();
        Console.WriteLine("Added one minute");
    }
}