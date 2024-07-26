using JetBrains.Annotations;

namespace VRC.Udon.Common // FIXME: Behaviours seem to go full path. This isn't sustainable
{
    public class UdonInputEventArgs
    {
        // ReSharper disable InconsistentNaming
        [PublicAPI] public bool boolValue { get; }
        // ReSharper restore InconsistentNaming
    }
}