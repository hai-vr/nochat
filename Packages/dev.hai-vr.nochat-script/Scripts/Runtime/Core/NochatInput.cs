using UnityEngine;

namespace NochatScript.Core
{
    public class NochatInput
    {
        public static float LeftTrigger { get; internal set; }
        public static float RightTrigger { get; internal set; }
        public static float LeftGrip { get; internal set; }
        public static float RightGrip { get; internal set; }
        public static float LeftAxisX { get; internal set; }
        public static float LeftAxisY { get; internal set; }
        public static float RightAxisX { get; internal set; }
        public static float RightAxisY { get; internal set; }

        public static float GetAxisRaw(string name)
        {
            switch (name)
            {
                case "Oculus_CrossPlatform_PrimaryIndexTrigger": return LeftTrigger;
                case "Oculus_CrossPlatform_SecondaryIndexTrigger": return RightTrigger;
                case "Oculus_CrossPlatform_PrimaryHandTrigger": return LeftGrip;
                case "Oculus_CrossPlatform_SecondaryHandTrigger": return RightGrip;
                case "Oculus_CrossPlatform_PrimaryThumbstickHorizontal": return LeftAxisX;
                case "Oculus_CrossPlatform_PrimaryThumbstickVertical": return LeftAxisY;
                case "Oculus_CrossPlatform_SecondaryThumbstickHorizontal": return RightAxisX;
                case "Oculus_CrossPlatform_SecondaryThumbstickVertical": return RightAxisY;
            }
            return Input.GetAxisRaw(name);
        }

        public static bool GetKey(KeyCode key)
        {
            return Input.GetKey(key);
        }

        public static bool GetKeyDown(KeyCode key)
        {
            return Input.GetKeyDown(key);
        }

        public static bool GetMouseButtonDown(int button)
        {
            return Input.GetMouseButtonDown(button);
        }

        public static bool GetMouseButtonUp(int button)
        {
            return Input.GetMouseButtonUp(button);
        }
    }
}