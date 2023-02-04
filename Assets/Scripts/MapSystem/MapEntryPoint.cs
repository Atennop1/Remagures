using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.MapSystem
{
    public class MapEntryPoint : MonoBehaviour
    {
        private void Awake()
            => PlayerPrefs.SetInt("Visited" + SceneManager.GetActiveScene().path, 1);
    }
}