using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class UmbracoBolt : Fastener
{
    public double ThreadLength { get; }

    public UmbracoBolt(FastenerSize size, double threadLength)
    {
        FastenerType = FastenerType.UmbracoBolt;
        Size = size;
        ThreadLength = threadLength;
    }

    public override string ToString()
    {
        return string.Format("{0} {1} {2}{3}", Type, Size, ThreadLength, "mm");
    }
}