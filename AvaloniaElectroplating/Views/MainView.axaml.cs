using Avalonia.Controls;
using AvaloniaElectroplating.Messages;
using AvaloniaElectroplating.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaElectroplating.Views;

// In MainView.axaml.cs
public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<MainView, WelcomeMessage>(this, static (w, m) =>
        {
            var dialog = new WelcomePageWindow()
            {
                DataContext = new WelcomePageViewModel()
            };

            m.Reply(dialog.ShowDialog<WelcomePageViewModel?>(w));
        });

        this.Opened += async (_, _) =>
        {
            WeakReferenceMessenger.Default.Send(new WelcomeMessage());
        };
    }
}
