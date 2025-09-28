using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class Washer : Fastener
{
    public Washer(FastenerSize fastenerSize)
    {
        FastenerType = FastenerType.Washer;
        Size = fastenerSize;
    }

    public override string ToString()
    {
        return string.Format("{0} {1}", Type, Size);
    }
}