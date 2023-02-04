using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.DialogSystem
{
    public class SpeakerInfoBuilder : SerializedMonoBehaviour
    {
        [SerializeField] private string _speakerName;
        [SerializeField] private Sprite _speakerSprite;
        [SerializeField] private DialogLayoutType _layoutType;
        
        public DialogSpeakerData BuiltData { get; private set; }

        public DialogSpeakerData Build()
        {
            BuiltData = new DialogSpeakerData(_speakerName, _speakerSprite, _layoutType);
            return BuiltData;
        }
    }
}