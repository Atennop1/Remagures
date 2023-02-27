using Remagures.View.QuestSystem;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class QuestSlotViewFactory : MonoBehaviour, IQuestSlotViewFactory
    {
        [SerializeField] private QuestSlotView _questSlotViewPrefab;
        
        public QuestSlotView Create(Transform parent)
            => Instantiate(_questSlotViewPrefab, parent.transform);
    }
}