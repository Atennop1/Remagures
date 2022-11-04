using Remagures.DialogSystem.UI;
using Remagures.Timeline.Clips;
using UnityEngine.Timeline;

namespace Remagures.Timeline.Tracks
{
    [TrackColor(1, 208f / 255f, 77f / 255f)]
    [TrackBindingType(typeof(DialogTypeWriter))]
    [TrackClipType(typeof(DialogClip))]
    public class DialogTrack : TrackAsset { }
}
