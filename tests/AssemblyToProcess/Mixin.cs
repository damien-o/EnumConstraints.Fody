using AssemblyToProcess.Extensions;

namespace AssemblyToProcess;

#pragma warning disable S1104 // Fields should not have public accessibility
#pragma warning disable S2376 // Write-only properties should not be used
public class Mixin
{
    public StatusAsInt64? value;
    public StatusAsInt64? SetOnly
    {
        set { this.value = value; }
    }
    public StatusAsInt64? NullableAuto { get; set; }
    public StatusAsInt64 Auto { get; set; }
    public StatusAsInt64? NullableReadonlyGet { get; }
    public StatusAsInt64? NullableAutoReadonlyGet => value;
    public StatusAsInt64 AutoReadonlyGet => value!.Value;

    public StatusAsInt64? NullableGetInit { get; init; } = StatusAsInt64.One;
    public StatusAsInt64 UnitializedGetInit { get; init; }

    public StatusAsInt64? NullablePrivateGetSet { private get; set; }
    public StatusAsInt64? NullablePrivateGet => NullablePrivateGetSet;
    public StatusAsInt64? NullableGetPrivateSet { get; private set; }

    public string? Other { get; set; }
    public int? NullableInteger { get; set; }
    public int Integer { get; set; }

    public virtual StatusAsInt64 Vritual { get; set; }
    public virtual List<int> OtherVritual { get; set; } = new();
}

#pragma warning restore S2376 // Write-only properties should not be used
#pragma warning restore S1104 // Fields should not have public accessibility
