using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace NochatScript.Core
{
    public class NochatUpdateCoordinator : MonoBehaviour
    {
        public Transform viewpointRepresentation;
        public Transform leftController;
        public Transform rightController;
        public NochatController leftState;
        public NochatController rightState;
        private Collider[] _leftOverlaps;
        private Collider[] _rightOverlaps;

        private void Update()
        {
            leftState.DoUpdate();
            rightState.DoUpdate();

            VRCPlayerApi.Nochat_ProvideForTrackingData(viewpointRepresentation, leftController, rightController);
            
            ExecutePickupActionsFor(rightController, rightState, ref _rightOverlaps);
            ExecutePickupActionsFor(leftController, leftState, ref _leftOverlaps);
        }

        private void ExecutePickupActionsFor(Transform controller, NochatController nochatController, ref Collider[] overlaps)
        {
            var referencePos = controller.position;
            
            if (nochatController.GripJustPressed)
            {
                overlaps = FindPickupsNear(referencePos);

                foreach (var overlap in overlaps)
                {
                    overlap.GetComponent<VRC_Pickup>().Core_SetIsHeldByLocalPlayer(true);
                    overlap.GetComponent<UdonSharpBehaviour>().OnPickup(); // FIXME: Trigger this on all behaviours
                }
            }

            if (nochatController.GripJustReleased)
            {
                foreach (var overlap in overlaps)
                {
                    overlap.GetComponent<VRC_Pickup>().Core_SetIsHeldByLocalPlayer(false);
                    overlap.GetComponent<UdonSharpBehaviour>().OnDrop(); // FIXME: Trigger this on all behaviours
                }

                overlaps = Array.Empty<Collider>();
            }
            
            if (nochatController.GripDown)
            {
                foreach (var overlap in overlaps)
                {
                    if (overlap != null)
                    {
                        overlap.transform.position = referencePos;
                    }
                }
            }

            if (nochatController.TriggerJustPressed)
            {
                foreach (var overlap in overlaps)
                {
                    overlap.GetComponent<UdonSharpBehaviour>().OnPickupUseDown(); // FIXME: Trigger this on all behaviours
                }
            }

            if (nochatController.TriggerJustReleased)
            {
                foreach (var overlap in overlaps)
                {
                    overlap.GetComponent<UdonSharpBehaviour>().OnPickupUseUp(); // FIXME: Trigger this on all behaviours
                }
            }
        }

        private Collider[] FindPickupsNear(Vector3 referencePos)
        {
            var grabRadiusCheck = 0.2f;
            var candidates = Physics.OverlapSphere(referencePos, grabRadiusCheck);
            if (candidates.Length > 0)
            {
                var closestDistance = float.MaxValue;
                var indexOfClosest = -1;
                for (var index = 0; index < candidates.Length; index++)
                {
                    var candidate = candidates[index];
                    
                    var pickup = candidate.GetComponent<VRC_Pickup>();
                    if (pickup == null) continue;
                    if (pickup.CoreIsHeld) continue;
                    if (!pickup.pickupable) continue;
                        
                    var distance = Vector3.Distance(candidate.transform.position, referencePos);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        indexOfClosest = index;
                    }
                }

                if (indexOfClosest != -1)
                {
                    return new[] { candidates[indexOfClosest] };
                }
            }

            return Array.Empty<Collider>();
        }
    }
}