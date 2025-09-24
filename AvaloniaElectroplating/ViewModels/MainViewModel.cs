using System;
using System.Threading.Tasks;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Factories;
using AvaloniaElectroplating.Messages;
using AvaloniaElectroplating.Models;
using AvaloniaElectroplating.Services;
using AvaloniaElectroplating.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaElectroplating.ViewModels;

public partial class MainViewModel : ViewModelBase, IRecipient<NavigateToMessage>
{

    [ObservableProperty]
    private PageViewModel? _currentPage;

    private readonly PageFactory _pageFactory;

    private UserSettingsService _settingsService;
    
    // Constructor for live design
    public MainViewModel(){}
    public MainViewModel(PageFactory pageFactory,  UserSettingsService settingsService)
    {
        _pageFactory = pageFactory;

        _settingsService = settingsService;
        
        WeakReferenceMessenger.Default.Register<NavigateToMessage>(this);
        
        NavigateTo(ApplicationPageNames.Calculate);

        CustomInitialize();
    }

    private async Task CustomInitialize()
    {
        await _settingsService.LoadSettingsFromJson();
    }

    public void Receive(NavigateToMessage message)
    {
        NavigateTo(message.Value);
    }

    private void NavigateTo(ApplicationPageNames page)
    {
        CurrentPage = page switch
        {
            ApplicationPageNames.Calculate => _pageFactory.CreatePage(ApplicationPageNames.Calculate),
            ApplicationPageNames.Settings => _pageFactory.CreatePage(ApplicationPageNames.Settings),
            ApplicationPageNames.About => _pageFactory.CreatePage(ApplicationPageNames.About),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    [RelayCommand]
    private void NavigateToSettings()
    {
        CurrentPage = _pageFactory.CreatePage(ApplicationPageNames.Settings);
    }
    
}