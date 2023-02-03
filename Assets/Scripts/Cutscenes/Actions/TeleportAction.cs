using System;
using UnityEngine;

namespace Remagures.Cutscenes
{
    public class TeleportAction : ICutsceneAction
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }

        private readonly Transform _transform;
        private readonly Vector2 _positionToTeleport;
        
        public TeleportAction(Transform transform, Vector2 positionToTeleport)
        {
            _transform = transform ? transform : throw new ArgumentNullException(nameof(transform));
            _positionToTeleport = positionToTeleport;
        }
        
        public void Start()
        {
            IsStarted = true;
            _transform.position = _positionToTeleport;
            IsFinished = true;
        }
        
        public void Finish() { }
    }
}