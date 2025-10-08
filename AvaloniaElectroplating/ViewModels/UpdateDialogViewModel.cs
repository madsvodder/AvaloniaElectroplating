using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaElectroplating.ViewModels;

public partial class UpdateDialogViewModel : ViewModelBase
{
    [ObservableProperty] private string _versionText;
    [ObservableProperty] private string _changelog;
    [ObservableProperty] private string _downloadUrl;

    public UpdateDialogViewModel(string version, string changelog, string downloadUrl)
    {
        VersionText = $"New version {version} is available!";
        Changelog = changelog;
        DownloadUrl = downloadUrl;
    }

    [RelayCommand]
    private void OpenBrowser()
    {

        var url = DownloadUrl;

        try
        {
            // .NET Core / .NET 5+ has Process.Start with UseShellExecute = true
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch
        {
            // Fallback for non-Windows (or restricted environments)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }
}