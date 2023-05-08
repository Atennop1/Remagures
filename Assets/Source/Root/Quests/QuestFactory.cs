using System.Linq;
using Remagures.Model.QuestSystem;
using Remagures.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class QuestFactory : SerializedMonoBehaviour, IQuestFactory
    {
        [field: SerializeField] public int QuestID { get; private set; }
        [SerializeField] private QuestData _questData;
        [SerializeField] private IGoalFactory[] _goalFactories;

        private Quest _builtQuest;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();
        private readonly ILateSystemUpdate _lateSystemUpdate = new LateSystemUpdate();

        private void LateUpdate()
            => _lateSystemUpdate.UpdateAll();

        private void Update()
            => _systemUpdate.UpdateAll();
        
        public IQuest Create()
        {
            if (_builtQuest != null)
                return _builtQuest;
            
            var questData = new Remagures.Model.QuestSystem.QuestData(_questData.Name, _questData.Description, new SerializableSprite(_questData.Sprite.texture));
            _builtQuest = new Quest(_goalFactories.Select(factory => factory.Create()).ToList(), questData);
            _systemUpdate.Add(_builtQuest);
            _lateSystemUpdate.Add(_builtQuest);
            return _builtQuest;
        }
    }
}