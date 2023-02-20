using System;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class PhysicsRandomNPCMover : MonoBehaviour
    {
        private IRandomNPCMover _randomNpcMover;

        public void Construct(IRandomNPCMover randomNpcMover)
            => _randomNpcMover = randomNpcMover ?? throw new ArgumentNullException(nameof(randomNpcMover));
        
        private void OnCollisionEnter2D()
            => _randomNpcMover.ChooseDifferentDirection();
    }
}