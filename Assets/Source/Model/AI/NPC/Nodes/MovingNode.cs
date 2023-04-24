using System.Threading;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Remagures.Model.AI.NPC
{
    public sealed class MovingNode : Action
    {
        public SharedFloat SharedMovingTime;
        public SharedVector3 SharedMoveDirection;
        
        public SharedBool SharedIsMoving;
        public SharedINPCMovement SharedNPCMovement;
        public SharedTransform SharedNPCTransform;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        public override async void OnAwake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            SharedNPCMovement.Value.Move(SharedNPCTransform.Value.position + SharedMoveDirection.Value * SharedMovingTime.Value / 2);
            await StayingTask();
        }

        public override void OnEnd()
            => _cancellationTokenSource.Cancel();

        private async Task StayingTask()
        {
            await Task.Delay((int)(SharedMovingTime.Value * 1000));

            if (!_cancellationTokenSource.IsCancellationRequested)
                SharedIsMoving.Value = false;
        }
    }
}