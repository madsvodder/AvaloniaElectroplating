using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class UmbracoBolt : Fastener
{
    public double ThreadLength { get; }

    public UmbracoBolt(FastenerSize size, double threadLength)
    {
        Type = FastenerType.UmbracoBolt;
        Size = size;
        ThreadLength = threadLength;
    }
}