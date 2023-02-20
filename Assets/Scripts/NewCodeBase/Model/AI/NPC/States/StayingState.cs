using System;
using System.Threading;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace Remagures.Model.AI.NPC
{
    public sealed class StayingState : IState
    {
        private readonly NPCMovementStatesChanger _movementStatesChanger;
        private readonly IRandomNPCMover _randomNpcMover;
        private readonly RandomMovementData _randomMovementData;
        
        private CancellationTokenSource _cancellationTokenSource;

        public StayingState(NPCMovementStatesChanger movementStatesChanger, IRandomNPCMover randomNpcMover, RandomMovementData randomMovementData)
        {
            _movementStatesChanger = movementStatesChanger ?? throw new ArgumentNullException(nameof(movementStatesChanger));
            _randomNpcMover = randomNpcMover ?? throw new ArgumentNullException(nameof(randomNpcMover));
            _randomMovementData = randomMovementData;
        }

        public void OnEnter()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _randomNpcMover.StopMoving();
            
            Task.Run(async () =>
            {
                await Task.Delay((int)(Random.Range(_randomMovementData.MinWaitTime, _randomMovementData.MaxWaitTime) * 1000));
                
                if (!_cancellationTokenSource.IsCancellationRequested)
                    _movementStatesChanger.StartMoving();
            });
        }

        public void OnExit()
            => _cancellationTokenSource.Cancel();

        public void Update() { }
    }
}