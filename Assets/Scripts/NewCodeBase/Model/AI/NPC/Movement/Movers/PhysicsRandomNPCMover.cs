using System;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class PhysicsRandomNPCMover : MonoBehaviour
    {
        private RandomNPCMover _randomNpcMover;

        public void Construct(RandomNPCMover randomNpcMover)
            => _randomNpcMover = randomNpcMover ?? throw new ArgumentNullException(nameof(randomNpcMover));
        
        private void OnCollisionEnter2D()
            => _randomNpcMover.ChooseDifferentDirection();
    }
}