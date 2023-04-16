using Remagures.Model.QuestSystem;
using Remagures.View.QuestSystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class QuestPopupsFactory : MonoBehaviour
    {
        [SerializeField] private QuestPopupView popupView;
        private IQuestPopups _builtPopups;
        
        public IQuestPopups Create()
        {
            if (_builtPopups != null)
                return _builtPopups;
            
            _builtPopups = new QuestPopups(popupView);
            return _builtPopups;
        }
    }
}