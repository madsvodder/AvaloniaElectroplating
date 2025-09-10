using System;
using AvaloniaElectroplating.Enums;
using AvaloniaElectroplating.ViewModels;

namespace AvaloniaElectroplating.Factories;

public class PageFactory
{
    private readonly Func<ApplicationPageNames, PageViewModel> _factory;
    
    public PageFactory(Func<ApplicationPageNames, PageViewModel> factory)
    {
        _factory = factory;
    }

    public PageViewModel GetPageViewModel(ApplicationPageNames pageName) => _factory.Invoke(pageName);
}