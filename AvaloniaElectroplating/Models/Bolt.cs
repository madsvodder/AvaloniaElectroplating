using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class Bolt : Fastener
{
    
    public double ThreadLength { get; set; }

    public Bolt(FastenerSize size, double threadLength)
    {
        Type = FastenerType.Bolt;
        Size = size;
        ThreadLength = threadLength;
    }
}