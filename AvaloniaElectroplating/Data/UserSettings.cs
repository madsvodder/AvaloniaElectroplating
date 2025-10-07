using System.Text.Json;
using System.Threading.Tasks;

namespace AvaloniaElectroplating.Models;

public class UserSettings
{
    public double CurrentDensity { get; set; } = 0.08;
    public bool CheckForUpdatesOnStartup { get; set; } = true;
}