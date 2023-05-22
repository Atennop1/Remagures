using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Model.MapSystem
{
    public sealed class MapEntryPoint : SerializedMonoBehaviour
    {
        private void Awake()
        {
            var currentScene = SceneManager.GetActiveScene();
            var saveStorage = new BinaryStorage<string>(new Path("CurrentScene"));
            saveStorage.Save(currentScene.name);
            PlayerPrefs.SetInt("Visited" + currentScene.path, 1);
        }
    }
}