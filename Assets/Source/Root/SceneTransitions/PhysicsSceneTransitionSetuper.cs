using Remagures.Model.SceneTransition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsSceneTransitionSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private ISceneTransitionFactory _sceneTransitionFactory;
        [SerializeField] private PhysicsSceneTransition _physicsSceneTransition;

        private void Awake()
            => _physicsSceneTransition.Construct(_sceneTransitionFactory.Create());
    }
}