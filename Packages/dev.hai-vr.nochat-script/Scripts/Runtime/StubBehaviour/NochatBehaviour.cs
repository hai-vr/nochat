using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using NochatScript.Core;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;
using VRC.Udon.Common.Enums;
using Object = UnityEngine.Object;

namespace UdonSharp
{
    public class UdonSharpBehaviour : MonoBehaviour
    {
        [PublicAPI] public NochatPublicVarHolder publicVariables { get; private set; }
        [PublicAPI] public bool DisableInteractive { get; set; }
        
        private Dictionary<string, FieldInfo> _fieldCache = new Dictionary<string, FieldInfo>();

        public object GetProgramVariable(string programVariableName)
        {
            // TODO: Handle invalid var names
            if (!_fieldCache.ContainsKey(programVariableName))
            {
                var field = GetType().GetField(programVariableName);
                if (field == null)
                {
                    // FIXME: If we add (Public), would it also include it? 
                    var field2 = GetType().GetField(programVariableName, BindingFlags.Instance | BindingFlags.NonPublic);
                    if (field2 == null)
                    {
                        Debug.LogError($"Failed to fetch field for program variable {programVariableName}, this may indicate an issue");
                        return null;
                    }
                    else
                    {
                        _fieldCache[programVariableName] = field2;
                    }
                }
                else
                {
                    _fieldCache[programVariableName] = field;
                }
            }
            return _fieldCache[programVariableName].GetValue(this);
        }

        public void RequestSerialization()
        {
            // TODO: Stub
        }

        public void SetProgramVariable(string programVariableName, object value)
        {
            Debug.Log($"Setting program variable {programVariableName} to {value} in {GetType().Name}");
            // FIXME: If we add (Public), would it also include it? 
            // FIXME: Use the field cache
            var field = GetType().GetField(programVariableName) ?? GetType().GetField(programVariableName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
            {
                field.SetValue(this, value);
            }
        }

        public void SendCustomEvent(string eventName)
        {
            Debug.Log($"{name} is being sent custom event {eventName}");
            try
            {
                SelfTriggerEvent(eventName);
            }
            catch (Exception e)
            {
                Debug.LogError($"An exception has occured during SendCustomEvent: {e.Message}");
                Debug.LogException(e);
                Console.WriteLine(e);
            }
        }

        public void SendCustomEventDelayedSeconds(string eventName, float delaySeconds)
        {
            Debug.Log($"{name} is being sent custom event {eventName}, delayed by {delaySeconds} seconds");
            TryStartCoroutine(DelayTime(eventName, delaySeconds), eventName);
        }

        public void SendCustomEventDelayedSeconds(string eventName, float delaySeconds, EventTiming timing)
        {
            // TODO Stub: Timing is not used, we need to run this in LateUpdate if Event timing is late update
            Debug.Log($"{name} is being sent custom event {eventName}, delayed by {delaySeconds} seconds");
            TryStartCoroutine(DelayTime(eventName, delaySeconds), eventName);
        }
        
        public void SendCustomEventDelayedFrames(string eventName, int frameCount)
        {
            Debug.Log($"{name} is being sent custom event {eventName}, delayed by {frameCount} frames");
            TryStartCoroutine(DelayFrames(eventName, frameCount), eventName);
        }
        
        public void SendCustomEventDelayedFrames(string eventName, int frameCount, EventTiming timing)
        {
            // TODO Stub: Timing is not used, we need to run this in LateUpdate if Event timing is late update
            Debug.Log($"{name} is being sent custom event {eventName}, delayed by {frameCount} frames");
            TryStartCoroutine(DelayFrames(eventName, frameCount), eventName);
        }

        private void TryStartCoroutine(IEnumerator enumerator, string eventName)
        {
            if (gameObject.activeInHierarchy)
            {
                // FIXME: Just because it is active doesn't mean it's going to stay active.
                // We may have to extract all coroutines to another object like done below (but maybe singleton-like).
                StartCoroutine(enumerator);
            }
            else
            {
                Debug.Log($"Can't start coroutine on disabled object for event {eventName}. Creating temporary behaviour...");
                var runner = new GameObject
                {
                    name = "CoroutineRunner"
                }.AddComponent<NochatCoroutineRunner>();
                runner.StartCoroutine(Runner(runner, enumerator, eventName));
            }
        }

        private IEnumerator Runner(NochatCoroutineRunner runner, IEnumerator enumerator, string eventName)
        {
            yield return enumerator;
            Debug.Log($"Temporary runner complete for {eventName}, destroying self...");
            Object.Destroy(runner.gameObject);
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
            
            var nochatBehaviour = GetComponent<UdonSharpBehaviour>();
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

        // FIXME: Method name
        public string GetUdonTypeName<T>()
        {
            // TODO: Stub
            return (typeof(T)).FullName;
        }

        public void SendCustomNetworkEvent(object whatever, string eventName)
        {
            Debug.Log($"{name} is trying to send custom NETWORKED event {eventName}");
            try
            {
                SelfTriggerEvent(eventName);
            }
            catch (Exception e)
            {
                Debug.LogError($"An exception has occured during SendCustomNetworkEvent: {e.Message}");
                Debug.LogException(e);
                Console.WriteLine(e);
            }
            // TODO: Stub
        }

        public T VRCInstantiate<T>(T input) where T : Object
        {
            // TODO: Stub
            return (T)Object.Instantiate(input);
        }

        public virtual void OnPlayerJoined(VRCPlayerApi player) { }
        public virtual void Interact() { }
        public virtual void OnDeserialization() { }
        public virtual void OnPickup() { }
        public virtual void OnDrop() { }
        public virtual void OnPickupUseDown() { }
        public virtual void OnPickupUseUp() { }
        public virtual void OnOwnershipTransferred(VRCPlayerApi player) { }
        public virtual void OnPlayerLeft(VRCPlayerApi player) { }
        public virtual void OnPlayerCollisionEnter(VRCPlayerApi player) { }
        public virtual void OnPlayerCollisionExit(VRCPlayerApi player) { }
        public virtual void OnStationEntered(VRCPlayerApi player) { }
        public virtual void OnStationExited(VRCPlayerApi player) { }
        public virtual void InputJump(bool value, UdonInputEventArgs args) { }
        public virtual void OnPreSerialization() { }
        public virtual void OnPlayerRespawn(VRCPlayerApi player) { }
        public virtual void OnPostSerialization(VRC.Udon.Common.SerializationResult result) { }
        public virtual void OnDeserialization(DeserializationResult result) {}
        public virtual void PostLateUpdate() {} // FIXME: Call PostLateUpdate
    }

    public class NochatPublicVarHolder
    {
        public void TryGetVariableValue<T>(string variableName, out T output)
        {
            output = default;
        }
    }
}