using Remagures.SO;
using SaveSystem;
using SaveSystem.Paths;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Model.MapSystem
{
    public class MapEntryPoint : MonoBehaviour
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