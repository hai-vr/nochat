using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

namespace NochatScript
{
    public class NochatBehaviour : MonoBehaviour
    {
        [PublicAPI] public NochatPublicVarHolder publicVariables { get; private set; }
        
        private Dictionary<string, FieldInfo> _fieldCache = new Dictionary<string, FieldInfo>();

        public object GetProgramVariable(string programVariableName)
        {
            // TODO: Handle invalid var names
            if (!_fieldCache.ContainsKey(programVariableName)) _fieldCache[programVariableName] = GetType().GetField(programVariableName);
            return _fieldCache[programVariableName].GetValue(this);
        }

        public void RequestSerialization()
        {
            // TODO: Stub
        }

        public void SetProgramVariable(string programVariableName, object value)
        {
            Debug.Log($"Setting program variable {programVariableName} to {value}");
            var field = GetType().GetField(programVariableName);
            if (field != null)
            {
                field.SetValue(this, value);
            }
        }

        public void SendCustomEvent(string eventName)
        {
            Debug.Log($"{name} is trying to send custom event {eventName}");
            SelfTriggerEvent(eventName); // FIXME: Stub, implement delay
        }

        public void SendCustomEventDelayedSeconds(string eventName, float delaySeconds)
        {
            Debug.Log($"{name} is trying to send custom event {eventName}, delayed by {delaySeconds} seconds");
            StartCoroutine(DelayTime(eventName, delaySeconds));
        }
        
        public void SendCustomEventDelayedFrames(string eventName, int frameCount)
        {
            Debug.Log($"{name} is trying to send custom event {eventName}, delayed by {frameCount} frames");
            StartCoroutine(DelayFrames(eventName, frameCount));
        }
        
        private IEnumerator DelayTime(string eventName, float delaySeconds)
        {    
            yield return new WaitForSeconds(delaySeconds);
            SelfTriggerEvent(eventName);
        }
        
        private IEnumerator DelayFrames(string eventName, float delayFrameCount)
        {
            // FIXME: Is this correct???
            if (delayFrameCount <= 0) delayFrameCount = 1;
            
            while (delayFrameCount > 0)
            {
                yield return new WaitForSeconds(0f);
                delayFrameCount--;
            }
            SelfTriggerEvent(eventName);
        }

        private void SelfTriggerEvent(string eventName)
        {
            // For parity, does not interrupt execution on the caller if there's no match.
            
            var nochatBehaviour = GetComponent<NochatBehaviour>();
            if (nochatBehaviour == null) return;
            
            var method = nochatBehaviour.GetType().GetMethod(eventName);
            if (method == null) return;
            
            Debug.Log($"Invoking event {eventName} on {nochatBehaviour.name} ({nochatBehaviour.GetType().Name})");
            method.Invoke(nochatBehaviour, null);
        }

        // FIXME: Method name
        public string GetUdonTypeName()
        {
            // TODO: Stub
            return GetType().FullName;
        }

        public void SendCustomNetworkEvent(object whatever, string eventName)
        {
            // TODO: Stub
        }

        public virtual void OnPlayerJoined(NochatPlayerApi player) { }
        public virtual void Interact() { }
        public virtual void OnDeserialization() { }
        public virtual void OnPickup() { }
        public virtual void OnDrop() { }
        public virtual void OnPickupUseDown() { }
        public virtual void OnPickupUseUp() { }
        public virtual void OnOwnershipTransferred(NochatPlayerApi player) { }
        public virtual void OnPlayerLeft(NochatPlayerApi player) { }
    }

    public class NochatPublicVarHolder
    {
        public void TryGetVariableValue<T>(string variableName, out T output)
        {
            output = default;
        }
    }
}