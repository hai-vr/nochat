namespace NochatScript
{
    public class NochatUtilities
    {
        public static bool IsValid<T>(T obj)
        {
            // FIXME: Does this invoke the Unity lifetime check?
            return obj != null;
        }
    }
}