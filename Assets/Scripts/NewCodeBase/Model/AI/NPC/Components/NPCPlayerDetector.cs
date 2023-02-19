using Remagures.Root;

namespace Remagures.Model.AI.NPC
{
    public class NPCPlayerDetector : ILateUpdatable
    {
        public bool HasPlayerDetected { get; private set; }
        public bool HasPlayerUndetected { get; private set; }
        
        public void LateUpdate()
        {
            HasPlayerDetected = false;
            HasPlayerUndetected = false;
        }

        public void Detect()
            => HasPlayerDetected = true;

        public void Undetect()
            => HasPlayerUndetected = true;
    }
}