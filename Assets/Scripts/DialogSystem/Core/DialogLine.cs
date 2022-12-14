using Remagures.Dialogs;
using Remagures.Dialogs.Model;
using Remagures.Dialogs.Model.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Remagures.DialogSystem.Core
{
    [System.Serializable]
    public struct DialogLine
    {
        [field: SerializeField, TextArea] public string Line { get; private set; }

        [field: SerializeField, Space] public Sprite SpeakerSprite { get; private set; }
        [field: SerializeField] public string SpeakerName { get; private set; }
        [field: SerializeField] public DialogLayoutType LayoutType { get; private set; }

        [field: SerializeField] public UnityEvent OnLineEnded { get; private set; }
    }
}
