using JetBrains.Annotations;
using UnityEngine;

namespace VRC.SDKBase
{
    // TODO Stub
    public class VRC_Pickup : MonoBehaviour
    {
        // ReSharper disable InconsistentNaming
        [PublicAPI] public bool pickupable { get; set; }
        [PublicAPI] public VRCPlayerApi currentPlayer { get; private set; }
        [PublicAPI] public bool IsHeld => CoreIsHeld;
        // ReSharper restore InconsistentNaming
        
        public bool CoreIsHeld { get; private set; }

        public void Drop()
        {
            CoreIsHeld = false;
            currentPlayer = null;
        }

        public void Core_SetIsHeldByLocalPlayer(bool isHeld)
        {
            CoreIsHeld = isHeld;
            currentPlayer = Networking.LocalPlayer;
        }

        public enum PickupHand
        {
            Left, Right
        }
    }
}