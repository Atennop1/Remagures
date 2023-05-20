using Remagures.Model.QuestSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.QuestSystem
{
    public sealed class QuestSlotView : MonoBehaviour, IQuestSlotView
    {
        [field: SerializeField] public Button Button { get; private set; }
        
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Image _image;

        public void Display(IQuest quest)
        {
            _nameText.text = quest.Data.Name;
            _descriptionText.text = quest.Data.Description;
            _image.sprite = quest.Data.Sprite.Get();
        }
    }
}
