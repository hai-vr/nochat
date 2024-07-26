#if NOCHAT_INTERNAL_EXTRA
using UnityEditor;
using UnityEngine;

namespace NochatScript.Editor.Extra
{
    public class NochatExtra
    {
        [MenuItem("Tools/Remove MonoBehaviours with missing script")]
        public static void RemoveMissingScripts()
        {
            var active = Selection.activeGameObject;
            foreach (Transform t in active.GetComponentsInChildren<Transform>(true))
            {
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(t.gameObject);
            }
        }
    }
}
#endif