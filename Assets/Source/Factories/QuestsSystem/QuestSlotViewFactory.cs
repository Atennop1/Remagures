using Remagures.View.QuestSystem;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class QuestSlotViewFactory : MonoBehaviour, IQuestSlotViewFactory
    {
        [SerializeField] private GameObject _questSlotViewPrefab;
        
        public IQuestSlotView Create(Transform parent)
            => Instantiate(_questSlotViewPrefab, parent.transform).GetComponent<IQuestSlotView>();
    }
}