using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class SpeakerInfoFactory : SerializedMonoBehaviour
    {
        [SerializeField] private string _speakerName;
        [SerializeField] private Sprite _speakerSprite;
        [SerializeField] private DialogLayoutType _layoutType;

        public DialogSpeakerData _builtData;

        public DialogSpeakerData Build()
        {
            if (_builtData.SpeakerName != null)
                return _builtData;
            
            _builtData = new DialogSpeakerData(_speakerName, _speakerSprite, _layoutType);
            return _builtData;
        }
    }
}