using JetBrains.Annotations;
using UnityEngine;

namespace VRC.SDKBase
{
    // TODO Stub
    public class VRC_Pickup : MonoBehaviour
    {
        // FIXME: Unused
        public float proximity = 1;

        // ReSharper disable InconsistentNaming
        [PublicAPI] public bool pickupable { get; set; } = true;
        [PublicAPI] public VRCPlayerApi currentPlayer { get; private set; }
        [PublicAPI] public bool IsHeld => CoreIsHeld;
        [PublicAPI] public PickupHand currentHand { get; private set; }
        // ReSharper restore InconsistentNaming
        
        public bool CoreIsHeld { get; private set; }

        public void Drop()
        {
            CoreIsHeld = false;
            currentPlayer = null;
            currentHand = PickupHand.None;
        }

        public void Core_SetIsHeldByLocalPlayer(bool isHeld, bool isRight)
        {
            CoreIsHeld = isHeld;
            currentHand = isRight ? PickupHand.Right : PickupHand.Left;
            currentPlayer = Networking.LocalPlayer;
        }

        public enum PickupHand
        {
            None, Left, Right
        }
    }
}