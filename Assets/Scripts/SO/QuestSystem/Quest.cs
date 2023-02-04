using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Remagures.SO
{
    [Serializable]
    public partial class Quest : ScriptableObject
    {
        public QuestData Information;
        public List<QuestGoal> Goals;

        [field: SerializeField] public bool Completed { get; private set; }

        public void Initialize()
        {
            Completed = false;

            foreach (var goal in Goals)
            {
                goal.Initialize();
                goal.GoalCompleted.AddListener(CheckGoals);
                goal.Evaluate();
            }
        }

        public QuestGoal GetActiveGoal()
        {
            if (Completed)
                throw new ArgumentException("Can't find active goal in completed quest!");
            
            return Goals[Goals.FindLastIndex(x => x.Completed) + 1];
        }

        public void Reset()
            => Completed = false;

        private void CheckGoals()
            =>Completed = Goals.All(g => g.Completed);
    }
}