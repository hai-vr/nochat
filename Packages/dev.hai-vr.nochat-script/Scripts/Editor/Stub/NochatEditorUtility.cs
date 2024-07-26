namespace NochatScript.Editor
{
    public static class NochatEditorUtility
    {
        public static NochatBehaviour GetBackingNosharpBehaviour(object element)
        {
            // TODO: Check this stub
            if (element is NochatBehaviour behaviour)
            {
                return behaviour;
            }
            return null;
        }
    }
}