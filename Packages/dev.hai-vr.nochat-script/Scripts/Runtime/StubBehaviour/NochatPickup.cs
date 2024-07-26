using JetBrains.Annotations;
using UnityEngine;

namespace NochatScript
{
    // TODO Stub
    public class NochatPickup : MonoBehaviour
    {
        // ReSharper disable InconsistentNaming
        [PublicAPI] public bool pickupable { get; set; }
        [PublicAPI] public NochatPlayerApi currentPlayer { get; private set; }
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
            currentPlayer = NochatNetworking.LocalPlayer;
        }

        public enum PickupHand
        {
            Left, Right
        }
    }
}