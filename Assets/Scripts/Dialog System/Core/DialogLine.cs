using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogLine
{
    [field: SerializeField, TextArea] public string Line { get; private set; }

    [field: SerializeField, Space] public Sprite SpeakerSprite { get; private set; }
    [field: SerializeField] public string SpeakerName { get; private set; }
    [field: SerializeField] public DialogLayoutType LayoutType { get; private set; }

    [field: SerializeField] public UnityEvent OnLineEnded { get; private set; }
}

public enum DialogLayoutType
{
    Left,
    Right
}
