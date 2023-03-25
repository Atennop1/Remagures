using UnityEngine;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
    public abstract class ParentTask : Task
    {
        [SerializeField]
        protected List<Task> children;
        public List<Task> Children { get { return children; } private set { children = value; } }

        // The maximum number of children a parent task can have. Will usually be 1 or int.MaxValue
        public virtual int MaxChildren() { return int.MaxValue; }

        // Boolean value to determine if the current task is a parallel task
        public virtual bool CanRunParallelChildren() { return false; }

        // The index of the currently active child
        public virtual int CurrentChildIndex() { return 0; }

        // Boolean value to determine if the current task can initially execute
        public virtual bool CanExecute() { return true; }

        // Apply a decorator to the executed status
        public virtual TaskStatus Decorate(TaskStatus status) { return status; }

        // Boolean value to determine if the current task is reevaluated every frame
        public virtual bool CanReevaluate() { return false; }

        // Notifies the parent task that the child has been executed and has a status of childStatus
        public virtual void OnChildExecuted(TaskStatus childStatus) { }

        // Notifies the parent task that the child at index childIndex has been executed and has a status of childStatus
        public virtual void OnChildExecuted(int childIndex, TaskStatus childStatus) { }

        // Notifies the task that the child has started to run
        public virtual void OnChildStarted() { }

        // Notifies the parallel task that the child at index childIndex has started to run
        public virtual void OnChildStarted(int childIndex) { }

        // Some parent tasks need to be able to override the status, such as parallel tasks
        public virtual TaskStatus OverrideStatus(TaskStatus status) { return status; }

        // The interrupt node will override the status if it has been interrupted.
        public virtual TaskStatus OverrideStatus() { return TaskStatus.Running; }

        // Notifies the composite task that an conditional abort has been triggered and the child index should reset
        public virtual void OnConditionalAbort(int childIndex) { }

        // The utility select task will need to know the tasks utility in order to determine the execution order
        public override float GetUtility()
        {
            float utility = 0;
            if (children != null) {
                for (int i = 0; i < children.Count; ++i) {
                    if (children[i] != null && !children[i].Disabled) {
                        utility += children[i].GetUtility();
                    }
                }
            }
            return utility;
        }

        // Support OnDrawGizmos within the task
        public override void OnDrawGizmos()
        {
            if (children != null) {
                for (int i = 0; i < children.Count; ++i) {
                    if (children[i] != null && !children[i].Disabled) {
                        children[i].OnDrawGizmos();
                    }
                }
            }
        }

        public void AddChild(Task child, int index)
        {
            if (children == null) {
                children = new List<Task>();
            }
            children.Insert(index, child);
        }

        // replace the child task if the index already exists, otherwise add
        public void ReplaceAddChild(Task child, int index)
        {
            if (children != null && index < children.Count) {
                children[index] = child;
            } else {
                AddChild(child, index);
            }
        }
    }
}