using UnityEngine;

namespace NochatScript
{
    public class NochatNetworking
    {
        private static NochatPlayerApi _localPlayer = new NochatPlayerApi();
        public static NochatPlayerApi LocalPlayer { get => _localPlayer; }

        public static int GetServerTimeInMilliseconds()
        {
            // TODO stub
            return 0;
        }

        public static bool IsOwner(GameObject networkedObject)
        {
            // TODO stub
            return true;
        }

        public static bool IsOwner(NochatPlayerApi player, GameObject networkedObject)
        {
            // TODO stub
            return true;
        }

        public static NochatPlayerApi GetOwner(GameObject networkedObject)
        {
            // TODO stub
            return LocalPlayer;
        }

        public static void SetOwner(NochatPlayerApi player, GameObject networkedObject)
        {
            // TODO stub
        }
    }
}