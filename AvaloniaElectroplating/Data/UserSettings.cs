using System.Text.Json;
using System.Threading.Tasks;

namespace AvaloniaElectroplating.Models;

public class UserSettings
{
    public double CurrentDensity { get; set; } = 0.9;
    public bool CheckForUpdatesOnStartup { get; set; } = true;
}