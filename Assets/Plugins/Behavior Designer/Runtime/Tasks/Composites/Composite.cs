namespace BehaviorDesigner.Runtime.Tasks
{
    public enum AbortType { None, Self, LowerPriority, Both }

    // Composite tasks are parent tasks that hold a list of child tasks. For example, one composite task may loop through the child tasks sequentially while another
    // composite task may run all of its child tasks at once. The return status of the composite tasks depends on its children. 
    public abstract class Composite : ParentTask
    {
        [Tooltip("Specifies the type of conditional abort. More information is located at https://www.opsive.com/support/documentation/behavior-designer/conditional-aborts/.")]
        [UnityEngine.SerializeField]
        protected AbortType abortType = AbortType.None;
        public AbortType AbortType { get { return abortType; } }

        // Notifies the parent task that a reevaluation has been requested. The parent task can prevent the reevaluation by returning false.
        public virtual bool OnReevaluationStarted() { return false; }

        // Notifies the parent task that the reevaluation has ended.
        public virtual void OnReevaluationEnded(TaskStatus status) { }
    }
}