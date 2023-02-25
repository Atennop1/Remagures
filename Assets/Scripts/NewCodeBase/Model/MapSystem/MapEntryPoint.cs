using Remagures.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Model.MapSystem
{
    public class MapEntryPoint : MonoBehaviour //TODO remove this when adding root layer
    {
        [SerializeField] private StringValue _currentScene;
        
        private void Awake()
        {
            var currentScene = SceneManager.GetActiveScene();
            _currentScene.Value = currentScene.name;
            PlayerPrefs.SetInt("Visited" + currentScene.path, 1);
        }
    }
}