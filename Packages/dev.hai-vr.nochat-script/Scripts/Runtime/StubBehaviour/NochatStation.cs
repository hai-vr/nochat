using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace VRC.SDK3.Components
{
    [AddComponentMenu("Nochat/Nochat3 Station")]
    public class VRCStation : VRC.SDKBase.VRCStation
    {
        public bool Nochat_EnterThisStation;

        private void Update()
        {
            if (Nochat_EnterThisStation)
            {
                Nochat_EnterThisStation = false;
                gameObject.GetComponent<UdonSharpBehaviour>().OnStationEntered(Networking.LocalPlayer);
            }
        }
    }
}