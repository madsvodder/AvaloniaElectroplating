using AvaloniaElectroplating.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaElectroplating.ViewModels;

public partial class PageViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationPageNames _pageName;
}