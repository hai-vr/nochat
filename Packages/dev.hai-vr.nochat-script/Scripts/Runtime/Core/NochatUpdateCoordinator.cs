using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace NochatScript.Core
{
    public class NochatUpdateCoordinator : MonoBehaviour
    {
        public Animator animator;
        public Transform viewpointRepresentation;
        public Transform leftController;
        public Transform rightController;
        
        public Transform cameraSpace;
        public Transform avatarSpace;
        public Transform avatarSpaceViewpoint;
        public Transform avatarSpaceLeft;
        public Transform avatarSpaceRight;
        
        public NochatController leftState;
        public NochatController rightState;
        private Collider[] _leftOverlaps = Array.Empty<Collider>();
        private Collider[] _rightOverlaps = Array.Empty<Collider>();
        
        private readonly Collider[] _workColliders_Capacity = new Collider[50];

        private void Update()
        {
            CopyCameraSpaceToAvatarSpace();

            leftState.DoUpdate();
            rightState.DoUpdate();

            NochatInput.LeftTrigger = leftState.TriggerAnalog;
            NochatInput.LeftGrip = leftState.GripAnalog;
            NochatInput.LeftAxisX = leftState.Thumbstick.x;
            NochatInput.LeftAxisY = leftState.Thumbstick.y;
            
            NochatInput.RightTrigger = rightState.TriggerAnalog;
            NochatInput.RightGrip = rightState.GripAnalog;
            NochatInput.RightAxisX = rightState.Thumbstick.x;
            NochatInput.RightAxisY = rightState.Thumbstick.y;

            VRCPlayerApi.Nochat_ProvideForTrackingData(avatarSpaceViewpoint, avatarSpaceLeft, avatarSpaceRight);
            if (animator != null)
            {
                VRCPlayerApi.Nochat_ProvideForHumanoidData(animator);
            }
            
            ExecutePickupActionsFor(avatarSpaceRight, rightState, ref _rightOverlaps, true);
            ExecutePickupActionsFor(avatarSpaceLeft, leftState, ref _leftOverlaps, false);
        }

        private void CopyCameraSpaceToAvatarSpace()
        {
            CopyTransform(cameraSpace, avatarSpace);
            CopyTransform(viewpointRepresentation, avatarSpaceViewpoint);
            CopyTransform(leftController, avatarSpaceLeft);
            CopyTransform(rightController, avatarSpaceRight);
        }

        private void CopyTransform(Transform from, Transform to)
        {
            to.transform.position = from.transform.position;
            to.transform.rotation = from.transform.rotation;
            to.transform.localScale = from.transform.localScale;
        }

        private void ExecutePickupActionsFor(Transform controller, NochatController nochatController, ref Collider[] overlaps, bool isRight)
        {
            var referencePos = controller.position;
            
            if (nochatController.GripJustPressed)
            {
                overlaps = FindPickupsNear(referencePos);

                foreach (var overlap in overlaps)
                {
                    overlap.GetComponent<VRC_Pickup>().Core_SetIsHeldByLocalPlayer(true, isRight);
                    overlap.GetComponent<UdonSharpBehaviour>().OnPickup(); // FIXME: Trigger this on all behaviours
                }
            }

            if (nochatController.GripJustReleased)
            {
                foreach (var overlap in overlaps)
                {
                    overlap.GetComponent<VRC_Pickup>().Core_SetIsHeldByLocalPlayer(false, isRight);
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
            
            var workColliders_ActualCount = Physics.OverlapSphereNonAlloc(referencePos, grabRadiusCheck, _workColliders_Capacity);
            if (workColliders_ActualCount > 0)
            {
                var closestDistance = float.MaxValue;
                var indexOfClosest = -1;
                for (var index = 0; index < workColliders_ActualCount; index++)
                {
                    var candidate = _workColliders_Capacity[index];
                    
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
                    return new[] { _workColliders_Capacity[indexOfClosest] };
                }
            }

            return Array.Empty<Collider>();
        }
    }
}