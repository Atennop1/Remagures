using UnityEngine;
#if ENABLE_MULTIPLAYER
using UnityEngine.Networking;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
#if ENABLE_MULTIPLAYER
    // Temporarly make Behavior not abstract so networking will work. This is needed due to Unity bug 697682.
    public class Behavior : NetworkBehaviour, IBehavior
#else
    public abstract class Behavior : MonoBehaviour, IBehavior
#endif
    {
        [SerializeField]
        private bool startWhenEnabled = true;
        public bool StartWhenEnabled { get { return startWhenEnabled; } set { startWhenEnabled = value; } }
        [SerializeField]
        private bool pauseWhenDisabled = false;
        public bool PauseWhenDisabled { get { return pauseWhenDisabled; } set { pauseWhenDisabled = value; } }
        [SerializeField]
        private bool restartWhenComplete = false;
        public bool RestartWhenComplete { get { return restartWhenComplete; } set { restartWhenComplete = value; } }
        [SerializeField]
        private bool logTaskChanges = false;
        public bool LogTaskChanges { get { return logTaskChanges; } set { logTaskChanges = value; } }
        [SerializeField]
        private int group = 0;
        public int Group { get { return group; } set { group = value; } }
        [SerializeField]
        private bool resetValuesOnRestart = false;
        public bool ResetValuesOnRestart { get { return resetValuesOnRestart; } set { resetValuesOnRestart = value; } }
        // reference to an external behavior tree, useful if creating a behavior tree from script
        [SerializeField]
        private ExternalBehavior externalBehavior;
        public ExternalBehavior ExternalBehavior
        {
            get { return externalBehavior; }
            set
            {
                if (externalBehavior == value) {
                    return;
                }
                if (BehaviorManager.instance != null) {
                    BehaviorManager.instance.DisableBehavior(this);
                }
                // If the external tree has been initialized then it has been manually deserialized. Don't deserialize again.
                if (value != null && value.Initialized) {
                    // Store a reference to the current tree's variables so they can inherit the external tree variables.
                    var variables = mBehaviorSource.GetAllVariables();
                    mBehaviorSource = value.BehaviorSource;
                    mBehaviorSource.HasSerialized = true;
                    if (variables != null) {
                        for (int i = 0; i < variables.Count; ++i) {
                            if (variables[i] == null) {
                                continue;
                            }
                            mBehaviorSource.SetVariable(variables[i].Name, variables[i]);
                        }
                    }
                } else {
                    mBehaviorSource.HasSerialized = false;
                    hasInheritedVariables = false;
                }
                initialized = false;
                externalBehavior = value;
                if (startWhenEnabled) {
                    EnableBehavior();
                }
            }
        }
        private bool hasInheritedVariables = false;
        public bool HasInheritedVariables { get { return hasInheritedVariables; } set { hasInheritedVariables = value; } }
        public string BehaviorName { get { return mBehaviorSource.behaviorName; } set { mBehaviorSource.behaviorName = value; } }
        public string BehaviorDescription { get { return mBehaviorSource.behaviorDescription; } set { mBehaviorSource.behaviorDescription = value; } }

        [SerializeField]
        private BehaviorSource mBehaviorSource;
        public BehaviorSource GetBehaviorSource() { return mBehaviorSource; }
        public void SetBehaviorSource(BehaviorSource behaviorSource) { mBehaviorSource = behaviorSource; }
        public UnityEngine.Object GetObject() { return this; }
        public string GetOwnerName() { return gameObject.name; }

        private bool isPaused = false;
        private TaskStatus executionStatus = TaskStatus.Inactive;
        public TaskStatus ExecutionStatus { get { return executionStatus; } set { executionStatus = value; } }
        private bool initialized = false;

        private Dictionary<Task, Dictionary<string, object>> defaultValues;
        private Dictionary<string, object> defaultVariableValues;

        // events
        public enum EventTypes { OnCollisionEnter, OnCollisionExit, OnTriggerEnter, OnTriggerExit,
                                 OnCollisionEnter2D, OnCollisionExit2D, OnTriggerEnter2D, OnTriggerExit2D, 
                                 OnControllerColliderHit, OnLateUpdate, OnFixedUpdate, OnAnimatorIK, None }
        private bool[] hasEvent = new bool[(int)EventTypes.None];
        public bool[] HasEvent { get { return hasEvent; } }
                
        // coroutines
        private Dictionary<string, List<TaskCoroutine>> activeTaskCoroutines = null;

        // events
        public delegate void BehaviorHandler(Behavior behavior);
        public event BehaviorHandler OnBehaviorStart;
        public event BehaviorHandler OnBehaviorRestart;
        public event BehaviorHandler OnBehaviorEnd;
        private Dictionary<Type, Dictionary<string, Delegate>> eventTable;

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
        // gizmo drawings
        public enum GizmoViewMode { Running, Always, Selected, Never }
        [NonSerialized]
        public GizmoViewMode gizmoViewMode;
        [NonSerialized]
        public bool showBehaviorDesignerGizmo = true;
#endif

        public Behavior()
        {
            mBehaviorSource = new BehaviorSource(this);
        }

        public void Start()
        {
            if (startWhenEnabled) {
                EnableBehavior();
            }
        }

        private bool TaskContainsMethod(string methodName, Task task)
        {
            if (task == null) {
                return false;
            }

#if !UNITY_EDITOR && (UNITY_WSA_8_0 || UNITY_WSA_8_1)
            var method = task.GetType().GetMethod(methodName, System.BindingFlags.Public |System.BindingFlags.NonPublic | System.BindingFlags.Instance | System.BindingFlags.DeclaredOnly);
#else
            var method = task.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
#endif
            if (method != null && method.DeclaringType.IsAssignableFrom(task.GetType())) {
                return true;
            }

            if (task is ParentTask) {
                var parentTask = task as ParentTask;
                if (parentTask.Children != null) {
                    for (int i = 0; i < parentTask.Children.Count; ++i) {
                        if (TaskContainsMethod(methodName, parentTask.Children[i])) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void EnableBehavior()
        {
            // create the behavior manager if it doesn't already exist
            CreateBehaviorManager();
            if (BehaviorManager.instance != null) {
                BehaviorManager.instance.EnableBehavior(this);
            }

            if (!initialized) {
                for (int i = 0; i < (int)EventTypes.None; ++i) {
                    hasEvent[i] = TaskContainsMethod(((EventTypes)i).ToString(), mBehaviorSource.RootTask);
                }
#if ENABLE_MULTIPLAYER
                var variables = GetAllVariables();
                if (variables != null) {
                    for (int i = 0; i < variables.Count; ++i) {
                        variables[i].Owner = this;
                    }
                }
#endif
                initialized = true;
            }
        }

        public void DisableBehavior()
        {
            if (BehaviorManager.instance != null) {
                BehaviorManager.instance.DisableBehavior(this, pauseWhenDisabled);
                isPaused = pauseWhenDisabled;
            }
        }

        public void DisableBehavior(bool pause)
        {
            if (BehaviorManager.instance != null) {
                BehaviorManager.instance.DisableBehavior(this, pause);
                isPaused = pause;
            }
        }

        public void OnEnable()
        {
            if (BehaviorManager.instance != null && isPaused) {
                BehaviorManager.instance.EnableBehavior(this);
                isPaused = false;
            } else if (startWhenEnabled && initialized) {
                EnableBehavior();
            }
        }

        public void OnDisable()
        {
            DisableBehavior();
        }

        public void OnDestroy()
        {
            if (BehaviorManager.instance != null) {
                BehaviorManager.instance.DestroyBehavior(this);
            }
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
                if (value is SharedVariable) {
                    var sharedVariableValue = value as SharedVariable;
                    if (!string.IsNullOrEmpty(sharedVariableValue.PropertyMapping)) {
                        sharedVariable.PropertyMapping = sharedVariableValue.PropertyMapping;
                        sharedVariable.PropertyMappingOwner = sharedVariableValue.PropertyMappingOwner;
                        sharedVariable.InitializePropertyMapping(mBehaviorSource);
                    } else {
                        sharedVariable.SetValue(sharedVariableValue.GetValue());
                    }
                } else {
                    sharedVariable.SetValue(value);
                }
                sharedVariable.ValueChanged();
            } else {
                if (value is SharedVariable) {
                    // Add the new variable
                    var sharedVariableValue = value as SharedVariable;
                    var variable = TaskUtility.CreateInstance(sharedVariableValue.GetType()) as SharedVariable;
                    variable.Name = sharedVariableValue.Name;
                    variable.IsShared = sharedVariableValue.IsShared;
                    variable.IsGlobal = sharedVariableValue.IsGlobal;
                    if (!string.IsNullOrEmpty(sharedVariableValue.PropertyMapping)) {
                        variable.PropertyMapping = sharedVariableValue.PropertyMapping;
                        variable.PropertyMappingOwner = sharedVariableValue.PropertyMappingOwner;
                        variable.InitializePropertyMapping(mBehaviorSource);
                    } else {
                        variable.SetValue(sharedVariableValue.GetValue());
                    }
                    mBehaviorSource.SetVariable(name, variable);
                } else {
                    Debug.LogError("Error: No variable exists with name " + name);
                }
            }
        }

        public List<SharedVariable> GetAllVariables()
        {
            CheckForSerialization();
            return mBehaviorSource.GetAllVariables();
        }

        public void CheckForSerialization()
        {
            if (externalBehavior != null) {
                List<SharedVariable> variables = null;
                var forceSerialization = false;
                if (!hasInheritedVariables && !externalBehavior.Initialized) {
                    mBehaviorSource.CheckForSerialization(false);
                    variables = mBehaviorSource.GetAllVariables();
                    hasInheritedVariables = true;
                    forceSerialization = true;
                }
                externalBehavior.BehaviorSource.Owner = ExternalBehavior;
                externalBehavior.BehaviorSource.CheckForSerialization(forceSerialization, GetBehaviorSource());
                externalBehavior.BehaviorSource.EntryTask = mBehaviorSource.EntryTask;
                if (variables != null) {
                    for (int i = 0; i < variables.Count; ++i) {
                        if (variables[i] == null) {
                            continue;
                        }
                        mBehaviorSource.SetVariable(variables[i].Name, variables[i]);
                    }
                }
            } else {
                mBehaviorSource.CheckForSerialization(false);
            }
        }

        // Support collisions/triggers:
        public void OnCollisionEnter(Collision collision)
        {
            if (hasEvent[(int)EventTypes.OnCollisionEnter] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnCollisionEnter(collision, this);
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            if (hasEvent[(int)EventTypes.OnCollisionExit] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnCollisionExit(collision, this);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (hasEvent[(int)EventTypes.OnTriggerEnter] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnTriggerEnter(other, this);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (hasEvent[(int)EventTypes.OnTriggerExit] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnTriggerExit(other, this);
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (hasEvent[(int)EventTypes.OnCollisionEnter2D] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnCollisionEnter2D(collision, this);
            }
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            if (hasEvent[(int)EventTypes.OnCollisionExit2D] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnCollisionExit2D(collision, this);
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (hasEvent[(int)EventTypes.OnTriggerEnter2D] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnTriggerEnter2D(other, this);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (hasEvent[(int)EventTypes.OnTriggerExit2D] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnTriggerExit2D(other, this);
            }
        }

        public void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hasEvent[(int)EventTypes.OnControllerColliderHit] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnControllerColliderHit(hit, this);
            }
        }

        public void OnAnimatorIK()
        {
            if (hasEvent[(int)EventTypes.OnAnimatorIK] && BehaviorManager.instance != null) {
                BehaviorManager.instance.BehaviorOnAnimatorIK(this);
            }
        }

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
        public void OnDrawGizmos()
        {
            DrawTaskGizmos(false);
        }

        public void OnDrawGizmosSelected()
        {
            if (showBehaviorDesignerGizmo) {
                Gizmos.DrawIcon(transform.position, "Behavior Designer Scene Icon.png");
            }
            DrawTaskGizmos(true);
        }

        private void DrawTaskGizmos(bool selected)
        {
            if (gizmoViewMode == GizmoViewMode.Never || (gizmoViewMode == GizmoViewMode.Selected && !selected)) {
                return;
            }

            if (gizmoViewMode == GizmoViewMode.Running || gizmoViewMode == GizmoViewMode.Always || (Application.isPlaying && ExecutionStatus == TaskStatus.Running) || !Application.isPlaying) {
                CheckForSerialization();

                DrawTaskGizmos(mBehaviorSource.RootTask);

                var detachedTasks = mBehaviorSource.DetachedTasks;
                if (detachedTasks != null) {
                    for (int i = 0; i < detachedTasks.Count; ++i) {
                        DrawTaskGizmos(detachedTasks[i]);
                    }
                }
            }
        }

        private void DrawTaskGizmos(Task task)
        {
            if (task == null) {
                return;
            }

            // If the view mode is Running then only draw the gizmo when the task is reevaluating (with conditional aborts) or is currently running.
            if (gizmoViewMode == GizmoViewMode.Running && (!task.NodeData.IsReevaluating && (task.NodeData.IsReevaluating || task.NodeData.ExecutionStatus != TaskStatus.Running))) {
                return;
            }

            task.OnDrawGizmos();

            if (task is ParentTask) {
                var parentTask = task as ParentTask;
                if (parentTask.Children != null) {
                    for (int i = 0; i < parentTask.Children.Count; ++i) {
                        DrawTaskGizmos(parentTask.Children[i]);
                    }
                }
            }
        }
#endif

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

        public List<Task> GetActiveTasks()
        {
            if (BehaviorManager.instance == null) {
                return null;
            }
            return BehaviorManager.instance.GetActiveTasks(this);
        }

        // System.objects don't normally support coroutines. Add that support here.
        public Coroutine StartTaskCoroutine(Task task, string methodName)
        {
#if !UNITY_EDITOR && (UNITY_WSA_8_0 || UNITY_WSA_8_1)
            var method = task.GetType().GetMethod(methodName, System.BindingFlags.Public |System.BindingFlags.NonPublic | System.BindingFlags.Instance);
#else
            var method = task.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
#endif
            if (method == null) {
                Debug.LogError("Unable to start coroutine " + methodName + ": method not found");
                return null;
            }
            if (activeTaskCoroutines == null) {
                activeTaskCoroutines = new Dictionary<string, List<TaskCoroutine>>();
            }
            var taskCoroutine = new TaskCoroutine(this, (IEnumerator)method.Invoke(task, new object[] { }), methodName);
            if (activeTaskCoroutines.ContainsKey(methodName)) {
                var taskCoroutines = activeTaskCoroutines[methodName];
                taskCoroutines.Add(taskCoroutine);
                activeTaskCoroutines[methodName] = taskCoroutines;
            } else {
                var taskCoroutines = new List<TaskCoroutine>();
                taskCoroutines.Add(taskCoroutine);
                activeTaskCoroutines.Add(methodName, taskCoroutines);
            }
            return taskCoroutine.Coroutine;
        }

        public Coroutine StartTaskCoroutine(Task task, string methodName, object value)
        {
#if !UNITY_EDITOR && (UNITY_WSA_8_0 || UNITY_WSA_8_1)
            var method = task.GetType().GetMethod(methodName, System.BindingFlags.Public |System.BindingFlags.NonPublic | System.BindingFlags.Instance);
#else
            var method = task.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
#endif
            if (method == null) {
                Debug.LogError("Unable to start coroutine " + methodName + ": method not found");
                return null;
            }
            if (activeTaskCoroutines == null) {
                activeTaskCoroutines = new Dictionary<string, List<TaskCoroutine>>();
            }
            var taskCoroutine = new TaskCoroutine(this, (IEnumerator)method.Invoke(task, new object[] { value }), methodName);
            if (activeTaskCoroutines.ContainsKey(methodName)) {
                var taskCoroutines = activeTaskCoroutines[methodName];
                taskCoroutines.Add(taskCoroutine);
                activeTaskCoroutines[methodName] = taskCoroutines;
            } else {
                var taskCoroutines = new List<TaskCoroutine>();
                taskCoroutines.Add(taskCoroutine);
                activeTaskCoroutines.Add(methodName, taskCoroutines);
            }
            return taskCoroutine.Coroutine;
        }

        public void StopTaskCoroutine(string methodName)
        {
            if (!activeTaskCoroutines.ContainsKey(methodName)) {
                return;
            }

            var taskCoroutines = activeTaskCoroutines[methodName];
            for (int i = 0; i < taskCoroutines.Count; ++i) {
                taskCoroutines[i].Stop();
            }
        }

        public void StopAllTaskCoroutines()
        {
            StopAllCoroutines();

            foreach (var entry in activeTaskCoroutines) {
                var taskCoroutines = entry.Value;
                for (int i = 0; i < taskCoroutines.Count; ++i) {
                    taskCoroutines[i].Stop();
                }
            }
        }

        public void TaskCoroutineEnded(TaskCoroutine taskCoroutine, string coroutineName)
        {
            if (activeTaskCoroutines.ContainsKey(coroutineName)) {
                var taskCoroutines = activeTaskCoroutines[coroutineName];
                if (taskCoroutines.Count == 1) {
                    activeTaskCoroutines.Remove(coroutineName);
                } else {
                    taskCoroutines.Remove(taskCoroutine);
                    activeTaskCoroutines[coroutineName] = taskCoroutines;
                }
            }
        }

        public void OnBehaviorStarted()
        {
            if (OnBehaviorStart != null) {
                OnBehaviorStart(this);
            }
        }

        public void OnBehaviorRestarted()
        {
            if (OnBehaviorRestart != null) {
                OnBehaviorRestart(this);
            }
        }

        public void OnBehaviorEnded()
        {
            if (OnBehaviorEnd != null) {
                OnBehaviorEnd(this);
            }
        }

        private void RegisterEvent(string name, Delegate handler)
        {
            if (eventTable == null) {
                eventTable = new Dictionary<Type, Dictionary<string, Delegate>>();
            }

            Dictionary<string, Delegate> eventHandlers;
            if (!eventTable.TryGetValue(handler.GetType(), out eventHandlers)) {
                eventHandlers = new Dictionary<string, Delegate>();
                eventTable.Add(handler.GetType(), eventHandlers);
            }

            Delegate prevHandlers;
            if (eventHandlers.TryGetValue(name, out prevHandlers)) {
                eventHandlers[name] = Delegate.Combine(prevHandlers, handler);
            } else {
                eventHandlers.Add(name, handler);
            }
        }

        public void RegisterEvent(string name, System.Action handler)
        {
            RegisterEvent(name, (Delegate)handler);
        }

        public void RegisterEvent<T>(string name, System.Action<T> handler)
        {
            RegisterEvent(name, (Delegate)handler);
        }

        public void RegisterEvent<T, U>(string name, System.Action<T, U> handler)
        {
            RegisterEvent(name, (Delegate)handler);
        }

        public void RegisterEvent<T, U, V>(string name, System.Action<T, U, V> handler)
        {
            RegisterEvent(name, (Delegate)handler);
        }

        private Delegate GetDelegate(string name, Type type)
        {
            Dictionary<string, Delegate> eventHandlers;
            if (eventTable != null && eventTable.TryGetValue(type, out eventHandlers)) {
                Delegate handler;
                if (eventHandlers.TryGetValue(name, out handler)) {
                    return handler;
                }
            }
            return null;
        }

        public void SendEvent(string name)
        {
            var handler = GetDelegate(name, typeof(System.Action)) as System.Action;
            if (handler != null) {
                handler();
            }
        }

        public void SendEvent<T>(string name, T arg1)
        {
            var handler = GetDelegate(name, typeof(System.Action<T>)) as System.Action<T>;
            if (handler != null) {
                handler(arg1);
            }
        }

        public void SendEvent<T, U>(string name, T arg1, U arg2)
        {
            var handler = GetDelegate(name, typeof(System.Action<T, U>)) as System.Action<T, U>;
            if (handler != null) {
                handler(arg1, arg2);
            }
        }

        public void SendEvent<T, U, V>(string name, T arg1, U arg2, V arg3)
        {
            var handler = GetDelegate(name, typeof(System.Action<T, U, V>)) as System.Action<T, U, V>;
            if (handler != null) {
                handler(arg1, arg2, arg3);
            }
        }

        private void UnregisterEvent(string name, Delegate handler)
        {
            if (eventTable == null) {
                return;
            }
            Dictionary<string, Delegate> eventHandlers;
            if (eventTable.TryGetValue(handler.GetType(), out eventHandlers)) {
                Delegate prevHandlers;
                if (eventHandlers.TryGetValue(name, out prevHandlers)) {
                    eventHandlers[name] = Delegate.Remove(prevHandlers, handler);
                }
            }
        }

        public void UnregisterEvent(string name, System.Action handler)
        {
            UnregisterEvent(name, (Delegate)handler);
        }

        public void UnregisterEvent<T>(string name, System.Action<T> handler)
        {
            UnregisterEvent(name, (Delegate)handler);
        }

        public void UnregisterEvent<T, U>(string name, System.Action<T, U> handler)
        {
            UnregisterEvent(name, (Delegate)handler);
        }

        public void UnregisterEvent<T, U, V>(string name, System.Action<T, U, V> handler)
        {
            UnregisterEvent(name, (Delegate)handler);
        }

        public void SaveResetValues()
        {
            if (defaultValues == null) {
                CheckForSerialization();

                defaultValues = new Dictionary<Task, Dictionary<string, object>>();
                defaultVariableValues = new Dictionary<string, object>();

                SaveValues();
            } else {
                ResetValues();
            }
        }

        private void SaveValues()
        {
            var variables = mBehaviorSource.GetAllVariables();
            if (variables != null) {
                for (int i = 0; i < variables.Count; ++i) {
                    defaultVariableValues.Add(variables[i].Name, variables[i].GetValue());
                }
            }

            SaveValue(mBehaviorSource.RootTask);
        }

        private void SaveValue(Task task)
        {
            if (task == null) {
                return;
            }

            var fields = TaskUtility.GetPublicFields(task.GetType());
            var taskValues = new Dictionary<string, object>();
            for (int i = 0; i < fields.Length; ++i) {
                var value = fields[i].GetValue(task);
                if (value is SharedVariable) {
                    var sharedVariable = value as SharedVariable;
                    if (sharedVariable.IsGlobal || sharedVariable.IsShared) {
                        continue;
                    }
                }

                taskValues.Add(fields[i].Name, fields[i].GetValue(task));
            }

            defaultValues.Add(task, taskValues);

            if (task is ParentTask) {
                var parentTask = task as ParentTask;
                if (parentTask.Children != null) {
                    for (int i = 0; i < parentTask.Children.Count; ++i) {
                        SaveValue(parentTask.Children[i]);
                    }
                }
            }
        }

        private void ResetValues()
        {
            foreach (var variableValue in defaultVariableValues) {
                SetVariableValue(variableValue.Key, variableValue.Value);
            }
            
            ResetValue(mBehaviorSource.RootTask);
        }

        private void ResetValue(Task task)
        {
            if (task == null) {
                return;
            }

            Dictionary<string, object> taskValues;
            if (!defaultValues.TryGetValue(task, out taskValues)) {
                return;
            }

            foreach (var taskValue in taskValues) {
                var field = task.GetType().GetField(taskValue.Key);
                if (field != null) {
                    field.SetValue(task, taskValue.Value);
                }
            }

            if (task is ParentTask) {
                var parentTask = task as ParentTask;
                if (parentTask.Children != null) {
                    for (int i = 0; i < parentTask.Children.Count; ++i) {
                        ResetValue(parentTask.Children[i]);
                    }
                }
            }
        }

#if ENABLE_MULTIPLAYER
        public void DirtyVariable(SharedVariable variable)
        {
            // Only the server can mark variables as dirty.
            if (!NetworkServer.active) {
                return;
            }

            // ClientRPC calls cannot send generic object parameters. In addition, ClientRPC methods cannot be overloaded (bug 697809)
            var variableValue = variable.GetValue();
            if (variableValue == null) {
                RpcDirtyVariableNull(variable.Name);
            } else {
                var variableType = variableValue.GetType();
                if (variableType == typeof(bool)) {
                    RpcDirtyVariableBool(variable.Name, (bool)variableValue);
                } else if (variableType == typeof(Color)) {
                    RpcDirtyVariableColor(variable.Name, (Color)variableValue);
                } else if (variableType == typeof(float)) {
                    RpcDirtyVariableFloat(variable.Name, (float)variableValue);
                } else if (variableType == typeof(GameObject)) {
                    RpcDirtyVariableGameObject(variable.Name, (GameObject)variableValue);
                } else if (variableType == typeof(int)) {
                    RpcDirtyVariableInt(variable.Name, (int)variableValue);
                } else if (variableType == typeof(Quaternion)) {
                    RpcDirtyVariableQuaternion(variable.Name, (Quaternion)variableValue);
                } else if (variableType == typeof(Rect)) {
                    RpcDirtyVariableRect(variable.Name, (Rect)variableValue);
                } else if (variableType == typeof(string)) {
                    RpcDirtyVariableString(variable.Name, (string)variableValue);
                } else if (variableType == typeof(Transform)) {
                    RpcDirtyVariableTransform(variable.Name, ((Transform)variableValue).gameObject);
                } else if (variableType == typeof(Vector2)) {
                    RpcDirtyVariableVector2(variable.Name, (Vector2)variableValue);
                } else if (variableType == typeof(Vector3)) {
                    RpcDirtyVariableVector3(variable.Name, (Vector3)variableValue);
                } else if (variableType == typeof(Vector4)) {
                    RpcDirtyVariableVector4(variable.Name, (Vector4)variableValue);
                } else {
                    Debug.LogError("Error: Unable to synchronize SharedVariable type " + variableType);
                }
            }
        }

        [ClientRpc]
        private void RpcDirtyVariableNull(string name)
        {
            GetVariable(name).SetValue(null);
        }

        [ClientRpc]
        private void RpcDirtyVariableBool(string name, bool value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableColor(string name, Color value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableFloat(string name, float value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableGameObject(string name, GameObject value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableInt(string name, int value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableQuaternion(string name, Quaternion value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableRect(string name, Rect value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableString(string name, string value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableTransform(string name, GameObject value)
        {
            GetVariable(name).SetValue(value.transform);
        }

        [ClientRpc]
        private void RpcDirtyVariableVector2(string name, Vector2 value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableVector3(string name, Vector3 value)
        {
            GetVariable(name).SetValue(value);
        }

        [ClientRpc]
        private void RpcDirtyVariableVector4(string name, Vector4 value)
        {
            GetVariable(name).SetValue(value);
        }
#endif

        public override string ToString()
        {
            return mBehaviorSource.ToString();
        }

        public static BehaviorManager CreateBehaviorManager()
        {
            if (BehaviorManager.instance == null && Application.isPlaying) {
                var behaviorManager = new GameObject();
                //behaviorManager.hideFlags = HideFlags.HideAndDontSave;
                behaviorManager.name = "Behavior Manager";
                return behaviorManager.AddComponent<BehaviorManager>();
            }
            return null;
        }
    }
}