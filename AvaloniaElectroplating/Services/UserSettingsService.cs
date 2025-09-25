using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AvaloniaElectroplating.Models;

namespace AvaloniaElectroplating.Services;

public class UserSettingsService
{

    public UserSettings Settings { get; private set; } = new();

    public async Task SaveSettingsAsJson()
    {

        Console.WriteLine("Saving settings...");

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var jsonString = JsonSerializer.Serialize<UserSettings>(Settings, options);

        await File.WriteAllTextAsync("settings.json", jsonString);

        Console.WriteLine("Settings saved.");
    }

    public async Task LoadSettingsFromJson()
    {
        if (!DoesSaveExist())
        {
            Console.WriteLine("No settings file found, creating new one...");
            await SaveSettingsAsJson();
        }

        if (DoesSaveExist())
        {
            Console.WriteLine("Loading settings...");
            var settingsJson = await File.ReadAllTextAsync("settings.json");
            Settings = JsonSerializer.Deserialize<UserSettings>(settingsJson);
        }
        else
        {
            Console.WriteLine("Failed to load or settings file.");
        }
    }

    private bool DoesSaveExist()
    {
        var b = File.Exists("settings.json");
        return b;
    }
}