using UnityEngine;
using UnityEngine.Timeline;

[TrackColor(0f / 255f, 53f / 255f, 255f / 255f)]
[TrackBindingType(typeof(Animator))]
[TrackClipType(typeof(AssetClip))]
public class PlayerTrack : TrackAsset { }
