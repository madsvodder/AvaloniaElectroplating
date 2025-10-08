using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaElectroplating.UpdateSystem;
using AvaloniaElectroplating.ViewModels;

namespace AvaloniaElectroplating.Services;

public class UpdateService
{
    private string _currentVersion;

    public async Task CheckForUpdates()
    {
        try
        {

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Get local json info
            var localJson = await File.ReadAllTextAsync("version.json");
            var local = JsonSerializer.Deserialize<VersionInfo>(localJson, options);

            // Get remote json info
            using var client = new HttpClient();
            var remoteJson =
                await client.GetStringAsync(
                    "https://drive.google.com/uc?export=download&id=158W340Ca-eh-LFww9SjgrdJ1StZ5wX96");
            var remote = JsonSerializer.Deserialize<VersionInfo>(remoteJson, options);

            var localVersion = Version.Parse(local.Version);
            var remoteVersion = Version.Parse(remote.Version);

            if (remoteVersion > localVersion)
            {
                var dialog = new UpdateDialogWindow();
                dialog.DataContext = new UpdateDialogViewModel(remote.Version, remote.Changelog, remote.DownloadUrl);

                await dialog.ShowDialog((Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow);
            }
            else
            {
                Console.WriteLine("You are up to date!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to  check for updates: {e.Message}");
        }
    }
}