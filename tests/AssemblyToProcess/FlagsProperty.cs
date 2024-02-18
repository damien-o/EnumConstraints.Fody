using AssemblyToProcess.Extensions;

namespace AssemblyToProcess;

public class FlagsProperty
{
    public FlagStatus? field;

    public FlagStatus? Property
    {
        get => field;
        set => field = value;
    }
}
