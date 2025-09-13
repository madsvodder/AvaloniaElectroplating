using AvaloniaElectroplating.Enums;

namespace AvaloniaElectroplating.Models;

public class Nut : Fastener
{
    public Nut(FastenerSize size)
    {
        Type = FastenerType.Nut;
        Size = size;
    }
}