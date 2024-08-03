using UdonSharp;
using UnityEngine;

namespace NochatScript.Core
{
    [DefaultExecutionOrder(1_000_000)]
    public class NochatPostLateUpdate : MonoBehaviour
    {
        private void LateUpdate()
        {
            // FIXME: STUB, Slow
            foreach (var behaviours in GameObject.FindObjectsByType<UdonSharpBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                behaviours.PostLateUpdate();
            }
        }
    }
}