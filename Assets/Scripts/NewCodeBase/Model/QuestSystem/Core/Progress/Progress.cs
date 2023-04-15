using Remagures.Tools;

namespace Remagures.Model.QuestSystem
{
    public sealed class Progress : IProgress
    {
        public int CurrentPoints { get; private set; }
        public int RequiredPoints { get; }

        public Progress(int requiredPoints)
            => RequiredPoints = requiredPoints.ThrowExceptionIfLessOrEqualsZero();

        public void AddPoints(int amount)
        {
            if (CurrentPoints + amount.ThrowExceptionIfLessOrEqualsZero() > RequiredPoints)
                return;
            
            CurrentPoints += amount;
        }
    }
}