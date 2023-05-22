using System;
using Remagures.Model.Character;
using Remagures.Root;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.SceneTransition
{
    public sealed class PhysicsSceneTransition : SerializedMonoBehaviour
    {
        private ISceneTransition _sceneTransition;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        public void Construct(ISceneTransition sceneTransition) 
            => _sceneTransition = sceneTransition ?? throw new ArgumentNullException(nameof(sceneTransition));

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out PhysicsCharacter _) || collision.isTrigger) 
                return;

            _sceneTransition.Activate();
        }
        
        private void FixedUpdate() 
            => _systemUpdate?.UpdateAll();
    }
}