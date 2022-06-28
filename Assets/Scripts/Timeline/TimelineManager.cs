using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public static bool IsPlaying { get; private set; }
    [SerializeField] private PlayableDirector _director;

    private void Update()
    {
        if (_director.state == PlayState.Playing)
            IsPlaying = true;
        else
            IsPlaying = false;
    }
}
