using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class Nut : Fastener
{
    public Nut(FastenerSize size)
    {
        FastenerType = FastenerType.Nut;
        Size = size;
    }

    public override string ToString()
    {
        return string.Format("{0} {1}", Type, Size);
    }
}