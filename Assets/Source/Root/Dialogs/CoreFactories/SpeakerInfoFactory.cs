using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class SpeakerInfoFactory : SerializedMonoBehaviour
    {
        [SerializeField] private string _speakerName;
        [SerializeField] private Sprite _speakerSprite;
        [SerializeField] private DialogLayoutType _layoutType;
        private DialogSpeakerData _builtData;

        public DialogSpeakerData Create()
        {
            if (_builtData.SpeakerName != null)
                return _builtData;
            
            _builtData = new DialogSpeakerData(_speakerName, _speakerSprite, _layoutType);
            return _builtData;
        }
    }
}