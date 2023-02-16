using System;
using Remagures.Root;
using UnityEngine;

namespace Remagures.Model.SceneTransition
{
    public class PhysicsSceneTransition : MonoBehaviour
    {
        private ISceneTransition _sceneTransition;
        private ISystemUpdate _systemUpdate;

        public void Construct(ISceneTransition sceneTransition)
        {
            _systemUpdate = new SystemUpdate();
            _sceneTransition = sceneTransition ?? throw new ArgumentNullException(nameof(sceneTransition));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player _) || collision.isTrigger) 
                return;

            _sceneTransition.Activate();
        }
        
        private void FixedUpdate() 
            => _systemUpdate?.UpdateAll();
    }
}