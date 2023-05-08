using Remagures.Tools;

namespace Remagures.Model.DialogSystem
{
    public interface IDialogSpeakerData
    {
        string SpeakerName { get; }
        SerializableSprite SpeakerSprite { get; }
        DialogLayoutType LayoutType { get; }
    }
}