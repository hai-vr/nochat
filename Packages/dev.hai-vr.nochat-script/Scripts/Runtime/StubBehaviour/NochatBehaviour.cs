using VRC.Udon;

namespace UdonSharp
{
    public class UdonSharpBehaviour : UdonBehaviour
    {
    }

    public class NochatPublicVarHolder
    {
        public void TryGetVariableValue<T>(string variableName, out T output)
        {
            output = default;
        }
    }
}