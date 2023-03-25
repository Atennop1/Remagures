using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
#if !UNITY_EDITOR && NETFX_CORE
using System.Reflection;
#endif

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public abstract class ExternalBehavior : ScriptableObject, IBehavior
    {
        [SerializeField]
        private BehaviorSource mBehaviorSource;
        public BehaviorSource BehaviorSource { get { return mBehaviorSource; } set { mBehaviorSource = value; } }
        public BehaviorSource GetBehaviorSource() { return mBehaviorSource; }
        public void SetBehaviorSource(BehaviorSource behaviorSource) { mBehaviorSource = behaviorSource; }
        public Object GetObject() { return this; }
        public string GetOwnerName() { return name; }
        private bool mInitialized;
        public bool Initialized { get { return mInitialized; } }
        
        public void Init()
        {
            CheckForSerialization();
            mInitialized = true;
        }

        // Support blackboard variables:
        public SharedVariable GetVariable(string name)
        {
            CheckForSerialization();
            return mBehaviorSource.GetVariable(name);
        }

        public void SetVariable(string name, SharedVariable item)
        {
            CheckForSerialization();
            mBehaviorSource.SetVariable(name, item);
        }

        public void SetVariableValue(string name, object value)
        {
            var sharedVariable = GetVariable(name);
            if (sharedVariable != null) {
                sharedVariable.SetValue(value);
                sharedVariable.ValueChanged();
            }
        }

        public T FindTask<T>() where T : Task
        {
            CheckForSerialization();
            return FindTask<T>(mBehaviorSource.RootTask);
        }

        private T FindTask<T>(Task task) where T : Task
        {
            if (task.GetType().Equals(typeof(T))) {
                return (T)task;
            }

            ParentTask parentTask;
            if ((parentTask = task as ParentTask) != null) {
                if (parentTask.Children != null) {
                    for (int i = 0; i < parentTask.Children.Count; ++i) {
                        T foundTask = null;
                        if ((foundTask = FindTask<T>(parentTask.Children[i])) != null) {
                            return foundTask;
                        }
                    }
                }
            }

            return null;
        }

        public List<T> FindTasks<T>() where T : Task
        {
            CheckForSerialization();

            List<T> tasks = new List<T>();
            FindTasks<T>(mBehaviorSource.RootTask, ref tasks);
            return tasks;
        }

        private void FindTasks<T>(Task task, ref List<T> taskList) where T : Task
        {
            if (typeof(T).IsAssignableFrom(task.GetType())) {
                taskList.Add((T)task);
            }

            ParentTask parentTask;
            if ((parentTask = task as ParentTask) != null) {
                if (parentTask.Children != null) {
                    for (int i = 0; i < parentTask.Children.Count; ++i) {
                        FindTasks<T>(parentTask.Children[i], ref taskList);
                    }
                }
            }
        }

        public Task FindTaskWithName(string taskName)
        {
            CheckForSerialization();
            return FindTaskWithName(taskName, mBehaviorSource.RootTask);
        }

        private void CheckForSerialization()
        {
            mBehaviorSource.Owner = this;
            mBehaviorSource.CheckForSerialization(false);
        }

        private Task FindTaskWithName(string taskName, Task task)
        {
            if (task.FriendlyName.Equals(taskName)) {
                return task;
            }

            ParentTask parentTask;
            if ((parentTask = task as ParentTask) != null) {
                if (parentTask.Children != null) {
                    for (int i = 0; i < parentTask.Children.Count; ++i) {
                        Task foundTask = null;
                        if ((foundTask = FindTaskWithName(taskName, parentTask.Children[i])) != null) {
                            return foundTask;
                        }
                    }
                }
            }

            return null;
        }

        public List<Task> FindTasksWithName(string taskName)
        {
            CheckForSerialization();

            List<Task> tasks = new List<Task>();
            FindTasksWithName(taskName, mBehaviorSource.RootTask, ref tasks);
            return tasks;
        }

        private void FindTasksWithName(string taskName, Task task, ref List<Task> taskList)
        {
            if (task.FriendlyName.Equals(taskName)) {
                taskList.Add(task);
            }

            ParentTask parentTask;
            if ((parentTask = task as ParentTask) != null) {
                if (parentTask.Children != null) {
                    for (int i = 0; i < parentTask.Children.Count; ++i) {
                        FindTasksWithName(taskName, parentTask.Children[i], ref taskList);
                    }
                }
            }
        }
    }
}