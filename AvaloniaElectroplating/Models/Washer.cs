using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class Washer : Fastener
{
    public Washer(FastenerSize fastenerSize)
    {
        Type = FastenerType.Washer;
        Size = fastenerSize;
    }
}