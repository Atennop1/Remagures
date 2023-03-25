using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using BehaviorDesigner.Runtime.Tasks;

#if DLL_DEBUG || DLL_RELEASE
// This class is required on for AOT platforms to work with the DLL. This class is not necessary if the source code is extracted.
// Without this class the following exception will occur:
// System.MissingMethodException: Method not found: 'Default constructor not found...ctor() of BehaviorDesigner.Runtime.BehaviorManager+TaskAddData'.
public class AOTLinker : MonoBehaviour
{
    public void Linker()
    {
#pragma warning disable 219
        var behaviorTree = new BehaviorDesigner.Runtime.BehaviorManager.BehaviorTree();
        var conditionalReevaluate = new BehaviorDesigner.Runtime.BehaviorManager.BehaviorTree.ConditionalReevaluate();
        var taskAddData = new BehaviorDesigner.Runtime.BehaviorManager.TaskAddData();
        var overrideFieldValue = new BehaviorDesigner.Runtime.BehaviorManager.TaskAddData.OverrideFieldValue();
#pragma warning restore 219
    }
}
#endif

namespace BehaviorDesigner.Runtime
{
    public enum UpdateIntervalType { EveryFrame, SpecifySeconds, Manual }

    [AddComponentMenu("Behavior Designer/Behavior Manager")]
    public class BehaviorManager : MonoBehaviour
    {
        static public BehaviorManager instance;

        [SerializeField]
        private UpdateIntervalType updateInterval = UpdateIntervalType.EveryFrame;
        public UpdateIntervalType UpdateInterval { get { return updateInterval; } set { updateInterval = value; UpdateIntervalChanged(); } }
        [SerializeField]
        private float updateIntervalSeconds = 0;
        public float UpdateIntervalSeconds { get { return updateIntervalSeconds; } set { updateIntervalSeconds = value; UpdateIntervalChanged(); } }
        public enum ExecutionsPerTickType { NoDuplicates, Count }
        [SerializeField]
        private ExecutionsPerTickType executionsPerTick = ExecutionsPerTickType.NoDuplicates;
        public ExecutionsPerTickType ExecutionsPerTick { get { return executionsPerTick; } set { executionsPerTick = value; } }
        [SerializeField]
        private int maxTaskExecutionsPerTick = 100;
        public int MaxTaskExecutionsPerTick { get { return maxTaskExecutionsPerTick; } set { maxTaskExecutionsPerTick = value; } }
        private WaitForSeconds updateWait = null;

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
        public delegate void BehaviorManagerHandler();
        public BehaviorManagerHandler OnEnableBehavior { set { onEnableBehavior = value; } }
        public BehaviorManagerHandler onEnableBehavior;
        public BehaviorManagerHandler OnTaskBreakpoint { get { return onTaskBreakpoint; } set { onTaskBreakpoint += value; } }
        public BehaviorManagerHandler onTaskBreakpoint;
#endif

        // one behavior tree for each client
        public class BehaviorTree
        {
            public class ConditionalReevaluate
            {
                public int index;
                public TaskStatus taskStatus;
                public int compositeIndex = -1; // -1 means inactive
                public int stackIndex = -1;

                public ConditionalReevaluate() { }

                public void Initialize(int i, TaskStatus status, int stack, int composite)
                {
                    index = i;
                    taskStatus = status;
                    stackIndex = stack;
                    compositeIndex = composite;
                }
            }

            public List<Task> taskList = new List<Task>();
            public List<int> parentIndex = new List<int>();
            public List<List<int>> childrenIndex = new List<List<int>>();
            // the relative child index is the index relative to the parent. For example, the first child has a relative child index of 0
            public List<int> relativeChildIndex = new List<int>();
            public List<Stack<int>> activeStack = new List<Stack<int>>();
            public List<TaskStatus> nonInstantTaskStatus = new List<TaskStatus>();
            public List<int> interruptionIndex = new List<int>();
            public List<ConditionalReevaluate> conditionalReevaluate = new List<ConditionalReevaluate>();
            public Dictionary<int, BehaviorTree.ConditionalReevaluate> conditionalReevaluateMap = new Dictionary<int, ConditionalReevaluate>();
            public List<int> parentReevaluate = new List<int>();
            public List<int> parentCompositeIndex = new List<int>();
            public List<List<int>> childConditionalIndex = new List<List<int>>();
            public int executionCount;
            public Behavior behavior;
            public bool destroyBehavior;

            public void Initialize(Behavior b)
            {
                behavior = b;

                for (int i = childrenIndex.Count - 1; i > -1; --i) {
                    ObjectPool.Return(childrenIndex[i]);
                }
                for (int i = activeStack.Count - 1; i > -1; --i) {
                    ObjectPool.Return(activeStack[i]);
                }
                for (int i = childConditionalIndex.Count - 1; i > -1; --i) {
                    ObjectPool.Return(childConditionalIndex[i]);
                }
                taskList.Clear();
                parentIndex.Clear();
                childrenIndex.Clear();
                relativeChildIndex.Clear();
                activeStack.Clear();
                nonInstantTaskStatus.Clear();
                interruptionIndex.Clear();
                conditionalReevaluate.Clear();
                conditionalReevaluateMap.Clear();
                parentReevaluate.Clear();
                parentCompositeIndex.Clear();
                childConditionalIndex.Clear();
            }
        }
        private List<BehaviorTree> behaviorTrees = new List<BehaviorTree>();
        public List<BehaviorTree> BehaviorTrees { get { return behaviorTrees; } }
        private Dictionary<Behavior, BehaviorTree> pausedBehaviorTrees = new Dictionary<Behavior, BehaviorTree>();
        private Dictionary<Behavior, BehaviorTree> behaviorTreeMap = new Dictionary<Behavior, BehaviorTree>();
        private List<int> conditionalParentIndexes = new List<int>();

        // Third party support
        public enum ThirdPartyObjectType { PlayMaker, uScript, DialogueSystem, uSequencer, ICode }
        public class ThirdPartyTask
        {
            private Task task;
            public Task Task { get { return task; } set { task = value; } }
            private ThirdPartyObjectType thirdPartyObjectType;
            public ThirdPartyObjectType ThirdPartyObjectType { get { return thirdPartyObjectType; } }
            public void Initialize(Task t, ThirdPartyObjectType objectType)
            {
                task = t;
                thirdPartyObjectType = objectType;
            }
        }
        public class ThirdPartyTaskComparer : IEqualityComparer<ThirdPartyTask>
        {
            public bool Equals(ThirdPartyTask a, ThirdPartyTask b)
            {
                if (ReferenceEquals(a, null)) return false;
                if (ReferenceEquals(b, null)) return false;
                return a.Task.Equals(b.Task);
            }

            public int GetHashCode(ThirdPartyTask obj)
            {
                return obj != null ? obj.Task.GetHashCode() : 0;
            }
        }

        private Dictionary<object, ThirdPartyTask> objectTaskMap = new Dictionary<object, ThirdPartyTask>();
        private Dictionary<ThirdPartyTask, object> taskObjectMap = new Dictionary<ThirdPartyTask, object>(new ThirdPartyTaskComparer());
        private ThirdPartyTask thirdPartyTaskCompare = new ThirdPartyTask();

        private static MethodInfo playMakerStopMethod = null;
        private static MethodInfo PlayMakerStopMethod
        {
            get { if (playMakerStopMethod == null) { playMakerStopMethod = TaskUtility.GetTypeWithinAssembly("BehaviorDesigner.Runtime.BehaviorManager_PlayMaker").GetMethod("StopPlayMaker"); } return playMakerStopMethod; }
        }
        private static MethodInfo uScriptStopMethod = null;
        private static MethodInfo UScriptStopMethod
        {
            get { if (uScriptStopMethod == null) { uScriptStopMethod = TaskUtility.GetTypeWithinAssembly("BehaviorDesigner.Runtime.BehaviorManager_uScript").GetMethod("StopuScript"); } return uScriptStopMethod; }
        }
        private static MethodInfo dialogueSystemStopMethod = null;
        private static MethodInfo DialogueSystemStopMethod
        {
            get { if (dialogueSystemStopMethod == null) { dialogueSystemStopMethod = TaskUtility.GetTypeWithinAssembly("BehaviorDesigner.Runtime.BehaviorManager_DialogueSystem").GetMethod("StopDialogueSystem"); } return dialogueSystemStopMethod; }
        }
        private static MethodInfo uSequencerStopMethod = null;
        private static MethodInfo USequencerStopMethod
        {
            get { if (uSequencerStopMethod == null) { uSequencerStopMethod = TaskUtility.GetTypeWithinAssembly("BehaviorDesigner.Runtime.BehaviorManager_uSequencer").GetMethod("StopuSequencer"); } return uSequencerStopMethod; }
        }
        private static MethodInfo iCodeStopMethod = null;
        private static MethodInfo ICodeStopMethod
        {
            get { if (iCodeStopMethod == null) { iCodeStopMethod = TaskUtility.GetTypeWithinAssembly("BehaviorDesigner.Runtime.BehaviorManager_ICode").GetMethod("StopICode"); } return iCodeStopMethod; }
        }
        private static object[] invokeParameters = null;

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
        private Behavior breakpointTree = null;
        public Behavior BreakpointTree { get { return breakpointTree; } set { breakpointTree = value; } }
        private bool dirty = false;
        public bool Dirty { get { return dirty; } set { dirty = value; } }
#endif
        
        // convenience class used for adding new tasks
        public class TaskAddData
        {
            public class OverrideFieldValue
            {
                private object value;
                public object Value { get { return value; } }
                private int depth;
                public int Depth { get { return depth; } }

                public void Initialize(object v, int d)
                {
                    value = v;
                    depth = d;
                }
            }

            public bool fromExternalTask = false;
            public ParentTask parentTask = null;
            public int parentIndex = -1;
            public int depth = 0;
            public int compositeParentIndex = -1;
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            public Vector2 offset;
#endif
            public Dictionary<string, OverrideFieldValue> overrideFields = null;
            public HashSet<object> overiddenFields = new HashSet<object>();
            public int errorTask = -1;
            public string errorTaskName = "";

            public void Initialize()
            {
                if (overrideFields != null) {
                    foreach (var field in overrideFields) {
                        ObjectPool.Return(field);
                    }
                }
                ObjectPool.Return(overrideFields);
                fromExternalTask = false;
                parentTask = null;
                parentIndex = -1;
                depth = 0;
                compositeParentIndex = -1;
                overrideFields = null;
            }
        }

        public void Awake()
        {
            instance = this;

            UpdateIntervalChanged();
        }

        private void UpdateIntervalChanged()
        {
            StopCoroutine("CoroutineUpdate");
            if (updateInterval == BehaviorDesigner.Runtime.UpdateIntervalType.EveryFrame) {
                enabled = true;
            } else if (updateInterval == BehaviorDesigner.Runtime.UpdateIntervalType.SpecifySeconds) {
                if (Application.isPlaying) {
                    updateWait = new WaitForSeconds(updateIntervalSeconds);
                    StartCoroutine("CoroutineUpdate");
                }
                enabled = false;
            } else { // manual
                enabled = false;
            }
        }

        public void OnDestroy()
        {
            for (int i = behaviorTrees.Count - 1; i > -1; --i) {
                DisableBehavior(behaviorTrees[i].behavior);
            }
            ObjectPool.Clear();
            instance = null;
        }

        public void OnApplicationQuit()
        {
            for (int i = behaviorTrees.Count - 1; i > -1; --i) {
                DisableBehavior(behaviorTrees[i].behavior);
            }
        }

        public void EnableBehavior(Behavior behavior)
        {
            BehaviorTree behaviorTree;
            if (IsBehaviorEnabled(behavior)) {
                return;
            } else if (pausedBehaviorTrees.TryGetValue(behavior, out behaviorTree)) { // unpause
                behaviorTrees.Add(behaviorTree);
                pausedBehaviorTrees.Remove(behavior);
                behavior.ExecutionStatus = TaskStatus.Running;

                for (int i = 0; i < behaviorTree.taskList.Count; ++i) {
                    behaviorTree.taskList[i].OnPause(false);
                }
                return;
            }

            var taskAddData = ObjectPool.Get<TaskAddData>();
            taskAddData.Initialize();
            // ensure the tree is deserialized
            behavior.CheckForSerialization();

            var rootTask = behavior.GetBehaviorSource().RootTask;
            if (rootTask == null) {
                Debug.LogError(string.Format("The behavior \"{0}\" on GameObject \"{1}\" contains no root task. This behavior will be disabled.", behavior.GetBehaviorSource().behaviorName, behavior.gameObject.name));
                return;
            }

            behaviorTree = ObjectPool.Get<BehaviorTree>();
            behaviorTree.Initialize(behavior);
            behaviorTree.parentIndex.Add(-1); // add the first entry for the root task
            behaviorTree.relativeChildIndex.Add(-1);
            behaviorTree.parentCompositeIndex.Add(-1);
            if (behavior.GetAllVariables() != null) {
                if (taskAddData.overrideFields == null) {
                    taskAddData.overrideFields = ObjectPool.Get<Dictionary<string, TaskAddData.OverrideFieldValue>>();
                    taskAddData.overrideFields.Clear();
                }
                var behaviorSource = behavior.GetBehaviorSource();
                for (int i = 0; i < behaviorSource.Variables.Count; ++i) {
                    var overrideFieldValue = ObjectPool.Get<TaskAddData.OverrideFieldValue>();
                    overrideFieldValue.Initialize(behaviorSource.Variables[i], 0);
                    taskAddData.overrideFields.Add(behaviorSource.Variables[i].Name, overrideFieldValue);
                }
            }

            bool hasExternalBehavior = behavior.ExternalBehavior != null;
            int status = AddToTaskList(behaviorTree, rootTask, ref hasExternalBehavior, taskAddData);
            if (status < 0) {
                // something is wrong with the tree. Don't go any further
                behaviorTree = null;
                switch (status) {
                    case -1:
                        Debug.LogError(string.Format("The behavior \"{0}\" on GameObject \"{1}\" contains a parent task ({2} (index {3})) with no children. This behavior will be disabled.",
                            behavior.GetBehaviorSource().behaviorName, behavior.gameObject.name, taskAddData.errorTaskName, taskAddData.errorTask));
                        break;
                    case -2:
                        Debug.LogError(string.Format("The behavior \"{0}\" on GameObject \"{1}\" cannot find the referenced external task. This behavior will be disabled.", behavior.GetBehaviorSource().behaviorName, behavior.gameObject.name));
                        break;
                    case -3:
                        Debug.LogError(string.Format("The behavior \"{0}\" on GameObject \"{1}\" contains a null task (referenced from parent task {2} (index {3})). This behavior will be disabled.",
                                behavior.GetBehaviorSource().behaviorName, behavior.gameObject.name, taskAddData.errorTaskName, taskAddData.errorTask));
                        break;
                    case -4:
                        Debug.LogError(string.Format("The behavior \"{0}\" on GameObject \"{1}\" contains multiple external behavior trees at the root task or as a child of a parent task which cannot contain so many children (such as a decorator task). This behavior will be disabled.", behavior.GetBehaviorSource().behaviorName, behavior.gameObject.name));
                        break;
                    case -5:
                        Debug.LogError(string.Format("The behavior \"{0}\" on GameObject \"{1}\" contains a Behavior Tree Reference task ({2} (index {3})) that which has an element with a null value in the externalBehaviors array. This behavior will be disabled.",
                                behavior.GetBehaviorSource().behaviorName, behavior.gameObject.name, taskAddData.errorTaskName, taskAddData.errorTask));
                        break;
                    case -6:
                        Debug.LogError(string.Format("The behavior \"{0}\" on GameObject \"{1}\" contains a root task which is disabled. This behavior will be disabled.",
                            behavior.GetBehaviorSource().behaviorName, behavior.gameObject.name));
                        break;
                }
                return;
            }

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            // a behavior tree is dirty when a new behavior tree is loaded. This will update the editor
            dirty = true;
            if (behavior.ExternalBehavior != null) {
                behavior.GetBehaviorSource().EntryTask = behavior.ExternalBehavior.BehaviorSource.EntryTask;
            }
            // update the root task with the newly instantiated task
            behavior.GetBehaviorSource().RootTask = behaviorTree.taskList[0];
#endif
            if (behavior.ResetValuesOnRestart) {
                behavior.SaveResetValues();
            }

            // add the first entry
            var stack = ObjectPool.Get<Stack<int>>();
            stack.Clear();
            behaviorTree.activeStack.Add(stack);
            behaviorTree.interruptionIndex.Add(-1);
            behaviorTree.nonInstantTaskStatus.Add(TaskStatus.Inactive);

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            if (behaviorTree.behavior.LogTaskChanges) {
                for (int i = 0; i < behaviorTree.taskList.Count; ++i) {
                    Debug.Log(string.Format("{0}: Task {1} ({2}, index {3}) {4}", RoundedTime(), behaviorTree.taskList[i].FriendlyName, behaviorTree.taskList[i].GetType(), i, behaviorTree.taskList[i].GetHashCode()));
                }
            }
#endif

            for (int i = 0; i < behaviorTree.taskList.Count; ++i) {
                behaviorTree.taskList[i].OnAwake();
            }

            // the behavior tree is ready to go
            behaviorTrees.Add(behaviorTree);
            behaviorTreeMap.Add(behavior, behaviorTree);

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            // let behavior designer know
            if (onEnableBehavior != null) {
                onEnableBehavior();
            }
#endif

            // start with the first index if it isn't disabled
            if (!behaviorTree.taskList[0].Disabled) {
                behaviorTree.behavior.OnBehaviorStarted();
                behavior.ExecutionStatus = TaskStatus.Running;
                PushTask(behaviorTree, 0, 0);
            }
        }

        // returns 0 for success
        // returns -1 if a parent doesn't have any children
        // returns -2 if the external task cannot be found
        // returns -3 if the task is null
        // returns -4 if there are multiple external behavior trees and the parent task is null or cannot handle as many behavior trees specified
        // returns -5 if a behavior tree reference task contains a null external tree
        // returns -6 the root task is disabled
        private int AddToTaskList(BehaviorTree behaviorTree, Task task, ref bool hasExternalBehavior, TaskAddData data)
        {
            if (task == null) {
                return -3;
            }

            // Assign the mono behavior components
            task.GameObject = behaviorTree.behavior.gameObject;
            task.Transform = behaviorTree.behavior.transform;
            task.Owner = behaviorTree.behavior;

            if (task is BehaviorReference) {
                BehaviorSource[] behaviorSource = null;
                var behaviorReference = task as BehaviorReference;
                if (behaviorReference != null) {
                    ExternalBehavior[] externalBehaviors = null;
                    if ((externalBehaviors = behaviorReference.GetExternalBehaviors()) != null) {
                        behaviorSource = new BehaviorSource[externalBehaviors.Length];
                        for (int i = 0; i < externalBehaviors.Length; ++i) {
                            if (externalBehaviors[i] == null) {
                                data.errorTask = behaviorTree.taskList.Count;
                                data.errorTaskName = !string.IsNullOrEmpty(task.FriendlyName) ? task.FriendlyName : task.GetType().ToString();
                                return -5;
                            }
                            behaviorSource[i] = externalBehaviors[i].BehaviorSource;
                            behaviorSource[i].Owner = externalBehaviors[i];
                        }
                    } else {
                        return -2;
                    }
                } else {
                    return -2;
                }
                if (behaviorSource != null) {
                    var parentTask = data.parentTask;
                    int parentIndex = data.parentIndex;
                    int compositeParentIndex = data.compositeParentIndex;
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                    data.offset = task.NodeData.Offset;
#endif
                    data.depth++;
                    for (int i = 0; i < behaviorSource.Length; ++i) {
                        // deserialize the external tasks into a new behavior source which will then be added into the original tree
                        var loadedBehaviorSource = ObjectPool.Get<BehaviorSource>();
                        loadedBehaviorSource.Initialize(behaviorSource[i].Owner);
                        behaviorSource[i].CheckForSerialization(true, loadedBehaviorSource);
                        var externalRootTask = loadedBehaviorSource.RootTask;
                        if (externalRootTask != null) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                            if (externalRootTask is ParentTask) {
                                externalRootTask.NodeData.Collapsed = (task as BehaviorReference).collapsed;
                            }
#endif
                            externalRootTask.Disabled = task.Disabled;
                            // find all of the variables loaded from the BehaviorReference task
                            if (behaviorReference.variables != null) {
                                for (int j = 0; j < behaviorReference.variables.Length; ++j) {
                                    if (data.overrideFields == null) {
                                        data.overrideFields = ObjectPool.Get<Dictionary<string, TaskAddData.OverrideFieldValue>>();
                                        data.overrideFields.Clear();
                                    }
                                    if (!data.overrideFields.ContainsKey(behaviorReference.variables[j].Value.name)) {
                                        var overrideFieldValue = ObjectPool.Get<TaskAddData.OverrideFieldValue>();
                                        overrideFieldValue.Initialize(behaviorReference.variables[j].Value, data.depth);
                                        // Generic named variables allow the variable to be nested across multiple levels so use the original override variable if a multi-level nesting exists.
                                        if (behaviorReference.variables[j].Value is NamedVariable) {
                                            var namedVariable = behaviorReference.variables[j].Value as NamedVariable;
                                            if (string.IsNullOrEmpty(namedVariable.name)) {
                                                Debug.LogWarning("Warning: Named variable on reference task " + behaviorReference.FriendlyName + " (id " + behaviorReference.ID + ") is null");
                                                continue;
                                            }
                                            if (namedVariable.value != null) {
                                                TaskAddData.OverrideFieldValue namedOverrideFieldValue;
                                                if (data.overrideFields.TryGetValue(namedVariable.name, out namedOverrideFieldValue)) {
                                                    overrideFieldValue = namedOverrideFieldValue;
                                                }
                                            }
                                        } else if (behaviorReference.variables[j].Value is GenericVariable) {
                                            var namedVariable = behaviorReference.variables[j].Value as GenericVariable;
                                            if (namedVariable.value != null) {
                                                if (string.IsNullOrEmpty(namedVariable.value.Name)) {
                                                    Debug.LogWarning("Warning: Named variable on reference task " + behaviorReference.FriendlyName + " (id " + behaviorReference.ID + ") is null");
                                                    continue;
                                                }
                                                TaskAddData.OverrideFieldValue genericOverrideFieldValue;
                                                if (data.overrideFields.TryGetValue(namedVariable.value.Name, out genericOverrideFieldValue)) {
                                                    overrideFieldValue = genericOverrideFieldValue;
                                                }
                                            }
                                        }
                                        data.overrideFields.Add(behaviorReference.variables[j].Value.name, overrideFieldValue);
                                    }
                                }
                            }

                            // bring the external behavior trees variables into the parent behavior tree
                            if (loadedBehaviorSource.Variables != null) {
                                for (int j = 0; j < loadedBehaviorSource.Variables.Count; ++j) {
                                    SharedVariable sharedVariable = null;
                                    // only set the behavior tree variable if it doesn't already exist in the parent behavior tree
                                    if ((sharedVariable = behaviorTree.behavior.GetVariable(loadedBehaviorSource.Variables[j].Name)) == null) {
                                        TaskAddData.OverrideFieldValue overrideField;
                                        if (data.overrideFields.TryGetValue(loadedBehaviorSource.Variables[j].Name, out overrideField)) {
                                            if (overrideField.Value is SharedVariable) {
                                                sharedVariable = overrideField.Value as SharedVariable;
                                            } else if (overrideField.Value is NamedVariable) {
                                                sharedVariable = (overrideField.Value as NamedVariable).value;
                                                sharedVariable.Name = (overrideField.Value as NamedVariable).name;
                                                if (sharedVariable.IsGlobal) {
                                                    sharedVariable = GlobalVariables.Instance.GetVariable(sharedVariable.Name);
                                                } else if (sharedVariable.IsShared) {
                                                    sharedVariable = behaviorTree.behavior.GetVariable(sharedVariable.Name);
                                                }
                                            } else if (overrideField.Value is GenericVariable) {
                                                sharedVariable = (overrideField.Value as GenericVariable).value;
                                                if (sharedVariable.IsGlobal) {
                                                    sharedVariable = GlobalVariables.Instance.GetVariable(sharedVariable.Name);
                                                } else if (sharedVariable.IsShared) {
                                                    sharedVariable = behaviorTree.behavior.GetVariable(sharedVariable.Name);
                                                }
                                            }
                                        } else {
                                            sharedVariable = loadedBehaviorSource.Variables[j];
                                        }
                                        behaviorTree.behavior.SetVariable(sharedVariable.Name, sharedVariable);
                                    } else {
                                        // The variable already exists in the parent tree so pass the value down
                                        loadedBehaviorSource.Variables[j].SetValue(sharedVariable.GetValue());
                                    }

                                    // automatically import the shared variables from the parent tree
                                    if (data.overrideFields == null) {
                                        data.overrideFields = ObjectPool.Get<Dictionary<string, TaskAddData.OverrideFieldValue>>();
                                        data.overrideFields.Clear();
                                    }
                                    if (!data.overrideFields.ContainsKey(sharedVariable.Name)) {
                                        var overrideFieldValue = ObjectPool.Get<TaskAddData.OverrideFieldValue>();
                                        overrideFieldValue.Initialize(sharedVariable, data.depth);
                                        data.overrideFields.Add(sharedVariable.Name, overrideFieldValue);
                                    }
                                }
                            }
                            ObjectPool.Return(loadedBehaviorSource);

                            if (i > 0) {
                                // If there are multiple external behavior trees then the TaskAddData was probably changed
                                data.parentTask = parentTask;
                                data.parentIndex = parentIndex;
                                data.compositeParentIndex = compositeParentIndex;
                                // Return an error if the parent task is null (root task) or if there are too many external behavior trees
                                if (data.parentTask == null || i >= data.parentTask.MaxChildren()) {
                                    return -4;
                                } else {
                                    // add the external tree
                                    behaviorTree.parentIndex.Add(data.parentIndex);
                                    behaviorTree.relativeChildIndex.Add(data.parentTask.Children.Count);
                                    behaviorTree.parentCompositeIndex.Add(data.compositeParentIndex);
                                    behaviorTree.childrenIndex[data.parentIndex].Add(behaviorTree.taskList.Count);
                                    data.parentTask.AddChild(externalRootTask, data.parentTask.Children.Count);
                                }
                            }
                            hasExternalBehavior = true;
                            bool fromExternalTask = data.fromExternalTask;
                            data.fromExternalTask = true;
                            int status = 0;
                            if ((status = AddToTaskList(behaviorTree, externalRootTask, ref hasExternalBehavior, data)) < 0) {
                                return status;
                            }
                            // reset back to the original value
                            data.fromExternalTask = fromExternalTask;
                        } else {
                            ObjectPool.Return(loadedBehaviorSource);
                            return -2;
                        }
                    }
                    // remove any overridden fields that aren't at the same depth in the tree anymore.
                    if (data.overrideFields != null) {
                        var overrideFields = ObjectPool.Get<Dictionary<string, TaskAddData.OverrideFieldValue>>();
                        overrideFields.Clear();
                        foreach (var field in data.overrideFields) {
                            if (field.Value.Depth != data.depth) {
                                overrideFields.Add(field.Key, field.Value);
                            }
                        }
                        ObjectPool.Return(data.overrideFields);
                        data.overrideFields = overrideFields;
                    }
                    data.depth--;
                } else {
                    return -2;
                }
            } else {
                if (behaviorTree.taskList.Count == 0 && task.Disabled) {
                    return -6;
                }
                task.ReferenceID = behaviorTree.taskList.Count;
                behaviorTree.taskList.Add(task);
                if (data.overrideFields != null) {
                    OverrideFields(behaviorTree, data, task);
                }
                if (data.fromExternalTask) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                    if (data.parentTask == null) { // A null parent task means the root task is the behavior tree reference task, so just replace that task
                        task.NodeData.Offset = behaviorTree.behavior.GetBehaviorSource().RootTask.NodeData.Offset;
                    } else {
#endif
                        int relativeChildIndex = behaviorTree.relativeChildIndex[behaviorTree.relativeChildIndex.Count - 1];
                        data.parentTask.ReplaceAddChild(task, relativeChildIndex);
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                        if (data.offset != Vector2.zero) {
                            task.NodeData.Offset = data.offset;
                            data.offset = Vector2.zero;
                        }
                    }
#endif
                }

                if (task is ParentTask) {
                    var parentTask = task as ParentTask;
                    if (parentTask.Children == null || parentTask.Children.Count == 0) {
                        data.errorTask = behaviorTree.taskList.Count - 1;
                        data.errorTaskName = !string.IsNullOrEmpty(behaviorTree.taskList[data.errorTask].FriendlyName) ?
                                                behaviorTree.taskList[data.errorTask].FriendlyName : behaviorTree.taskList[data.errorTask].GetType().ToString();
                        return -1; // invalid tree
                    }
                    int status;
                    int parentIndex = behaviorTree.taskList.Count - 1;
                    var list = ObjectPool.Get<List<int>>();
                    list.Clear();
                    behaviorTree.childrenIndex.Add(list);
                    list = ObjectPool.Get<List<int>>();
                    list.Clear();
                    behaviorTree.childConditionalIndex.Add(list);
                    // store the childCount ahead of time in case new external trees are added to the current parent
                    int childCount = parentTask.Children.Count;
                    for (int i = 0; i < childCount; ++i) {
                        behaviorTree.parentIndex.Add(parentIndex);
                        behaviorTree.relativeChildIndex.Add(i);
                        behaviorTree.childrenIndex[parentIndex].Add(behaviorTree.taskList.Count);
                        data.parentTask = task as ParentTask;
                        data.parentIndex = parentIndex;
                        if (task is Composite) {
                            data.compositeParentIndex = parentIndex;
                        }
                        behaviorTree.parentCompositeIndex.Add(data.compositeParentIndex);
                        if ((status = AddToTaskList(behaviorTree, parentTask.Children[i], ref hasExternalBehavior, data)) < 0) {
                            if (status == -3) { // invalid task
                                data.errorTask = parentIndex;
                                data.errorTaskName = !string.IsNullOrEmpty(behaviorTree.taskList[data.errorTask].FriendlyName) ?
                                                        behaviorTree.taskList[data.errorTask].FriendlyName : behaviorTree.taskList[data.errorTask].GetType().ToString();
                            }
                            return status;
                        }
                    }
                } else { // the task isn't a parent so it doesn't have any children or conditional status
                    behaviorTree.childrenIndex.Add(null);
                    behaviorTree.childConditionalIndex.Add(null);
                    // mark the parent composite task as having a conditional task
                    if (task is Conditional) {
                        int taskIndex = behaviorTree.taskList.Count - 1;
                        int compositeParent = behaviorTree.parentCompositeIndex[taskIndex];
                        if (compositeParent != -1) {
                            behaviorTree.childConditionalIndex[compositeParent].Add(taskIndex);
                        }
                    }
                }
            }
            return 0;
        }

        private void OverrideFields(BehaviorTree behaviorTree, TaskAddData data, object obj)
        {
            if (obj == null || Equals(obj, null)) {
                return;
            }
            var fields = TaskUtility.GetSerializableFields(obj.GetType());
            for (int i = 0; i < fields.Length; ++i) {
                var value = fields[i].GetValue(obj);
                if (value == null) {
                    continue;
                }
                if (typeof(SharedVariable).IsAssignableFrom(fields[i].FieldType)) {
                    var overrideSharedVariable = OverrideSharedVariable(behaviorTree, data, fields[i].FieldType, value as SharedVariable);
                    if (overrideSharedVariable != null) {
                        fields[i].SetValue(obj, overrideSharedVariable);
                    }
                } else if (typeof(IList).IsAssignableFrom(fields[i].FieldType)) {
                    Type fieldType;
                    if (typeof(SharedVariable).IsAssignableFrom((fieldType = fields[i].FieldType.GetElementType())) ||
#if !UNITY_EDITOR && NETFX_CORE
			            (fields[i].FieldType.GetTypeInfo().IsGenericType && typeof(SharedVariable).IsAssignableFrom((fieldType = fields[i].FieldType.GetGenericArguments()[0])))) {
#else
                        (fields[i].FieldType.IsGenericType && typeof(SharedVariable).IsAssignableFrom((fieldType = fields[i].FieldType.GetGenericArguments()[0])))) {
#endif
                        var list = value as IList<SharedVariable>;
                        if (list != null) {
                            for (int j = 0; j < list.Count; ++j) {
                                var overrideSharedVariable = OverrideSharedVariable(behaviorTree, data, fieldType, list[j]);
                                if (overrideSharedVariable != null) {
                                    list[j] = overrideSharedVariable;
                                }
                            }
                        }
                    }
                }
#if !UNITY_EDITOR && NETFX_CORE
		        else if (fields[i].FieldType.GetTypeInfo().IsClass && !fields[i].FieldType.Equals(typeof(Type)) && !typeof(Delegate).IsAssignableFrom(fields[i].FieldType)) {
#else
                else if (fields[i].FieldType.IsClass && !fields[i].FieldType.Equals(typeof(Type)) && !typeof(Delegate).IsAssignableFrom(fields[i].FieldType)) {
#endif
                    if (!data.overiddenFields.Contains(value)) {
                        data.overiddenFields.Add(value);
                        OverrideFields(behaviorTree, data, value);
                        data.overiddenFields.Remove(value);
                    }
                }
            }
        }

        private SharedVariable OverrideSharedVariable(BehaviorTree behaviorTree, TaskAddData data, Type fieldType, SharedVariable sharedVariable)
        {
            var origSharedVariable = sharedVariable;
            if (sharedVariable is SharedGenericVariable) {
                sharedVariable = ((sharedVariable as SharedGenericVariable).GetValue() as GenericVariable).value;
            } else if (sharedVariable is SharedNamedVariable) {
                sharedVariable = ((sharedVariable as SharedNamedVariable).GetValue() as NamedVariable).value;
            }
            if (sharedVariable == null) {
                return null;
            }
            TaskAddData.OverrideFieldValue overrideField;
            if (!string.IsNullOrEmpty(sharedVariable.Name) && data.overrideFields.TryGetValue(sharedVariable.Name, out overrideField)) {
                SharedVariable overrideSharedVariable = null;
                if (overrideField.Value is SharedVariable) {
                    overrideSharedVariable = overrideField.Value as SharedVariable;
                } else if (overrideField.Value is NamedVariable) {
                    overrideSharedVariable = (overrideField.Value as NamedVariable).value;
                    if (overrideSharedVariable.IsGlobal) {
                        overrideSharedVariable = GlobalVariables.Instance.GetVariable(overrideSharedVariable.Name);
                    } else if (overrideSharedVariable.IsShared) {
                        overrideSharedVariable = behaviorTree.behavior.GetVariable(overrideSharedVariable.Name);
                    }
                } else if (overrideField.Value is GenericVariable) {
                    overrideSharedVariable = (overrideField.Value as GenericVariable).value;
                    if (overrideSharedVariable.IsGlobal) {
                        overrideSharedVariable = GlobalVariables.Instance.GetVariable(overrideSharedVariable.Name);
                    } else if (overrideSharedVariable.IsShared) {
                        overrideSharedVariable = behaviorTree.behavior.GetVariable(overrideSharedVariable.Name);
                    }
                }
                if ((origSharedVariable is SharedNamedVariable) || (origSharedVariable is SharedGenericVariable)) {
                    if ((fieldType.Equals(typeof(SharedVariable)) || overrideSharedVariable.GetType().Equals(sharedVariable.GetType()))) {
                        if (origSharedVariable is SharedNamedVariable) {
                            (origSharedVariable as SharedNamedVariable).Value.value = overrideSharedVariable;
                        } else if (origSharedVariable is SharedGenericVariable) {
                            (origSharedVariable as SharedGenericVariable).Value.value = overrideSharedVariable;
                        }
                        behaviorTree.behavior.SetVariableValue(sharedVariable.Name, overrideSharedVariable.GetValue());
                    }
                } else if (overrideSharedVariable is SharedVariable) {
                    return overrideSharedVariable;
                }
            }
            return null;
        }

        public void DisableBehavior(Behavior behavior)
        {
            DisableBehavior(behavior, false);
        }

        public void DisableBehavior(Behavior behavior, bool paused)
        {
            DisableBehavior(behavior, paused, TaskStatus.Success);
        }

        public void DisableBehavior(Behavior behavior, bool paused, TaskStatus executionStatus)
        {
            if (!IsBehaviorEnabled(behavior)) {
                // Enable the behavior tree and continue with the disable if the behavior tree is currently paused
                if (pausedBehaviorTrees.ContainsKey(behavior) && !paused) {
                    EnableBehavior(behavior);
                } else {
                    return;
                }
            }

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            if (behavior.LogTaskChanges) {
                Debug.Log(string.Format("{0}: {1} {2}", RoundedTime(), (paused ? "Pausing" : "Disabling"), behavior.ToString()));
            }
#endif

            if (paused) {
                BehaviorTree behaviorTree;
                if (!behaviorTreeMap.TryGetValue(behavior, out behaviorTree)) {
                    return;
                }

                if (!pausedBehaviorTrees.ContainsKey(behavior)) {
                    pausedBehaviorTrees.Add(behavior, behaviorTree);
                    behavior.ExecutionStatus = TaskStatus.Inactive;

                    for (int i = 0; i < behaviorTree.taskList.Count; ++i) {
                        behaviorTree.taskList[i].OnPause(true);
                    }

                    behaviorTrees.Remove(behaviorTree);
                }
            } else {
                DestroyBehavior(behavior, executionStatus);
            }
        }

        public void DestroyBehavior(Behavior behavior)
        {
            DestroyBehavior(behavior, TaskStatus.Success);
        }

        public void DestroyBehavior(Behavior behavior, TaskStatus executionStatus)
        {
            BehaviorTree behaviorTree;
            if (!behaviorTreeMap.TryGetValue(behavior, out behaviorTree) || behaviorTree.destroyBehavior) {
                return;
            }
            behaviorTree.destroyBehavior = true;
            // Unpause the tree immediately before it is destroyed.
            if (pausedBehaviorTrees.ContainsKey(behavior)) {
                pausedBehaviorTrees.Remove(behavior);
                for (int i = 0; i < behaviorTree.taskList.Count; ++i) {
                    behaviorTree.taskList[i].OnPause(false);
                }
                behavior.ExecutionStatus = TaskStatus.Running;
            }

            // pop all of the tasks so they receive the end callback
            var status = executionStatus;
            for (int i = behaviorTree.activeStack.Count - 1; i > -1; --i) {
                while (behaviorTree.activeStack[i].Count > 0) {
                    int stackCount = behaviorTree.activeStack[i].Count;
                    PopTask(behaviorTree, behaviorTree.activeStack[i].Peek(), i, ref status, true, false);
                    if (stackCount == 1) {
                        break;
                    }
                }
            }

            // Remove all of the conditional aborts that haven't had a chance to run yet
            RemoveChildConditionalReevaluate(behaviorTree, -1);

            for (int i = 0; i < behaviorTree.taskList.Count; ++i) {
                behaviorTree.taskList[i].OnBehaviorComplete();
            }

            behaviorTreeMap.Remove(behavior);
            behaviorTrees.Remove(behaviorTree);
            behaviorTree.destroyBehavior = false;
            ObjectPool.Return(behaviorTree);

            behavior.ExecutionStatus = status;
            behavior.OnBehaviorEnded();
        }

        public void RestartBehavior(Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            // pop all of the tasks so they receive the end callback
            var status = TaskStatus.Success;
            for (int i = behaviorTree.activeStack.Count - 1; i > -1; --i) {
                while (behaviorTree.activeStack[i].Count > 0) {
                    int stackCount = behaviorTree.activeStack[i].Count;
                    PopTask(behaviorTree, behaviorTree.activeStack[i].Peek(), i, ref status, true, false);
                    if (stackCount == 1) {
                        break;
                    }
                }
            }

            // start things again
            Restart(behaviorTree);
        }

        public bool IsBehaviorEnabled(Behavior behavior)
        {
            return behaviorTreeMap != null && behaviorTreeMap.Count > 0 && behavior != null && behavior.ExecutionStatus == TaskStatus.Running;
        }

        public void Update()
        {
            Tick();
        }

        public void LateUpdate()
        {
            for (int i = 0; i < behaviorTrees.Count; ++i) {
                if (behaviorTrees[i].behavior.HasEvent[(int)Behavior.EventTypes.OnLateUpdate]) {
                    for (int j = behaviorTrees[i].activeStack.Count - 1; j > -1; --j) {
                        var taskIndex = behaviorTrees[i].activeStack[j].Peek();
                        behaviorTrees[i].taskList[taskIndex].OnLateUpdate();
                    }
                }
            }
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < behaviorTrees.Count; ++i) {
                if (behaviorTrees[i].behavior.HasEvent[(int)Behavior.EventTypes.OnFixedUpdate]) {
                    for (int j = behaviorTrees[i].activeStack.Count - 1; j > -1; --j) {
                        var taskIndex = behaviorTrees[i].activeStack[j].Peek();
                        behaviorTrees[i].taskList[taskIndex].OnFixedUpdate();
                    }
                }
            }
        }

        private IEnumerator CoroutineUpdate()
        {
            while (true) {
                Tick();
                yield return updateWait;
            }
        }

        // Tick all of the behavior trees
        public void Tick()
        {
            for (int i = 0; i < behaviorTrees.Count; ++i) {
                Tick(behaviorTrees[i]);
            }
        }

        // Manually tick a specific behavior tree
        public void Tick(Behavior behavior)
        {
            if (behavior == null || !IsBehaviorEnabled(behavior)) {
                return;
            }

            Tick(behaviorTreeMap[behavior]);
        }

        private void Tick(BehaviorTree behaviorTree)
        {
            behaviorTree.executionCount = 0;
            ReevaluateParentTasks(behaviorTree);
            ReevaluateConditionalTasks(behaviorTree);

            for (int j = behaviorTree.activeStack.Count - 1; j > -1; --j) {
                // ensure there are no interruptions within the hierarchy
                var status = TaskStatus.Inactive;
                int interruptedTask;
                if (j < behaviorTree.interruptionIndex.Count && (interruptedTask = behaviorTree.interruptionIndex[j]) != -1) {
                    behaviorTree.interruptionIndex[j] = -1;
                    while (behaviorTree.activeStack[j].Peek() != interruptedTask) {
                        int stackCount = behaviorTree.activeStack[j].Count;
                        PopTask(behaviorTree, behaviorTree.activeStack[j].Peek(), j, ref status, true);
                        if (stackCount == 1) {
                            break;
                        }
                    }
                    // pop the interrupt task. Performing a check to be sure the interrupted task is at the top of the stack because the interrupted task
                    // may be in a different stack and the active stack has completely been removed
                    if (j < behaviorTree.activeStack.Count && behaviorTree.activeStack[j].Count > 0 && behaviorTree.taskList[interruptedTask] == behaviorTree.taskList[behaviorTree.activeStack[j].Peek()]) {
                        if (behaviorTree.taskList[interruptedTask] is ParentTask) {
                            status = (behaviorTree.taskList[interruptedTask] as ParentTask).OverrideStatus();
                        }
                        PopTask(behaviorTree, interruptedTask, j, ref status, true);
                    }
                }
                int startIndex = -1;
                int taskIndex;
                while (status != TaskStatus.Running && j < behaviorTree.activeStack.Count && behaviorTree.activeStack[j].Count > 0) {
                    taskIndex = behaviorTree.activeStack[j].Peek();
                    // bail out if the index is the same as what it was before runTask was executed or the behavior is no longer enabled
                    if ((j < behaviorTree.activeStack.Count && behaviorTree.activeStack[j].Count > 0 && startIndex == behaviorTree.activeStack[j].Peek()) || !IsBehaviorEnabled(behaviorTree.behavior)) {
                        break;
                    } else {
                        startIndex = taskIndex;
                    }
                    status = RunTask(behaviorTree, taskIndex, j, status);
                }
            }
        }

        private void ReevaluateConditionalTasks(BehaviorTree behaviorTree)
        {
            // Loop through all of the conditional tasks that are currently being reevaluated
            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                // The task may not be quite ready yet
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }

                int conditionalIndex = behaviorTree.conditionalReevaluate[i].index;
                var conditionalStatus = behaviorTree.taskList[conditionalIndex].OnUpdate();
                // stop the subsequent tasks from running if the status changed
                if (conditionalStatus != behaviorTree.conditionalReevaluate[i].taskStatus) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                    if (behaviorTree.behavior.LogTaskChanges) {
                        int compositeAbort = behaviorTree.parentCompositeIndex[conditionalIndex];
                        print(string.Format("{0}: {1}: Conditional abort with task {2} ({3}, index {4}) because of conditional task {5} ({6}, index {7}) with status {8}",
                                            RoundedTime(), behaviorTree.behavior.ToString(), behaviorTree.taskList[compositeAbort].FriendlyName, behaviorTree.taskList[compositeAbort].GetType(),
                                            compositeAbort, behaviorTree.taskList[conditionalIndex].FriendlyName, behaviorTree.taskList[conditionalIndex].GetType(),
                                            conditionalIndex, conditionalStatus));
                    }
#endif
                    int compositeIndex = behaviorTree.conditionalReevaluate[i].compositeIndex;
                    for (int j = behaviorTree.activeStack.Count - 1; j > -1; --j) {
                        if (behaviorTree.activeStack[j].Count > 0) {
                            int taskIndex = behaviorTree.activeStack[j].Peek();
                            int lcaIndex = FindLCA(behaviorTree, conditionalIndex, taskIndex);
                            // Let the task continue to run if the LCA isn't the composite index or a child of the composite index.
                            // We don't want to pop a branch that isn't affected by the abort
                            if (!IsChild(behaviorTree, lcaIndex, compositeIndex)) {
                                continue;
                            }
                            var stackCount = behaviorTree.activeStack.Count;
                            // Don't pop anymore if there are no more tasks on a particular stack because that stack index has been removed
                            while (taskIndex != -1 && taskIndex != lcaIndex && behaviorTree.activeStack.Count == stackCount) {
                                var status = TaskStatus.Failure;
                                behaviorTree.taskList[taskIndex].OnConditionalAbort();
                                PopTask(behaviorTree, taskIndex, j, ref status, false);
                                taskIndex = behaviorTree.parentIndex[taskIndex];
                            }
                        }
                    }

                    // Remove any conditional tasks within the same stack as well. They will be added again when the task gets pushed again
                    for (int j = behaviorTree.conditionalReevaluate.Count - 1; j > i - 1; --j) {
                        var jConditionalReval = behaviorTree.conditionalReevaluate[j];
                        if (FindLCA(behaviorTree, compositeIndex, jConditionalReval.index) == compositeIndex) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                            behaviorTree.taskList[behaviorTree.conditionalReevaluate[j].index].NodeData.IsReevaluating = false;
#endif
                            ObjectPool.Return(behaviorTree.conditionalReevaluate[j]);
                            behaviorTree.conditionalReevaluateMap.Remove(behaviorTree.conditionalReevaluate[j].index);
                            behaviorTree.conditionalReevaluate.RemoveAt(j);
                        }
                    }

                    // Update the composite index for any tasks that have the same composite parent. The new index should be the child composite index (which has to first be found)
                    // This is done to allow multiple conditional tasks to exist under the same composite task and allow the first conditional task to cause an abort with the abort type
                    // of Lower Priority running
                    var compositeTask = behaviorTree.taskList[behaviorTree.parentCompositeIndex[conditionalIndex]] as Composite;
                    for (int j = i - 1; j > -1; --j) {
                        var jConditionalReval = behaviorTree.conditionalReevaluate[j];
                        // Stop the task reevaluation if it has the same index as task being stopped and the parent composite task has an AbortType of LowerPriority.
                        if (compositeTask.AbortType == AbortType.LowerPriority && behaviorTree.parentCompositeIndex[jConditionalReval.index] == behaviorTree.parentCompositeIndex[conditionalIndex]) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                            behaviorTree.taskList[behaviorTree.conditionalReevaluate[j].index].NodeData.IsReevaluating = false;
#endif
                            behaviorTree.conditionalReevaluate[j].compositeIndex = -1;
                            continue;
                        }
                        if (behaviorTree.parentCompositeIndex[jConditionalReval.index] == behaviorTree.parentCompositeIndex[conditionalIndex]) {
                            for (int k = 0; k < behaviorTree.childrenIndex[compositeIndex].Count; ++k) {
                                if (IsParentTask(behaviorTree, behaviorTree.childrenIndex[compositeIndex][k], jConditionalReval.index)) {
                                    // The child index has to be a composite task
                                    var childIndex = behaviorTree.childrenIndex[compositeIndex][k];
                                    while (!(behaviorTree.taskList[childIndex] is Composite)) {
                                        if (behaviorTree.childrenIndex[childIndex] != null) {
                                            childIndex = behaviorTree.childrenIndex[childIndex][0]; // Decorator tasks can only have one child
                                        } else {
                                            break;
                                        }
                                    }
                                    if (behaviorTree.taskList[childIndex] is Composite) {
                                        jConditionalReval.compositeIndex = childIndex;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    // get a listing of all of the parents, starting with the parent at the highest level
                    conditionalParentIndexes.Clear();
                    int parentIndex = behaviorTree.parentIndex[conditionalIndex];
                    while (parentIndex != compositeIndex) {
                        conditionalParentIndexes.Add(parentIndex);
                        parentIndex = behaviorTree.parentIndex[parentIndex];
                    }
                    if (conditionalParentIndexes.Count == 0) {
                        conditionalParentIndexes.Add(behaviorTree.parentIndex[conditionalIndex]);
                    }

                    // notify all of the parents of the conditional abort and push the parents
                    var parentTask = behaviorTree.taskList[compositeIndex] as ParentTask;
                    parentTask.OnConditionalAbort(behaviorTree.relativeChildIndex[conditionalParentIndexes[conditionalParentIndexes.Count - 1]]);
                    for (int j = conditionalParentIndexes.Count - 1; j > -1; --j) {
                        parentTask = behaviorTree.taskList[conditionalParentIndexes[j]] as ParentTask;
                        if (j == 0) {
                            parentTask.OnConditionalAbort(behaviorTree.relativeChildIndex[conditionalIndex]);
                        } else {
                            parentTask.OnConditionalAbort(behaviorTree.relativeChildIndex[conditionalParentIndexes[j - 1]]);
                        }
                    }
#if UNITY_EDITOR || DLL_RELEASE || DLL_DEBUG
                    behaviorTree.taskList[conditionalIndex].NodeData.InterruptTime = Time.realtimeSinceStartup;
#endif
                }
            }
        }

        private void ReevaluateParentTasks(BehaviorTree behaviorTree)
        {
            for (int i = behaviorTree.parentReevaluate.Count - 1; i > -1; --i) {
                var parentReevaluateIndex = behaviorTree.parentReevaluate[i];
                if (behaviorTree.taskList[parentReevaluateIndex] is Decorator) {
                    if (behaviorTree.taskList[parentReevaluateIndex].OnUpdate() == TaskStatus.Failure) {
                        Interrupt(behaviorTree.behavior, behaviorTree.taskList[parentReevaluateIndex]);
                    }
                } else if (behaviorTree.taskList[parentReevaluateIndex] is Composite) {
                    var parentReevaluateTask = behaviorTree.taskList[parentReevaluateIndex] as Composite;
                    if (parentReevaluateTask.OnReevaluationStarted()) {
                        int stackIndex = 0;
                        var status = RunParentTask(behaviorTree, parentReevaluateIndex, ref stackIndex, TaskStatus.Inactive);
                        parentReevaluateTask.OnReevaluationEnded(status);
                    }
                }
            }
        }

        private TaskStatus RunTask(BehaviorTree behaviorTree, int taskIndex, int stackIndex, TaskStatus previousStatus)
        {
            var task = behaviorTree.taskList[taskIndex];
            if (task == null)
                return previousStatus;

            // If the task is disabled then return immediately with the previous status. Notify the parent task that the child task finished executing so it will move on to the next child
            if (task.Disabled) {
                if (behaviorTree.behavior.LogTaskChanges) {
                    print(string.Format("{0}: {1}: Skip task {2} ({3}, index {4}) at stack index {5} (task disabled)", RoundedTime(), behaviorTree.behavior.ToString(),
                                            behaviorTree.taskList[taskIndex].FriendlyName, behaviorTree.taskList[taskIndex].GetType(), taskIndex, stackIndex));
                }
                if (behaviorTree.parentIndex[taskIndex] != -1) {
                    var parentTask = behaviorTree.taskList[behaviorTree.parentIndex[taskIndex]] as ParentTask;
                    if (!parentTask.CanRunParallelChildren()) {
                        parentTask.OnChildExecuted(TaskStatus.Inactive);
                    } else {
                        parentTask.OnChildExecuted(behaviorTree.relativeChildIndex[taskIndex], TaskStatus.Inactive);
                        RemoveStack(behaviorTree, stackIndex);
                    }
                }
                return previousStatus;
            }

            var status = previousStatus;
            // If the task is non instant and the task has already completed executing then pop the task
            if (!task.IsInstant && (behaviorTree.nonInstantTaskStatus[stackIndex] == TaskStatus.Failure || behaviorTree.nonInstantTaskStatus[stackIndex] == TaskStatus.Success)) {
                status = behaviorTree.nonInstantTaskStatus[stackIndex];
                PopTask(behaviorTree, taskIndex, stackIndex, ref status, true);
                return status;
            }
            PushTask(behaviorTree, taskIndex, stackIndex);
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            if (breakpointTree != null) {
                return TaskStatus.Running;
            }
#endif

            if (task is ParentTask) {
                var parentTask = task as ParentTask;
                status = RunParentTask(behaviorTree, taskIndex, ref stackIndex, status);

                // let the parent task override the children status. The last child task could fail immediately and we don't want that to represent the entire task
                status = parentTask.OverrideStatus(status);
            } else {
                status = task.OnUpdate();
            }

            if (status != TaskStatus.Running) {
                // pop the task immediately if the task is instant. If the task is not instant then wait for the next update
                if (task.IsInstant) {
                    PopTask(behaviorTree, taskIndex, stackIndex, ref status, true);
                } else {
                    behaviorTree.nonInstantTaskStatus[stackIndex] = status;
                }
            }

            return status;
        }

        private TaskStatus RunParentTask(BehaviorTree behaviorTree, int taskIndex, ref int stackIndex, TaskStatus status)
        {
            var parentTask = behaviorTree.taskList[taskIndex] as ParentTask;
            if (!parentTask.CanRunParallelChildren() || parentTask.OverrideStatus(TaskStatus.Running) != TaskStatus.Running) {
                var childStatus = TaskStatus.Inactive;
                // nest within a while loop so multiple child tasks can be run within a single update loop (such as conditions)
                // also, if the parent is a parallel task, start running all of the children
                int parentStack = stackIndex;
                int prevChildIndex = -1;
                Behavior activeBehavior = behaviorTree.behavior;
                while (parentTask.CanExecute() && (childStatus != TaskStatus.Running || parentTask.CanRunParallelChildren()) && IsBehaviorEnabled(activeBehavior)) {
                    var childrenIndexes = behaviorTree.childrenIndex[taskIndex];
                    int childIndex = parentTask.CurrentChildIndex();
                    // bail out if the child index is the same as what it was before runTask was executed or if the execution count is too large. This will prevent an infinite loop.
                    if ((executionsPerTick == ExecutionsPerTickType.NoDuplicates && childIndex == prevChildIndex) ||
                        (executionsPerTick == ExecutionsPerTickType.Count && behaviorTree.executionCount >= maxTaskExecutionsPerTick)) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                        if (executionsPerTick == ExecutionsPerTickType.Count) {
                            Debug.LogWarning(string.Format("{0}: {1}: More than the specified number of task executions per tick ({2}) have executed, returning early.",
                                                                RoundedTime(), behaviorTree.behavior.ToString(), maxTaskExecutionsPerTick));
                        }
#endif
                        status = TaskStatus.Running;
                        break;
                    }
                    prevChildIndex = childIndex;
                    if (parentTask.CanRunParallelChildren()) {
                        // need to create a new stack level
                        behaviorTree.activeStack.Add(ObjectPool.Get<Stack<int>>());
                        behaviorTree.interruptionIndex.Add(-1);
                        behaviorTree.nonInstantTaskStatus.Add(TaskStatus.Inactive);
                        stackIndex = behaviorTree.activeStack.Count - 1;
                        parentTask.OnChildStarted(childIndex);
                    } else {
                        parentTask.OnChildStarted();
                    }
                    status = childStatus = RunTask(behaviorTree, childrenIndexes[childIndex], stackIndex, status);
                }
                stackIndex = parentStack;
            }
            return status;
        }

        private void PushTask(BehaviorTree behaviorTree, int taskIndex, int stackIndex)
        {
            if (!IsBehaviorEnabled(behaviorTree.behavior) || stackIndex >= behaviorTree.activeStack.Count) {
                return;
            }

            var activeStack = behaviorTree.activeStack[stackIndex];
            if (activeStack.Count == 0 || activeStack.Peek() != taskIndex) {
                activeStack.Push(taskIndex);
                behaviorTree.nonInstantTaskStatus[stackIndex] = TaskStatus.Running;
                behaviorTree.executionCount++;
                var task = behaviorTree.taskList[taskIndex];
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                task.NodeData.PushTime = Time.realtimeSinceStartup;
                task.NodeData.ExecutionStatus = TaskStatus.Running;
                if (task.NodeData.IsBreakpoint) {
                    // let behavior designer know
                    if (onTaskBreakpoint != null) {
                        breakpointTree = behaviorTree.behavior;
                        onTaskBreakpoint();
                    }
                }
                
                if (behaviorTree.behavior.LogTaskChanges) {
                    print(string.Format("{0}: {1}: Push task {2} ({3}, index {4}) at stack index {5}", RoundedTime(), behaviorTree.behavior.ToString(),
                                    task.FriendlyName, task.GetType(), taskIndex, stackIndex));
                }
#endif
                task.OnStart();

                if (task is ParentTask) {
                    var parentTask = task as ParentTask;
                    if (parentTask.CanReevaluate()) {
                        behaviorTree.parentReevaluate.Add(taskIndex);
                    }
                }
            }
        }

        private void PopTask(BehaviorTree behaviorTree, int taskIndex, int stackIndex, ref TaskStatus status, bool popChildren)
        {
            PopTask(behaviorTree, taskIndex, stackIndex, ref status, popChildren, true);
        }

        private void PopTask(BehaviorTree behaviorTree, int taskIndex, int stackIndex, ref TaskStatus status, bool popChildren, bool notifyOnEmptyStack)
        {
            // return immediately if the behavior tree isn't enabled or the stack index is larger then the number of items on the stack.
            // this latter case can happen if you restart the behavior tree within a task.
            if (!IsBehaviorEnabled(behaviorTree.behavior) || stackIndex >= behaviorTree.activeStack.Count ||
                        behaviorTree.activeStack[stackIndex].Count == 0 || taskIndex != behaviorTree.activeStack[stackIndex].Peek()) {
                return;
            }

            behaviorTree.activeStack[stackIndex].Pop();
            behaviorTree.nonInstantTaskStatus[stackIndex] = TaskStatus.Inactive;
            // notify any third party plugin that the task has stopped
            StopThirdPartyTask(behaviorTree, taskIndex);
            var task = behaviorTree.taskList[taskIndex];
            task.OnEnd();

            int parentIndex = behaviorTree.parentIndex[taskIndex];
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            task.NodeData.PushTime = -1;
            task.NodeData.PopTime = Time.realtimeSinceStartup;
            task.NodeData.ExecutionStatus = status;
            if (behaviorTree.behavior.LogTaskChanges) {
                print(string.Format("{0}: {1}: Pop task {2} ({3}, index {4}) at stack index {5} with status {6}", RoundedTime(), behaviorTree.behavior.ToString(),
                                    task.FriendlyName, task.GetType(), taskIndex, stackIndex, status));
            }
#endif

            // let the parent know
            if (parentIndex != -1) {
                if (task is Conditional) {
                    int compositeParentIndex = behaviorTree.parentCompositeIndex[taskIndex];
                    if (compositeParentIndex != -1) {
                        var compositeTask = behaviorTree.taskList[compositeParentIndex] as Composite;
                        if (compositeTask.AbortType != AbortType.None) {
                            // The key may already exist if the conditional abort is set to LowerPriority and a decorator started the same branch again.
                            // This will cause the LowerPriority abort to never be removed and it still existing on the conditionalReevaluateMap (ConditionalAbortTest32).
                            BehaviorTree.ConditionalReevaluate conditionalReevaluate;
                            if (behaviorTree.conditionalReevaluateMap.TryGetValue(taskIndex, out conditionalReevaluate)) {
                                conditionalReevaluate.compositeIndex = (compositeTask.AbortType != AbortType.LowerPriority ? compositeParentIndex : -1);
                                conditionalReevaluate.taskStatus = status;
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                                task.NodeData.IsReevaluating = (compositeTask.AbortType != AbortType.LowerPriority ? true : false);
#endif
                            } else {
                                // The abort type will be self until the composite task is popped
                                var conditionalReval = ObjectPool.Get<BehaviorTree.ConditionalReevaluate>();
                                conditionalReval.Initialize(taskIndex, status, stackIndex, (compositeTask.AbortType != AbortType.LowerPriority ? compositeParentIndex : -1));
                                behaviorTree.conditionalReevaluate.Add(conditionalReval);
                                behaviorTree.conditionalReevaluateMap.Add(taskIndex, conditionalReval);
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                                task.NodeData.IsReevaluating = compositeTask.AbortType == AbortType.Self || compositeTask.AbortType == AbortType.Both;
#endif
                            }
                        }
                    }
                }
                var parentTask = behaviorTree.taskList[parentIndex] as ParentTask;
                if (!parentTask.CanRunParallelChildren()) {
                    parentTask.OnChildExecuted(status);
                    status = parentTask.Decorate(status);
                } else {
                    parentTask.OnChildExecuted(behaviorTree.relativeChildIndex[taskIndex], status);
                }
            }

            if (task is ParentTask) {
                var parentTask = task as ParentTask;
                if (parentTask.CanReevaluate()) {
                    for (int i = behaviorTree.parentReevaluate.Count - 1; i > -1; --i) {
                        if (behaviorTree.parentReevaluate[i] == taskIndex) {
                            behaviorTree.parentReevaluate.RemoveAt(i);
                            break;
                        }
                    }
                }

                if (parentTask is Composite) {
                    var compositeTask = parentTask as Composite;

                    // no longer observing if the type is self or there are no more tasks in the stack
                    if (compositeTask.AbortType == AbortType.Self || compositeTask.AbortType == AbortType.None || behaviorTree.activeStack[stackIndex].Count == 0) {
                        RemoveChildConditionalReevaluate(behaviorTree, taskIndex);
                    } else if (compositeTask.AbortType == AbortType.LowerPriority || compositeTask.AbortType == AbortType.Both) {
                        int parentCompositeIndex = behaviorTree.parentCompositeIndex[taskIndex];
                        if (parentCompositeIndex != -1) {
                            // If the parent is a parallel task then all children aborts should be removed to prevent the abort from triggering when the task is inactive.
                            if (!(behaviorTree.taskList[parentCompositeIndex] as ParentTask).CanRunParallelChildren()) {
                                // the conditional task now becomes active so it will be reevaluated
                                for (int i = 0; i < behaviorTree.childConditionalIndex[taskIndex].Count; ++i) {
                                    int conditionalIndex = behaviorTree.childConditionalIndex[taskIndex][i];
                                    // the key may not exist if the stack is empty
                                    BehaviorTree.ConditionalReevaluate conditionalReevaluate;
                                    if (behaviorTree.conditionalReevaluateMap.TryGetValue(conditionalIndex, out conditionalReevaluate)) {
                                        // Do not update the composite index if the parent composite a parallel task. Parallel tasks should not reevaluate a separate child.
                                        if (!(behaviorTree.taskList[parentCompositeIndex] as ParentTask).CanRunParallelChildren()) {
                                            conditionalReevaluate.compositeIndex = behaviorTree.parentCompositeIndex[taskIndex];
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                                            behaviorTree.taskList[conditionalIndex].NodeData.IsReevaluating = true;
#endif
                                        } else {
                                            // Remove any conditional tasks within the same stack as well. They will be added again when the task gets pushed again
                                            for (int j = behaviorTree.conditionalReevaluate.Count - 1; j > i - 1; --j) {
                                                var jConditionalReval = behaviorTree.conditionalReevaluate[j];
                                                if (FindLCA(behaviorTree, parentCompositeIndex, jConditionalReval.index) == parentCompositeIndex) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                                                    behaviorTree.taskList[behaviorTree.conditionalReevaluate[j].index].NodeData.IsReevaluating = false;
#endif
                                                    ObjectPool.Return(behaviorTree.conditionalReevaluate[j]);
                                                    behaviorTree.conditionalReevaluateMap.Remove(behaviorTree.conditionalReevaluate[j].index);
                                                    behaviorTree.conditionalReevaluate.RemoveAt(j);
                                                }
                                            }
                                        }
                                    }
                                }
                            } else {
                                RemoveChildConditionalReevaluate(behaviorTree, taskIndex);
                            }
                        }
                        // Update the composite index with the parent composite so the correct LCA between the active task and the conditional task will be found
                        for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                            if (behaviorTree.conditionalReevaluate[i].compositeIndex == taskIndex) {
                                behaviorTree.conditionalReevaluate[i].compositeIndex = behaviorTree.parentCompositeIndex[taskIndex];
                            }
                        }
                    }
                }
            }

            // pop any task whose base parent is equal to the base parent of the current task being popped
            if (popChildren) {
                for (int i = behaviorTree.activeStack.Count - 1; i > stackIndex; --i) {
                    if (behaviorTree.activeStack[i].Count > 0) {
                        if (IsParentTask(behaviorTree, taskIndex, behaviorTree.activeStack[i].Peek())) {
                            var childStatus = TaskStatus.Failure;
                            int stackCount = behaviorTree.activeStack[i].Count;
                            while (stackCount > 0) {
                                PopTask(behaviorTree, behaviorTree.activeStack[i].Peek(), i, ref childStatus, false, notifyOnEmptyStack);
                                stackCount--;
                            }
                        }
                    }
                }
            }

            // If there are no more items in the stack, restart the tree (in the case of the root task) or remove the stack created by the parallel task
            if (stackIndex < behaviorTree.activeStack.Count && behaviorTree.activeStack[stackIndex].Count == 0) {
                if (stackIndex == 0) {
                    // restart the tree
                    if (notifyOnEmptyStack) {
                        if (behaviorTree.behavior.RestartWhenComplete) {
                            Restart(behaviorTree);
                        } else {
                            DisableBehavior(behaviorTree.behavior, false, status);
                        }
                    }
                    status = TaskStatus.Inactive;
                } else {
                    // don't remove the stack from the very first index
                    RemoveStack(behaviorTree, stackIndex);

                    // set the status to running to prevent the loop from running again within Update
                    status = TaskStatus.Running;
                }
            }
        }

        private void RemoveChildConditionalReevaluate(BehaviorTree behaviorTree, int compositeIndex)
        {
            for (int i = behaviorTree.conditionalReevaluate.Count - 1; i > -1; --i) {
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == compositeIndex) {
                    ObjectPool.Return(behaviorTree.conditionalReevaluate[i]);
                    int conditionalIndex = behaviorTree.conditionalReevaluate[i].index;
                    behaviorTree.conditionalReevaluateMap.Remove(conditionalIndex);
                    behaviorTree.conditionalReevaluate.RemoveAt(i);
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                    behaviorTree.taskList[conditionalIndex].NodeData.IsReevaluating = false;
#endif
                }
            }
        }

        private void Restart(BehaviorTree behaviorTree)
        {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            if (behaviorTree.behavior.LogTaskChanges) {
                Debug.Log(string.Format("{0}: Restarting {1}", RoundedTime(), behaviorTree.behavior.ToString()));
            }
#endif
            // Remove all of the conditional aborts that haven't had a chance to run yet
            RemoveChildConditionalReevaluate(behaviorTree, -1);

            if (behaviorTree.behavior.ResetValuesOnRestart) {
                behaviorTree.behavior.SaveResetValues();
            }

            for (int i = 0; i < behaviorTree.taskList.Count; ++i) {
                behaviorTree.taskList[i].OnBehaviorRestart();
            }

            behaviorTree.behavior.OnBehaviorRestarted();

            PushTask(behaviorTree, 0, 0);
        }

        // returns if possibleParent is a parent of possibleChild
        private bool IsParentTask(BehaviorTree behaviorTree, int possibleParent, int possibleChild)
        {
            int parentIndex = 0;
            int childIndex = possibleChild;
            while (childIndex != -1) {
                parentIndex = behaviorTree.parentIndex[childIndex];
                if (parentIndex == possibleParent) {
                    return true;
                }
                childIndex = parentIndex;
            }
            return false;
        }

        public void Interrupt(Behavior behavior, Task task)
        {
            Interrupt(behavior, task, task);
        }

        // a task has been interrupted. Store the interrupted index so the update loop knows to stop executing tasks with a parent task equal to the interrupted task
        public void Interrupt(Behavior behavior, Task task, Task interruptionTask)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            // determine the index of the task that is causing the interruption
            int interruptionIndex = -1;
            var behaviorTree = behaviorTreeMap[behavior];
            for (int i = 0; i < behaviorTree.taskList.Count; ++i) {
                if (behaviorTree.taskList[i].ReferenceID == task.ReferenceID) {
                    interruptionIndex = i;
                    break;
                }
            }

            if (interruptionIndex > -1) {
                // loop through the active tasks. Mark any stack that has interruption index as its parent
                int taskIndex;
                for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
                    if (behaviorTree.activeStack[i].Count > 0) {
                        taskIndex = behaviorTree.activeStack[i].Peek();
                        while (taskIndex != -1) {
                            if (taskIndex == interruptionIndex) {
                                behaviorTree.interruptionIndex[i] = interruptionIndex;
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                                if (behavior.LogTaskChanges) {
                                    Debug.Log(string.Format("{0}: {1}: Interrupt task {2} ({3}) with index {4} at stack index {5}", RoundedTime(), behaviorTree.behavior.ToString(),
                                                            task.FriendlyName, task.GetType().ToString(), interruptionIndex, i));
                                }
                                interruptionTask.NodeData.InterruptTime = Time.realtimeSinceStartup;
#endif
                                break;
                            }
                            taskIndex = behaviorTree.parentIndex[taskIndex];
                        }
                    }
                }
            }
        }

        public void StopThirdPartyTask(BehaviorTree behaviorTree, int taskIndex)
        {
            // stop the third party task if it is running
            thirdPartyTaskCompare.Task = behaviorTree.taskList[taskIndex];
            object thirdPartyObject;
            if (taskObjectMap.TryGetValue(thirdPartyTaskCompare, out thirdPartyObject)) {
                var thirdPartyObjectType = objectTaskMap[thirdPartyObject].ThirdPartyObjectType;
                if (invokeParameters == null) {
                    invokeParameters = new object[1];
                }
                invokeParameters[0] = behaviorTree.taskList[taskIndex];
                switch (thirdPartyObjectType) {
                    case ThirdPartyObjectType.PlayMaker:
                        PlayMakerStopMethod.Invoke(null, invokeParameters);
                        break;
                    case ThirdPartyObjectType.uScript:
                        UScriptStopMethod.Invoke(null, invokeParameters);
                        break;
                    case ThirdPartyObjectType.DialogueSystem:
                        DialogueSystemStopMethod.Invoke(null, invokeParameters);
                        break;
                    case ThirdPartyObjectType.uSequencer:
                        USequencerStopMethod.Invoke(null, invokeParameters);
                        break;
                    case ThirdPartyObjectType.ICode:
                        ICodeStopMethod.Invoke(null, invokeParameters);
                        break;
                }

                RemoveActiveThirdPartyTask(behaviorTree.taskList[taskIndex]);
            }
        }

        public void RemoveActiveThirdPartyTask(Task task)
        {
            thirdPartyTaskCompare.Task = task;
            object thirdPartyObject;
            if (taskObjectMap.TryGetValue(thirdPartyTaskCompare, out thirdPartyObject)) {
                ObjectPool.Return(thirdPartyObject);
                taskObjectMap.Remove(thirdPartyTaskCompare);
                objectTaskMap.Remove(thirdPartyObject);
            }
        }

        // remove the stack at stackIndex
        private void RemoveStack(BehaviorTree behaviorTree, int stackIndex)
        {
            var stack = behaviorTree.activeStack[stackIndex];
            stack.Clear();
            ObjectPool.Return(stack);
            behaviorTree.activeStack.RemoveAt(stackIndex);
            behaviorTree.interruptionIndex.RemoveAt(stackIndex);
            behaviorTree.nonInstantTaskStatus.RemoveAt(stackIndex);
        }

        // Find the LCA of the two tasks
        private int FindLCA(BehaviorTree behaviorTree, int taskIndex1, int taskIndex2)
        {
            var set = ObjectPool.Get<HashSet<int>>();
            set.Clear();
            int parentIndex = taskIndex1;
            while (parentIndex != -1) {
                set.Add(parentIndex);
                parentIndex = behaviorTree.parentIndex[parentIndex];
            }

            parentIndex = taskIndex2;
            while (!set.Contains(parentIndex)) {
                parentIndex = behaviorTree.parentIndex[parentIndex];
            }
            ObjectPool.Return(set);

            return parentIndex;
        }

        // Is taskIndex1 a child of taskIndex2?
        private bool IsChild(BehaviorTree behaviorTree, int taskIndex1, int taskIndex2)
        {
            int parentIndex = taskIndex1;
            while (parentIndex != -1) {
                if (parentIndex == taskIndex2) {
                    return true;
                }
                parentIndex = behaviorTree.parentIndex[parentIndex];
            }
            return false;
        }

        public List<Task> GetActiveTasks(Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return null;
            }

            var activeTasks = new List<Task>();
            var behaviorTree = behaviorTreeMap[behavior];
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
                var task = behaviorTree.taskList[behaviorTree.activeStack[i].Peek()];
                if (!(task is BehaviorDesigner.Runtime.Tasks.Action)) {
                    continue;
                }
                activeTasks.Add(task);
            }
            return activeTasks;
        }

        // Forward the collision/trigger callback to the active task
        public void BehaviorOnCollisionEnter(Collision collision, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnCollisionEnter(collision);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnCollisionEnter(collision);
            }
        }

        public void BehaviorOnCollisionExit(Collision collision, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnCollisionExit(collision);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnCollisionExit(collision);
            }
        }

        public void BehaviorOnTriggerEnter(Collider other, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
#if UNITY_EDITOR || DLL_RELEASE || DLL_DEBUG
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
#endif
                    behaviorTree.taskList[taskIndex].OnTriggerEnter(other);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnTriggerEnter(other);
            }
        }

        public void BehaviorOnTriggerExit(Collider other, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnTriggerExit(other);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnTriggerExit(other);
            }
        }

        public void BehaviorOnCollisionEnter2D(Collision2D collision, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnCollisionEnter2D(collision);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnCollisionEnter2D(collision);
            }
        }

        public void BehaviorOnCollisionExit2D(Collision2D collision, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnCollisionExit2D(collision);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnCollisionExit2D(collision);
            }
        }

        public void BehaviorOnTriggerEnter2D(Collider2D other, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnTriggerEnter2D(other);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnTriggerEnter2D(other);
            }
        }

        public void BehaviorOnTriggerExit2D(Collider2D other, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnTriggerExit2D(other);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnTriggerExit2D(other);
            }
        }

        public void BehaviorOnControllerColliderHit(ControllerColliderHit hit, Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnControllerColliderHit(hit);
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnControllerColliderHit(hit);
            }
        }

        public void BehaviorOnAnimatorIK(Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            int taskIndex;
            for (int i = 0; i < behaviorTree.activeStack.Count; ++i) {
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
                // There won't be an active task if the first task of the stack is disabled
                if (behaviorTree.activeStack[i].Count == 0) {
                    continue;
                }
#endif
                taskIndex = behaviorTree.activeStack[i].Peek();
                while (taskIndex != -1) {
                    if (behaviorTree.taskList[taskIndex].Disabled) {
                        break;
                    }
                    behaviorTree.taskList[taskIndex].OnAnimatorIK();
                    taskIndex = behaviorTree.parentIndex[taskIndex];
                }
            }

            for (int i = 0; i < behaviorTree.conditionalReevaluate.Count; ++i) {
                taskIndex = behaviorTree.conditionalReevaluate[i].index;
                if (behaviorTree.taskList[taskIndex].Disabled) {
                    continue;
                }
                if (behaviorTree.conditionalReevaluate[i].compositeIndex == -1) {
                    continue;
                }
                behaviorTree.taskList[taskIndex].OnAnimatorIK();
            }
        }

        // third party support
        public bool MapObjectToTask(object objectKey, Task task, ThirdPartyObjectType objectType)
        {
            if (objectTaskMap.ContainsKey(objectKey)) {
                string thirdPartyName = "";
                switch (objectType) {
                    case ThirdPartyObjectType.PlayMaker:
                        thirdPartyName = "PlayMaker FSM";
                        break;
                    case ThirdPartyObjectType.uScript:
                        thirdPartyName = "uScript Graph";
                        break;
                    case ThirdPartyObjectType.DialogueSystem:
                        thirdPartyName = "Dialogue System";
                        break;
                    case ThirdPartyObjectType.uSequencer:
                        thirdPartyName = "uSequencer sequence";
                        break;
                    case ThirdPartyObjectType.ICode:
                        thirdPartyName = "ICode state machine";
                        break;
                }
                Debug.LogError(string.Format("Only one behavior can be mapped to the same instance of the {0}.", thirdPartyName));
                return false;
            }
            var thirdPartyTask = ObjectPool.Get<ThirdPartyTask>();
            thirdPartyTask.Initialize(task, objectType);
            objectTaskMap.Add(objectKey, thirdPartyTask);
            taskObjectMap.Add(thirdPartyTask, objectKey);
            return true;
        }

        public Task TaskForObject(object objectKey)
        {
            ThirdPartyTask thirdPartyTask;
            if (!objectTaskMap.TryGetValue(objectKey, out thirdPartyTask)) {
                return null;
            }
            return thirdPartyTask.Task;
        }

        private decimal RoundedTime()
        {
            return Math.Round((decimal)Time.time, 5, MidpointRounding.AwayFromZero);
        }

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
        public List<Task> GetTaskList(Behavior behavior)
        {
            if (!IsBehaviorEnabled(behavior)) {
                return null;
            }

            var behaviorTree = behaviorTreeMap[behavior];
            return behaviorTree.taskList;
        }
#endif
    }
}