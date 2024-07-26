using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace VRC.SDKBase
{
    public class VRCPlayerApi
    {
        private bool _inVR = true;
        public bool IsUserInVR()
        {
            // TODO: Stub
            return _inVR;
        }

        private int _playerId = 1;
        public int playerId { get => _playerId; }
        
        private string _displayName = "Nochat Player 1";
        public string displayName { get => _displayName; }
        
        private bool _isLocal = true;
        public bool isLocal { get => _isLocal; }
        
        private bool _isMaster = true;
        public bool isMaster { get => _isMaster; }

        private readonly Dictionary<string, string> _tags = new Dictionary<string, string>();
        private static TrackingData _headTrackingData;
        private static TrackingData _leftTrackingData;
        private static TrackingData _rightTrackingData;

        public static VRCPlayerApi GetPlayerById(int allowedPlayer)
        {
            // TODO: Stub
            return new VRCPlayerApi();
        }

        public Vector3 GetPosition()
        {
            // TODO: Stub
            return _headTrackingData.position;
        }

        public static VRCPlayerApi[] GetPlayers(VRCPlayerApi[] targetArray)
        {
            // TODO: Stub
            return targetArray;
        }

        public static int GetPlayerCount()
        {
            // TODO: Stub
            return 1;
        }

        public bool IsOwner(GameObject networkedObject)
        {
            // TODO: Stub
            return true;
        }

        public struct TrackingData
        {
            public TrackingData(Vector3 position, Quaternion rotation)
            {
                this.position = position;
                this.rotation = rotation;
            }

            // ReSharper disable InconsistentNaming
            [PublicAPI] public Vector3 position { get; private set; }
            [PublicAPI] public Quaternion rotation { get; private set; }
            // ReSharper restore InconsistentNaming
        }
        
        public static void Nochat_ProvideForTrackingData(Transform viewpointRepresentation, Transform leftController, Transform rightController)
        {
            // TODO: Stub
            _headTrackingData = new TrackingData(viewpointRepresentation.position, viewpointRepresentation.rotation);
            _leftTrackingData = new TrackingData(leftController.position, leftController.rotation);
            _rightTrackingData = new TrackingData(rightController.position, rightController.rotation);
        }

        public TrackingData GetTrackingData(TrackingDataType which)
        {
            switch (which)
            {
                case TrackingDataType.Head:
                    return _headTrackingData;
                case TrackingDataType.LeftHand:
                    return _leftTrackingData;
                case TrackingDataType.RightHand:
                    return _rightTrackingData;
                default:
                    throw new ArgumentOutOfRangeException(nameof(which), which, null);
            }
        }

        public enum TrackingDataType
        {
            Head,
            LeftHand,
            RightHand
        }

        public void Immobilize(bool b)
        {
            // TODO: Stub
            throw new System.NotImplementedException();
        }

        public Vector3 GetBonePosition(HumanBodyBones bone)
        {
            // FIXME: MEGA STUB
            return Camera.main.transform.position;
        }

        public Quaternion GetBoneRotation(HumanBodyBones bone)
        {
            // FIXME: MEGA STUB
            return Camera.main.transform.rotation;
        }

        public void PlayHapticEventInHand(object hand, float p1, float p2, float p3)
        {
            // TODO: Stub
        }

        public void SetPlayerTag(string key, string value)
        {
            _tags[key] = value;
        }

        public string GetPlayerTag(string key)
        {
            if (_tags.TryGetValue(key, out var tag)) return tag;

            return ""; // FIXME: Stub, is this correct?
        }

        public void UseAttachedStation()
        {
            // TODO Stub
        }

        public void SetVelocity(Vector3 velocity)
        {
            // TODO Stub
        }

        public void SetVoiceDistanceNear(float f)
        {
            // TODO Stub
        }

        public void SetVoiceDistanceFar(float f)
        {
            // TODO Stub
        }

        public void SetVoiceGain(float f)
        {
            // TODO Stub
        }

        public bool IsPlayerGrounded()
        {
            // TODO Stub
            return true;
        }

        public Vector3 GetVelocity()
        {
            // TODO stub
            return Vector3.zero;
        }
    }
}