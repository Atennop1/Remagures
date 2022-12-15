using Remagures.Dialogs.Model.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Dialogs.Model.Builders
{
    public class SpeakerInfoBuilder : SerializedMonoBehaviour
    {
        [SerializeField] private string _speakerName;
        [SerializeField] private Sprite _speakerSprite;
        [SerializeField] private DialogLayoutType _layoutType;
        
        public DialogSpeakerInfo BuiltInfo { get; private set; }

        public DialogSpeakerInfo Build()
        {
            BuiltInfo = new DialogSpeakerInfo(_speakerName, _speakerSprite, _layoutType);
            return BuiltInfo;
        }
    }
}