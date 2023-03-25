#if ENABLE_MULTIPLAYER
using UnityEngine;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime
{
    public class GlobalVariablesNetworkSync : NetworkBehaviour
    {
        public static GlobalVariablesNetworkSync instance = null;

        public void Awake()
        {
            instance = this;
        }

        public static void DirtyVariable(SharedVariable variable)
        {
            // Only the server can mark variables as dirty.
            if (!NetworkServer.active) {
                return;
            }

            instance.DirtyVariableInternal(variable);
        }

        private void DirtyVariableInternal(SharedVariable variable)
        {

            // ClientRPC calls cannot send generic object parameters. In addition, ClientRPC methods cannot be overloaded (bug 697809)
            var variableValue = variable.GetValue();
            if (variableValue == null) {
                RpcDirtyVariableNull(variable.Name);
            } else {
                var variableType = variableValue.GetType();
                if (variableType == typeof(bool)) {
                    RpcDirtyVariableBool(variable.Name, (bool)variableValue);
                } else if (variableType == typeof(Color)) {
                    RpcDirtyVariableColor(variable.Name, (Color)variableValue);
                } else if (variableType == typeof(float)) {
                    RpcDirtyVariableFloat(variable.Name, (float)variableValue);
                } else if (variableType == typeof(GameObject)) {
                    RpcDirtyVariableGameObject(variable.Name, (GameObject)variableValue);
                } else if (variableType == typeof(int)) {
                    RpcDirtyVariableInt(variable.Name, (int)variableValue);
                } else if (variableType == typeof(Quaternion)) {
                    RpcDirtyVariableQuaternion(variable.Name, (Quaternion)variableValue);
                } else if (variableType == typeof(Rect)) {
                    RpcDirtyVariableRect(variable.Name, (Rect)variableValue);
                } else if (variableType == typeof(string)) {
                    RpcDirtyVariableString(variable.Name, (string)variableValue);
                } else if (variableType == typeof(Transform)) {
                    RpcDirtyVariableTransform(variable.Name, ((Transform)variableValue).gameObject);
                } else if (variableType == typeof(Vector2)) {
                    RpcDirtyVariableVector2(variable.Name, (Vector2)variableValue);
                } else if (variableType == typeof(Vector3)) {
                    RpcDirtyVariableVector3(variable.Name, (Vector3)variableValue);
                } else if (variableType == typeof(Vector4)) {
                    RpcDirtyVariableVector4(variable.Name, (Vector4)variableValue);
                } else {
                    Debug.LogError("Error: Unable to synchronize SharedVariable type " + variableType);
                }
            }

            RpcDirtyVariableInt(variable.Name, (int)variable.GetValue());
        }

        [ClientRpc]
        private void RpcDirtyVariableNull(string name)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(null);
        }

        [ClientRpc]
        private void RpcDirtyVariableBool(string name, bool value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableColor(string name, Color value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableFloat(string name, float value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableGameObject(string name, GameObject value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableInt(string name, int value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableQuaternion(string name, Quaternion value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableRect(string name, Rect value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableString(string name, string value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableTransform(string name, GameObject value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value.transform);
        }

        [ClientRpc]
        private void RpcDirtyVariableVector2(string name, Vector2 value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableVector3(string name, Vector3 value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableVector4(string name, Vector4 value)
        {
            GlobalVariables.Instance.GetVariable(name).SetValue(value);
        }
    }
}
#endif