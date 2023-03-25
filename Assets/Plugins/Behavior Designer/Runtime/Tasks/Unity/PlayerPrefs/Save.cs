using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
    [TaskCategory("Basic/PlayerPrefs")]
    [TaskDescription("Saves the PlayerPrefs.")]
    public class Save : Action
    {
        public override TaskStatus OnUpdate()
        {
            PlayerPrefs.Save();

            return TaskStatus.Success;
        }
    }
}