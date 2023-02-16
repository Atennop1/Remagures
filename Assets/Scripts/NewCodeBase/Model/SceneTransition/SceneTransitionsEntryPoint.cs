using Remagures.Factories;
using UnityEngine;

namespace Remagures.Model.SceneTransition
{
    public class SceneTransitionsEntryPoint : MonoBehaviour
    {
        [SerializeField] private IGameObjectFactory _fadingFactory;

        private const int FADE_LIFETIME_IN_SECONDS = 1;
        
        private void Awake()
        {
            var fade = _fadingFactory.Create(Vector3.zero);
            Destroy(fade, FADE_LIFETIME_IN_SECONDS);
        }
    }
}