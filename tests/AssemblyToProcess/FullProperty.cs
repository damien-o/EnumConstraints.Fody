using AssemblyToProcess.Extensions;

namespace AssemblyToProcess;

public class FullProperty
{
    public StatusAsShort? field;

    public StatusAsShort? Property
    {
        get => field;
        set => field = value;
    }
}
