using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating;

public interface ICalculable
{
    double SurfaceArea { get; }
    ModelType Type { get; set; }

    string ToString();
}