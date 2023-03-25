﻿#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
    [TaskCategory("Basic/Timeline")]
    [TaskDescription("Is the timeline currently paused?")]
    public class IsPaused : Conditional
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;

        private PlayableDirector playableDirector;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                playableDirector = currentGameObject.GetComponent<PlayableDirector>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (playableDirector == null) {
                Debug.LogWarning("PlayableDirector is null");
                return TaskStatus.Failure;
            }

            return playableDirector.state == PlayState.Paused ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}
#endif