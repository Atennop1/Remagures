﻿using Remagures.Model.QuestSystem.QuestListeners;
using UnityEngine;

namespace Remagures.Root.QuestListenersFactories
{
    public sealed class DeleteFromQuestsListListenerFactory : MonoBehaviour
    {
        [SerializeField] private QuestFactory _questFactory;
        [SerializeField] private QuestsListFactory _questsListFactory;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();

        private void Awake()
        {
            var listener = new DeleteFromQuestsListListener(_questFactory.Create(), _questsListFactory.Create());
            _systemUpdate.Add(listener);
        }
    }
}