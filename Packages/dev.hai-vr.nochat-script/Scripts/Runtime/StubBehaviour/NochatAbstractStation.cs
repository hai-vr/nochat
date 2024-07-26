using JetBrains.Annotations;
using UnityEngine;

namespace VRC.SDKBase
{
    public abstract class VRCStation : MonoBehaviour
    {
        [PublicAPI] public Transform stationEnterPlayerLocation;

        public void ExitStation(VRCPlayerApi localPlayer)
        {
            // TODO Stub
        }
    }
}