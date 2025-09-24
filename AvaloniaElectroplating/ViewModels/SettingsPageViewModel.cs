using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Messages;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaElectroplating.ViewModels;

public partial class SettingsPageViewModel : PageViewModel
{
    public SettingsPageViewModel()
    {
        PageName = ApplicationPageNames.Settings;
    }

    [RelayCommand]
    private void NavigateBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigateToMessage(ApplicationPageNames.Calculate));
    }
}