namespace AssemblyToProcess.Extensions;

[Flags]
public enum FlagStatus : byte
{
    None = 0,
    Active = 1,
    Hidden = 2,
}
