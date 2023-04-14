using Remagures.Factories;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class SceneTransitionsEntryPoint : SerializedMonoBehaviour
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