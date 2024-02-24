using AssemblyToProcess.Extensions;

namespace AssemblyToProcess;

#pragma warning disable S2292 // Trivial properties should be auto-implemented
#pragma warning disable S1104 // Fields should not have public accessibility
public abstract class Abstract
{
    public abstract StatusAsByte Enum { get; set; }
    public abstract string? Other { get; set; }
}

public class VirtualBase : Abstract
{
    public StatusAsByte inVirtualBase;

    public override StatusAsByte Enum
    {
        get => inVirtualBase;
        set => inVirtualBase = value;
    }

    public override string? Other { get; set; }
}

public class Virtual : VirtualBase
{
    public StatusAsByte inVirtual;
    public override StatusAsByte Enum
    {
        get => inVirtual;
        set => inVirtual = value;
    }
}



#pragma warning restore S1104 // Fields should not have public accessibility
#pragma warning restore S2292 // Trivial properties should be auto-implemented
