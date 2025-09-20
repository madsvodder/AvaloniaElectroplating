using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Messages;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaElectroplating.ViewModels;

public partial class AboutPageViewModel : PageViewModel
{
    public AboutPageViewModel()
    {
        PageName = ApplicationPageNames.About;
    }
    
    [RelayCommand]
    private void NavigateBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigateToMessage(ApplicationPageNames.Calculate));
    }
}