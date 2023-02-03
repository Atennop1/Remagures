using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.DialogSystem
{
    [Serializable]
    public struct DialogSpeakerInfo
    {
        public string SpeakerName { get; }
        public SerializableSprite SpeakerSprite { get; }
        public DialogLayoutType LayoutType { get; }
        
        public DialogSpeakerInfo(string speakerName, Sprite speakerSprite, DialogLayoutType layoutType)
        {
            SpeakerName = speakerName ?? throw new ArgumentException("SpeakerName can't be null");
            SpeakerSprite = speakerSprite ? new SerializableSprite(speakerSprite.texture) : throw new ArgumentException("Sprite can't be null");
            LayoutType = layoutType;
        }
    }
}