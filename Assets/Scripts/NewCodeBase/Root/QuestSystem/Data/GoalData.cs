using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "GoalData", menuName = "QuestSystem/GoalData")]
    public sealed class GoalData : ScriptableObject
    {
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int RequiredPointsCount { get; private set; }
    }
}