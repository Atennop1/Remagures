using System.Linq;
using Remagures.Model.QuestSystem;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class QuestFactory : MonoBehaviour
    {
        [field: SerializeField] public int QuestID { get; private set; }
        [SerializeField] private QuestData _questData;
        [SerializeField] private GoalFactory[] _goalFactories;

        private IQuest _builtQuest;
        
        public IQuest Create()
        {
            if (_builtQuest != null)
                return _builtQuest;
            
            var questData = new Remagures.Model.QuestSystem.QuestData(_questData.Name, _questData.Description, new SerializableSprite(_questData.Sprite.texture));
            _builtQuest = new Quest(_goalFactories.Select(factory => factory.Create()).ToList(), questData);
            return _builtQuest;
        }
    }
}