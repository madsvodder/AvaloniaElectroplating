using System.Threading.Tasks;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Messages;
using AvaloniaElectroplating.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaElectroplating.ViewModels;

public partial class SettingsPageViewModel : PageViewModel
{

    private UserSettingsService _settingsService;
    
    // Settings properties
    [ObservableProperty] private double _currentMultiplier;
    public SettingsPageViewModel(UserSettingsService userSettingsService)
    {
        PageName = ApplicationPageNames.Settings;
        _settingsService = userSettingsService;
        
        SetUiToUserSettings();
    }

    [RelayCommand]
    private async Task NavigateBack()
    {
        SetUserSettingsFromUi();
        await _settingsService.SaveSettingsAsJson();
        WeakReferenceMessenger.Default.Send(new NavigateToMessage(ApplicationPageNames.Calculate));
    }

    private void SetUiToUserSettings()
    {
        CurrentMultiplier = _settingsService.Settings.FinalCurrentMultiplier;
    }
    
    private void SetUserSettingsFromUi()
    {
        _settingsService.Settings.FinalCurrentMultiplier = CurrentMultiplier;
    }
}