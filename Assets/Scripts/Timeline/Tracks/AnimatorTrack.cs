using Remagures.Timeline.Clips;
using UnityEngine;
using UnityEngine.Timeline;

namespace Remagures.Timeline.Tracks
{
    [TrackColor(0, 132f/255f, 1)]
    [TrackBindingType(typeof(Animator))]
    [TrackClipType(typeof(AnimatorClip))]
    public class AnimatorTrack : TrackAsset { }
}
