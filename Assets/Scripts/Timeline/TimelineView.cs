using UnityEngine;
using UnityEngine.Playables;

public class TimelineView : MonoBehaviour
{
    [field: SerializeField] public PlayableDirector Director { get; private set; }
    
    public bool IsPlaying { get; private set; }
    public static TimelineView Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (Director.state == PlayState.Playing)
            IsPlaying = true;
        else
            IsPlaying = false;
    }
}
