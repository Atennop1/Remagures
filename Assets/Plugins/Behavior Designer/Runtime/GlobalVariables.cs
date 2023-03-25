using UnityEngine;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime
{
    public class GlobalVariables : ScriptableObject, IVariableSource
    {
        private static GlobalVariables instance = null;
        public static GlobalVariables Instance
        {
            get
            {
                if (instance == null) {
                    instance = Resources.Load("BehaviorDesignerGlobalVariables", typeof(GlobalVariables)) as GlobalVariables;
                    if (instance != null) {
                        instance.CheckForSerialization(false);
#if ENABLE_MULTIPLAYER
                        if (UnityEngine.Networking.NetworkServer.active) {
                            // Loop through the global variables. If any are synchronized on the network then the GlobalVariableNetworkSync component needs to exist.
                            var variables = instance.GetAllVariables();
                            var networkSync = false;
                            for (int i = 0; i < variables.Count; ++i) {
                                if (variables[i].NetworkSync) {
                                    networkSync = true;
                                }
                            }
                            if (networkSync) {
                                if (GlobalVariablesNetworkSync.instance == null) {
                                    Debug.LogError("Error: No GlobalVariablesNetworkSync singleton exists. Please create one ahead of time with a NetworkIdentity. Due to a Unity networking bug " +
                                                    "one cannot be created automatically.");
                                }
                                // As soon as the invalid asset id bug is fixed:
                                /* 
                                var syncGameObject = new GameObject("Global Variable Network Sync");
                                syncGameObject.AddComponent<UnityEngine.Networking.NetworkIdentity>();
                                syncGameObject.AddComponent<GlobalVariablesNetworkSync>();
                                UnityEngine.Networking.NetworkServer.Spawn(syncGameObject);
                                */
                            }
                        }
#endif
                    }
                }
                return instance;
            }
        }

        [SerializeField]
        private List<SharedVariable> mVariables = null;
        public List<SharedVariable> Variables { get { return mVariables; } set { mVariables = value; UpdateVariablesIndex(); } }
        private Dictionary<string, int> mSharedVariableIndex;

        [SerializeField]
        private VariableSerializationData mVariableData;
        public VariableSerializationData VariableData { get { return mVariableData; } set { mVariableData = value; } }

        [SerializeField]
        private string mVersion;
        public string Version { get { return mVersion; } set { mVersion = value; } }

        public void CheckForSerialization(bool force)
        {
            // Only deserialize if there isn't any behavior tree elements, as in there is not a entry task or any variables
            // When Unity serializes mVariables to a prefab it is going to serialze the element count but not the actual objects because those object derive from ScriptableObject.
            // If there are elements within the array check to make sure the first element isn't null. The first element will be null when converting to a prefab.
            if (force || mVariables == null || (mVariables.Count > 0 && mVariables[0] == null)) {
                if (VariableData != null && !string.IsNullOrEmpty(VariableData.JSONSerialization)) {
                    JSONDeserialization.Load(VariableData.JSONSerialization, this, mVersion);
                } else { // binary serialization
                    BinaryDeserialization.Load(this, mVersion);
                }
            }
        }

        public SharedVariable GetVariable(string name)
        {
            if (name == null) {
                return null;
            }
            CheckForSerialization(false);
            if (mVariables != null) {
                if (mSharedVariableIndex == null || (mSharedVariableIndex.Count != mVariables.Count)) {
                    UpdateVariablesIndex();
                }
                int index;
                if (mSharedVariableIndex.TryGetValue(name, out index)) {
                    return mVariables[index];
                }
            }
            return null;
        }

        public List<SharedVariable> GetAllVariables()
        {
            CheckForSerialization(false);
            return mVariables;
        }

        public void SetVariable(string name, SharedVariable sharedVariable)
        {
            CheckForSerialization(false);
            if (mVariables == null) {
                mVariables = new List<SharedVariable>();
                // When the game starts the index is null because dictionaries are not serialized by Unity
            } else if (mSharedVariableIndex == null) {
                UpdateVariablesIndex();
            }

            sharedVariable.Name = name;
            int index;
            if (mSharedVariableIndex != null && mSharedVariableIndex.TryGetValue(name, out index)) {
                var origSharedVariable = mVariables[index];
                if (!origSharedVariable.GetType().Equals(typeof(SharedVariable)) && !origSharedVariable.GetType().Equals(sharedVariable.GetType())) {
                    Debug.LogError(string.Format("Error: Unable to set SharedVariable {0} - the variable type {1} does not match the existing type {2}",
                                                    name, origSharedVariable.GetType(), sharedVariable.GetType()));
                } else {
                    origSharedVariable.SetValue(sharedVariable.GetValue());
                }
            } else {
                mVariables.Add(sharedVariable);
                UpdateVariablesIndex();
            }
        }

        public void SetVariableValue(string name, object value)
        {
            var sharedVariable = GetVariable(name);
            if (sharedVariable != null) {
                sharedVariable.SetValue(value);
                sharedVariable.ValueChanged();
            }
        }

        public void UpdateVariableName(SharedVariable sharedVariable, string name)
        {
            CheckForSerialization(false);
            sharedVariable.Name = name;
            UpdateVariablesIndex();
        }

        public void SetAllVariables(List<SharedVariable> variables)
        {
            mVariables = variables;
        }

        private void UpdateVariablesIndex()
        {
            if (mVariables == null) {
                if (mSharedVariableIndex != null) {
                    mSharedVariableIndex = null;
                }
                return;
            }
            if (mSharedVariableIndex == null) {
                mSharedVariableIndex = new Dictionary<string, int>(mVariables.Count);
            } else {
                mSharedVariableIndex.Clear();
            }
            for (int i = 0; i < mVariables.Count; ++i) {
                if (mVariables[i] == null)
                    continue;
                mSharedVariableIndex.Add(mVariables[i].Name, i);
            }
        }
    }
}