using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    // One use for this task is if you have an unit that plays a series of tasks to attack. You may want the unit to attack at different points within
    // the behavior tree, and you want that attack to always be the same. Instead of copying and pasting the same tasks over and over you can just use
    // an external behavior and then the tasks are always guaranteed to be the same. This example is demonstrated in the RTS sample project located at
    // https://www.opsive.com/integrations/?pid=803.
    [TaskDescription("Behavior Reference allows you to run another behavior tree within the current behavior tree.")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer/external-behavior-trees/")]
    [TaskIcon("BehaviorTreeReferenceIcon.png")]
    public abstract class BehaviorReference : Action
    {
        [Tooltip("External behavior array that this task should reference")]
        public ExternalBehavior[] externalBehaviors;
        [Tooltip("Any variables that should be set for the specific tree")]
        public SharedNamedVariable[] variables;
        [HideInInspector]
        public bool collapsed;
        
        // Returns the external behavior. Can be overridden.
        public virtual ExternalBehavior[] GetExternalBehaviors()
        {
            return externalBehaviors;
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            externalBehaviors = null;
        }
    }
}