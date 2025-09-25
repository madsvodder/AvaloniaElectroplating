using System.Threading.Tasks;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Messages;
using AvaloniaElectroplating.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaElectroplating.ViewModels;

public partial class AboutPageViewModel : PageViewModel
{

    private UserSettingsService _settingsService;

    [ObservableProperty] private bool _checkForUpdatesOnStartup = true;

    public AboutPageViewModel(UserSettingsService userSettingsService)
    {
        PageName = ApplicationPageNames.About;
        _settingsService = userSettingsService;
        SetUiFromUserSettings();
    }
    
    [RelayCommand]
    private async Task NavigateBack()
    {
        SetUserSettingsFromUi();
        await _settingsService.SaveSettingsAsJson();
        WeakReferenceMessenger.Default.Send(new NavigateToMessage(ApplicationPageNames.Calculate));
    }

    private void SetUiFromUserSettings()
    {
        CheckForUpdatesOnStartup = _settingsService.Settings.CheckForUpdatesOnStartup;
    }

    private void SetUserSettingsFromUi()
    {
        _settingsService.Settings.CheckForUpdatesOnStartup = CheckForUpdatesOnStartup;
    }
}