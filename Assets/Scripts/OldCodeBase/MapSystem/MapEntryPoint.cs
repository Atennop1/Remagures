using Remagures.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.MapSystem
{
    public class MapEntryPoint : MonoBehaviour
    {
        [SerializeField] private StringValue _currentScene;
        
        private void Awake()
        {
            _currentScene.Value = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt("Visited" + SceneManager.GetActiveScene().path, 1);
        }
    }
}