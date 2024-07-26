using JetBrains.Annotations;
using UnityEngine;

namespace NochatScript
{
    public class NochatPlayerApi
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

        public static NochatPlayerApi GetPlayerById(int allowedPlayer)
        {
            // TODO: Stub
            return new NochatPlayerApi();
        }

        public Vector3 GetPosition()
        {
            // TODO: Stub
            return Vector3.zero;
        }

        public static NochatPlayerApi[] GetPlayers(NochatPlayerApi[] targetArray)
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

        public class TrackingData
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

        public TrackingData GetTrackingData(TrackingDataType which)
        {
            // FIXME: MEGA STUB
            var tr = Camera.main.transform;
            return new TrackingData(tr.position, tr.rotation);
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
            // TODO STUB
            return Vector3.zero;
        }

        public void PlayHapticEventInHand(object hand, float p1, float p2, float p3)
        {
            // TODO: Stub
        }
    }
}