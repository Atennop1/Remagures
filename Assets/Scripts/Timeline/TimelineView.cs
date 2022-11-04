using UnityEngine;
using UnityEngine.Playables;

namespace Remagures.Timeline
{
    public class TimelineView : MonoBehaviour
    {
        [field: SerializeField] public PlayableDirector Director { get; private set; }
    
        public bool IsPlaying { get; private set; }
        public bool CanContinue { get; private set; }
    
        public static TimelineView Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        private void FixedUpdate()
        {
            IsPlaying = Director.state == PlayState.Playing;
        }

        public void SetCanContinue(bool value)
        {
            if (value && IsPlaying)
                CanContinue = true;
        
            if (!value || !IsPlaying)
                CanContinue = value;
        }
    }
}
