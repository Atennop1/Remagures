using System.Collections.Generic;
using Remagures.Model.QuestSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class QuestsDatabaseFactory : SerializedMonoBehaviour
    {
        [SerializeField] private List<QuestFactory> _questFactories;
        private QuestsDatabase _builtDatabase;
        
        public QuestsDatabase Create()
        {
            if (_builtDatabase != null)
                return _builtDatabase;
            
            _builtDatabase = new QuestsDatabase(_questFactories);
            return _builtDatabase;
        }
    }
}