using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Tools
{
    [CreateAssetMenu(fileName = "Scene", menuName = "Create/Scene Data")]
    public sealed class SceneData : ScriptableObject, ISerializationCallbackReceiver
    {
#if UNITY_EDITOR

        [SerializeField, Required] private SceneAsset _scene;
#endif
        public string Name { get; private set; }
        public string Path { get; private set; }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (_scene == null) 
                return;
            
            Path = AssetDatabase.GetAssetPath(_scene);
            Name = _scene.name;
#endif
        }

        [Button("Validate", ButtonSizes.Large, ButtonStyle.CompactBox), GUIColor(1, 1, 1)]
        public void Validate()
        {
            var existsInBuildingSettings = false;

            for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (SceneUtility.GetScenePathByBuildIndex(i).Contains(Name))
                    existsInBuildingSettings = true;
            }

            if (existsInBuildingSettings) Debug.Log("Successfully validated!");
            else Debug.LogError("Scene doesn't exist in building settings!");
        }
        
        public void OnAfterDeserialize() { }
    }
}