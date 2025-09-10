using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.Factories;
using AvaloniaElectroplating.Models;
using AvaloniaElectroplating.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Design time constructor
    /// </summary>
    public MainViewModel()
    {
        CurrentPage = new CalculatePageViewModel();
    }

    private PageFactory _pageFactory;
    
    [ObservableProperty]
    private PageViewModel _currentPage;

    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;

        GoToCalculate();
    }

    [RelayCommand]
    private void GoToCalculate() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Calculate);
    
    [RelayCommand]
    private void GoToNotes() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Notes);
    
        
    
}