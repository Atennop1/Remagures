using Remagures.View.QuestSystem;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IQuestSlotViewFactory
    {
        IQuestSlotView Create(Transform parent);
    }
}