namespace VRC.SDKBase
{
    public class Utilities
    {
        public static bool IsValid<T>(T obj)
        {
            // FIXME: Does this invoke the Unity lifetime check?
            return obj != null;
        }
    }
}