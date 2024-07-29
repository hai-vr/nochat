using JetBrains.Annotations;
using UdonSharp;
using UnityEngine;
using VRC.Udon.Common.Enums;

#if NOCHAT_USE_OWN_NAMESPACE
namespace NochatScript
#else
// FIXME: Unsure if namespace needs to be the same to avoid breaking prefabs
namespace VRC.Udon
#endif
{
    // FIXME:
    // This uses the same UUID as the original UdonBehaviour.
    // This is because the SendCustomEvent calls used by UI elements fail to trigger otherwise.
    // Don't know if there's a better way to fix this yet.
    public class UdonBehaviour : MonoBehaviour
    {
        [PublicAPI] public NochatPublicVarHolder publicVariables => GetNochatBehaviour().publicVariables;

        public void SendCustomEvent(string eventName)
        {
            GetNochatBehaviour().SendCustomEvent(eventName);
        }

        public void SendCustomEventDelayedSeconds(string eventName, float delaySeconds)
        {
            GetNochatBehaviour().SendCustomEventDelayedSeconds(eventName, delaySeconds);
        }

        public void SendCustomEventDelayedSeconds(string eventName, float delaySeconds, EventTiming timing)
        {
            GetNochatBehaviour().SendCustomEventDelayedSeconds(eventName, delaySeconds, timing);
        }
        
        public void SendCustomEventDelayedFrames(string eventName, int frameCount)
        {
            GetNochatBehaviour().SendCustomEventDelayedFrames(eventName, frameCount);
        }
        
        public void SendCustomEventDelayedFrames(string eventName, int frameCount, EventTiming timing)
        {
            GetNochatBehaviour().SendCustomEventDelayedFrames(eventName, frameCount, timing);
        }
        
        public void SetProgramVariable(string programVariableName, object value)
        {
            GetNochatBehaviour().SetProgramVariable(programVariableName, value);
        }
        
        public object GetProgramVariable(string programVariableName)
        {
            return GetNochatBehaviour().GetProgramVariable(programVariableName);
        }

        private UdonSharpBehaviour GetNochatBehaviour()
        {
            // FIXME: How do we get the exact NochatBehaviour associated with this?
            return GetComponent<UdonSharpBehaviour>();
        }
    }
}
