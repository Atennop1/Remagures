using Remagures.View.QuestSystem;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IQuestSlotViewFactory
    {
        QuestSlotView Create(Transform parent);
    }
}