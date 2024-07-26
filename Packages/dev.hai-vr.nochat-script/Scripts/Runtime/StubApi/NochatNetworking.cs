using JetBrains.Annotations;
using UnityEngine;

namespace VRC.SDKBase
{
    public class Networking
    {
        private static VRCPlayerApi _localPlayer = new VRCPlayerApi();
        public static VRCPlayerApi LocalPlayer { get => _localPlayer; }
        
        private static bool _isClogged = false;
        // ReSharper disable ConvertToAutoProperty
        [PublicAPI] public static bool IsClogged { get => _isClogged; }
        // ReSharper restore ConvertToAutoProperty

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

        public static bool IsOwner(VRCPlayerApi player, GameObject networkedObject)
        {
            // TODO stub
            return true;
        }

        public static VRCPlayerApi GetOwner(GameObject networkedObject)
        {
            // TODO stub
            return LocalPlayer;
        }

        public static void SetOwner(VRCPlayerApi player, GameObject networkedObject)
        {
            // TODO stub
        }

        public static double GetServerTimeInSeconds()
        {
            // TODO: Stub
            return Time.time;
        }
    }
}