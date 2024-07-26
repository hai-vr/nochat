using UdonSharp;

namespace UdonSharpEditor
{
    public static class UdonSharpEditorUtility
    {
        public static UdonSharpBehaviour GetBackingUdonBehaviour(object element)
        {
            // TODO: Check this stub
            if (element is UdonSharpBehaviour behaviour)
            {
                return behaviour;
            }
            return null;
        }
    }
}