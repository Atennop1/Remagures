using Remagures.Root;

namespace Remagures.Model.AI.NPC
{
    public sealed class NPCMovementStatesChanger : ILateUpdatable
    {
        public bool HasStartedMoving { get; private set; }
        public bool HasEndedMoving { get; private set; }
        
        public void LateUpdate()
        {
            HasStartedMoving = false;
            HasEndedMoving = false;
        }
        
        public void StartMoving()
            => HasStartedMoving = true;

        public void EndMoving()
            => HasEndedMoving = true;
    }
}