using System;

namespace NochatScript
{
    public class NochatBehaviourSyncMode : Attribute
    {
        public NochatBehaviourSyncMode(NochatSyncMode syncMode)
        {
            // TODO: Stub
        }
    }

    public enum NochatSyncMode
    {
        // FIXME: Ordering
        None,
        NoVariableSync,
        Manual
    }
}