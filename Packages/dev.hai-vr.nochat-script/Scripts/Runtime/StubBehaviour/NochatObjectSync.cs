using UnityEngine;

namespace VRC.SDK3.Components
{
    public class VRCObjectSync : MonoBehaviour
    {
        private Vector3 _initialLocalPos;
        private Quaternion _initialLocalRot;

        private void Awake()
        {
            _initialLocalPos = transform.localPosition;
            _initialLocalRot = transform.localRotation;
        }

        public void Respawn()
        {
            // TODO Stub
            transform.localPosition = _initialLocalPos;
            transform.localRotation = _initialLocalRot;
        }
    }
}