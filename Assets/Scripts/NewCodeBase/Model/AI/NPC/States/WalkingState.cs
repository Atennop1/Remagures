using System;
using System.Threading;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace Remagures.Model.AI.NPC
{
    public class WalkingState : IState
    {
        private readonly NPCMovementStatesChanger _movementStatesChanger;
        private readonly IRandomNPCMover _randomNpcMover;
        private readonly RandomMovementData _randomMovementData;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        public WalkingState(NPCMovementStatesChanger movementStatesChanger, IRandomNPCMover randomNpcMover, RandomMovementData randomMovementData)
        {
            _movementStatesChanger = movementStatesChanger ?? throw new ArgumentNullException(nameof(movementStatesChanger));
            _randomNpcMover = randomNpcMover ?? throw new ArgumentNullException(nameof(randomNpcMover));
            _randomMovementData = randomMovementData;
        }
        
        public void OnEnter()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _randomNpcMover.ChooseDifferentDirection();
            _randomNpcMover.Move();

            Task.Run(async () =>
            {
                await Task.Delay((int)(Random.Range(_randomMovementData.MinMoveTime, _randomMovementData.MaxMoveTime) * 1000));
                
                if (!_cancellationTokenSource.IsCancellationRequested)
                    _movementStatesChanger.EndMoving();
            });
        }

        public void OnExit()
            => _cancellationTokenSource.Cancel();
        
        public void Update() { }
    }
}