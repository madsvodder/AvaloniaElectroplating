using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Messages;
using AvaloniaElectroplating.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaElectroplating.ViewModels;

public partial class SettingsPageViewModel : PageViewModel
{

    private UserSettingsService _settingsService;
    public SettingsPageViewModel(UserSettingsService userSettingsService)
    {
        PageName = ApplicationPageNames.Settings;
        _settingsService = userSettingsService;
    }

    [RelayCommand]
    private void NavigateBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigateToMessage(ApplicationPageNames.Calculate));
    }
}