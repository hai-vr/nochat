using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace NochatScript.Core
{
    // https://docs.unity3d.com/Manual/xr_input.html
    public class NochatController : MonoBehaviour
    {
        public bool isRight;

        private List<InputDevice> _hand;
        public bool TriggerDown { get; private set; }
        public bool TriggerJustPressed { get; private set; }
        public bool TriggerJustReleased { get; private set; }
        public bool GripDown { get; private set; }
        public bool GripJustPressed { get; private set; }
        public bool GripJustReleased { get; private set; }

        public float TriggerAnalog { get; private set; }
        public float GripAnalog { get; private set; }
        public Vector2 Thumbstick { get; private set; }

        private void Awake()
        {
            _hand = new List<InputDevice>();
        }

        private void OnEnable()
        {
            var allDevices = new List<InputDevice>();
            InputDevices.GetDevices(allDevices);
            foreach (var device in allDevices) OnDeviceConnected(device);

            InputDevices.deviceConnected -= OnDeviceConnected;
            InputDevices.deviceDisconnected -= OnDeviceDisconnected;
            
            InputDevices.deviceConnected += OnDeviceConnected;
            InputDevices.deviceDisconnected += OnDeviceDisconnected;
        }

        private void OnDisable()
        {
            InputDevices.deviceConnected -= OnDeviceConnected;
            InputDevices.deviceDisconnected -= OnDeviceDisconnected;
            _hand.Clear();
        }

        private void OnDeviceConnected(InputDevice device)
        {
            var whichHand = isRight ? InputDeviceCharacteristics.Right : InputDeviceCharacteristics.Left;
            var isCorrectHand = (device.characteristics & whichHand) != 0;
            if (isCorrectHand /*&& device.TryGetFeatureValue(_feature, out _)*/) _hand.Add(device);
        }

        private void OnDeviceDisconnected(InputDevice device)
        {
            if (_hand.Contains(device))
                _hand.Remove(device);
        }

        internal void DoUpdate()
        {
            var newTrigger = IsDown(CommonUsages.triggerButton, _hand);
            var newGrip = IsDown(CommonUsages.gripButton, _hand);
            TriggerAnalog = Analog(CommonUsages.trigger, _hand);
            GripAnalog = Analog(CommonUsages.grip, _hand);
            Thumbstick = Joystick(CommonUsages.primary2DAxis, _hand);
            
            if (newTrigger != TriggerDown)
            {
                TriggerDown = newTrigger;
                TriggerJustPressed = newTrigger;
                TriggerJustReleased = !newTrigger;
            }
            else
            {
                TriggerJustPressed = false;
                TriggerJustReleased = false;
            }
            if (newGrip != GripDown)
            {
                GripDown = newGrip;
                GripJustPressed = newGrip;
                GripJustReleased = !newGrip;
            } 
            else
            {
                GripJustPressed = false;
                GripJustReleased = false;
            }
        }

        private bool IsDown(InputFeatureUsage<bool> feature, List<InputDevice> hand)
        {
            foreach (var device in hand)
            {
                var result = device.TryGetFeatureValue(feature, out var buttonState) && buttonState;
                if (result) return true;
            }

            return false;
        }

        private float Analog(InputFeatureUsage<float> feature, List<InputDevice> hand)
        {
            foreach (var device in hand)
            {
                var result = device.TryGetFeatureValue(feature, out var floatValue);
                if (result) return floatValue;
            }

            return 0f;
        }

        private Vector2 Joystick(InputFeatureUsage<Vector2> feature, List<InputDevice> hand)
        {
            foreach (var device in hand)
            {
                var result = device.TryGetFeatureValue(feature, out var vectorValue);
                if (result) return vectorValue;
            }

            return Vector2.zero;
        }
    }
}