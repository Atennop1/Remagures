using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Root;

namespace Remagures.Model.QuestSystem
{
    public sealed class QuestsDatabase
    {
        private readonly List<QuestFactory> _factories;

        public QuestsDatabase(List<QuestFactory> factories) 
            => _factories = factories ?? throw new ArgumentNullException(nameof(factories));

        public int GetQuestID(IQuest quest)
        {
            if (_factories.All(factory => factory.Create() != quest))
                throw new InvalidOperationException("Unknown item");

            return _factories.Find(factory => factory.Create() == quest).QuestID;
        }

        public IQuest GetByID(int id)
        {
            if (_factories.All(factory => factory.QuestID != id))
                throw new InvalidOperationException("Unknown id");

            return _factories.Find(factory => factory.QuestID == id).Create();
        }
    }
}