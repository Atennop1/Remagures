using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "QuestSystem/QuestInfo")]
    public sealed class QuestData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}