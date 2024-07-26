using System;

namespace UdonSharp
{
    public class UdonBehaviourSyncMode : Attribute
    {
        public UdonBehaviourSyncMode(BehaviourSyncMode syncMode)
        {
            // TODO: Stub
        }
    }

    public enum BehaviourSyncMode
    {
        // FIXME: Ordering
        None,
        NoVariableSync,
        Manual,
        Continuous
    }

    public enum UdonSyncMode
    {
        None,
        Linear
    }
}