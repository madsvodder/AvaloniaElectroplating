using System.Text.Json;
using System.Threading.Tasks;

namespace AvaloniaElectroplating.Models;

public class UserSettings
{
    public double FinalCurrentMultiplier { get; set; } = 1;
    public bool CheckForUpdatesOnStartup { get; set; } = true;
}