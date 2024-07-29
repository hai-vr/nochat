using UdonSharp;
using VRC.Udon;

namespace UdonSharpEditor
{
    public static class UdonSharpEditorUtility
    {
        public static UdonBehaviour GetBackingUdonBehaviour(object element)
        {
            // TODO: Check this stub
            if (element is UdonSharpBehaviour behaviour)
            {
                // FIXME: How to get the exact UdonBehaviour associated with this?
                return behaviour.GetComponent<UdonBehaviour>();
            }
            return null;
        }
    }
}