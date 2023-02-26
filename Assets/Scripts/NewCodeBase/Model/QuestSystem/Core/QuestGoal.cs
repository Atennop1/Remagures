using System;
using Remagures.Root;

namespace Remagures.Model.QuestSystem
{
    public sealed class QuestGoal : IQuestGoal, IUpdatable, ILateUpdatable
    {
        public string Description { get; }
        public IProgress Progress { get; }

        public bool IsCompleted { get; private set; }
        public bool HasCompleted { get; private set; }
        
        public QuestGoal(string description, IProgress progress)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Progress = progress ?? throw new ArgumentNullException(nameof(progress));
        }

        public void LateUpdate()
            => HasCompleted = false;
        
        public void Update()
        {
            if (IsCompleted || Progress.CurrentPoints < Progress.RequiredPoints)
                return;
            
            IsCompleted = true;
            HasCompleted = true;
        }
    }
}