using NochatScript;
using UnityEngine;

// FIXME: Unsure if namespace needs to be the same to avoid breaking prefabs
namespace VRC.Udon
{
    // FIXME:
    // This uses the same UUID as the original UdonBehaviour.
    // This is because the SendCustomEvent calls used by UI elements fail to trigger otherwise.
    // Don't know if there's a better way to fix this yet.
    public sealed class UdonBehaviour : MonoBehaviour
    {
        public void SendCustomEvent(string eventName)
        {
            Debug.Log($"In UdonBehaviour, {name} is trying to send custom event {eventName}");
            SelfTriggerEvent(eventName);
        }

        public void SendCustomEventDelayedSeconds(string eventName, float delaySeconds)
        {
            Debug.Log($"In UdonBehaviour, {name} is trying to send custom event {eventName}, delayed by {delaySeconds} seconds");
            SelfTriggerEvent(eventName); // FIXME: Stub, implement delay
        }

        public void SendCustomEventDelayedFrames(string eventName, int frameCount)
        {
            Debug.Log($"In UdonBehaviour, {name} is trying to send custom event {eventName}, delayed by {frameCount} frames");
            SelfTriggerEvent(eventName); // FIXME: Stub, implement delay
        }

        private void SelfTriggerEvent(string eventName)
        {
            // FIXME: This should fail silently without interrupting execution
            var nochatBehaviour = GetComponent<NochatBehaviour>();
            var method = nochatBehaviour.GetType().GetMethod(eventName);
            method.Invoke(nochatBehaviour, null);
        }
    }
}
