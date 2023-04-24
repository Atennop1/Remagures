using System.Threading;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Remagures.Model.AI.NPC
{
    public sealed class StayingNode : Action
    {
        public SharedBool SharedIsMoving;
        public SharedFloat SharedStayingTime;
        public SharedINPCMovement SharedNPCMovement;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        public override async void OnAwake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            SharedNPCMovement.Value.StopMoving();
            await StayingTask();
        }

        public override void OnEnd()
            => _cancellationTokenSource.Cancel();

        private async Task StayingTask()
        {
            await Task.Delay((int)(SharedStayingTime.Value * 1000));

            if (!_cancellationTokenSource.IsCancellationRequested)
                SharedIsMoving.Value = true;
        }
    }
}