using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class Bolt : Fastener
{
    
    public double ThreadLength { get; }

    public Bolt(FastenerSize size, double threadLength)
    {
        FastenerType = FastenerType.Bolt;
        Size = size;
        ThreadLength = threadLength;
    }

    public override string ToString()
    {
        return string.Format("{0} {1} {2}{3}", Type, Size, ThreadLength, "mm");
    }
}