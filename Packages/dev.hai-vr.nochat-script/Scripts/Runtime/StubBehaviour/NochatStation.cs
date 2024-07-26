using JetBrains.Annotations;
using UnityEngine;
using NochatPlayerApi = VRC.SDKBase.VRCPlayerApi;

namespace VRC.SDKBase
{
    public class VRCStation : MonoBehaviour
    {
        [PublicAPI] public Transform stationEnterPlayerLocation;

        public void ExitStation(NochatPlayerApi localPlayer)
        {
            // TODO Stub
        }
    }
}

namespace VRC.SDK3.Components // FIXME: ??????
{
    public class VRCStation : VRC.SDKBase.VRCStation // FIXME: ?????? in SaccFlight, see SaacVehicleSeat at line 71
    {
    }
}