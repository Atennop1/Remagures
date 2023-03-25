#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class NodeData
    {
        // a reference to the node designer when in the editor
        [SerializeField]
        private object nodeDesigner;
        public object NodeDesigner { get { return nodeDesigner; } set { nodeDesigner = value; } }

        // the offset within the graph
        [SerializeField]
        private Vector2 offset;
        public Vector2 Offset { get { return offset; } set { offset = value; } }

        // deprecated with 1.3.1
        [SerializeField]
        private string friendlyName = "";
        public string FriendlyName { get { return friendlyName; } set { friendlyName = value; } }

        [SerializeField]
        private string comment = "";
        public string Comment { get { return comment; } set { comment = value; } }

        // is the current task a breakpoint
        [SerializeField]
        private bool isBreakpoint = false;
        public bool IsBreakpoint { get { return isBreakpoint; } set { isBreakpoint = value; } }

        [SerializeField]
        private Texture icon;
        public Texture Icon { get { return icon; } set { icon = value; } }

        [SerializeField]
        private bool collapsed = false;
        public bool Collapsed { get { return collapsed; } set { collapsed = value; } }

        [SerializeField]
        private int colorIndex = 0;
        public int ColorIndex { get { return colorIndex; } set { colorIndex = value; } }

        // keep a separate list of the fields that will serialize properly
        [SerializeField]
        private List<string> watchedFieldNames = null;
        public List<String> WatchedFieldNames { get { return watchedFieldNames; } set { watchedFieldNames = value; } }
        private List<FieldInfo> watchedFields = null;
        public List<FieldInfo> WatchedFields { get { return watchedFields; } set { watchedFields = value; } }

        // the time that the task was pushed.
        private float pushTime = -1;
        public float PushTime { get { return pushTime; } set { pushTime = value; } }
        // the time that the task was popped.
        private float popTime = -1;
        public float PopTime { get { return popTime; } set { popTime = value; } }
        // The time the interrupt was triggered
        private float interruptTime = -1;
        public float InterruptTime { get { return interruptTime; } set { interruptTime = value; } }
        // reevaluation for conditional aborts
        private bool isReevaluating = false;
        public bool IsReevaluating { get { return isReevaluating; } set { isReevaluating = value; } }

        private TaskStatus executionStatus = TaskStatus.Inactive;
        public TaskStatus ExecutionStatus { get { return executionStatus; } set { executionStatus = value; } }

        public void InitWatchedFields(Task task)
        {
            if (watchedFieldNames != null && watchedFieldNames.Count > 0) {
                watchedFields = new List<FieldInfo>();
                for (int i = 0; i < watchedFieldNames.Count; ++i) {
                    var field = task.GetType().GetField(watchedFieldNames[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (field != null) {
                        watchedFields.Add(field);
                    }
                }
            }
        }

        public void CopyFrom(NodeData nodeData, Task task)
        {
            nodeDesigner = nodeData.NodeDesigner;
            offset = nodeData.Offset;
            comment = nodeData.Comment;
            isBreakpoint = nodeData.IsBreakpoint;
            collapsed = nodeData.Collapsed;
            if (nodeData.WatchedFields != null && nodeData.WatchedFields.Count > 0) {
                watchedFields = new List<FieldInfo>();
                watchedFieldNames = new List<string>();
                for (int i = 0; i < nodeData.watchedFields.Count; ++i) {
                    var field = task.GetType().GetField(nodeData.WatchedFields[i].Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (field != null) {
                        watchedFields.Add(field);
                        watchedFieldNames.Add(field.Name);
                    }
                }
            }
        }

        public bool ContainsWatchedField(FieldInfo field)
        {
            return (watchedFields != null && watchedFields.Contains(field));
        }

        public void AddWatchedField(FieldInfo field)
        {
            if (watchedFields == null) {
                watchedFields = new List<FieldInfo>();
                watchedFieldNames = new List<string>();
            }

            watchedFields.Add(field);
            watchedFieldNames.Add(field.Name);
        }

        public void RemoveWatchedField(FieldInfo field)
        {
            if (watchedFields != null) {
                watchedFields.Remove(field);
                watchedFieldNames.Remove(field.Name);
            }
        }

        private static Vector2 StringToVector2(string vector2String)
        {
            var stringSplit = vector2String.Substring(1, vector2String.Length - 2).Split(',');
            return new Vector3(float.Parse(stringSplit[0]), float.Parse(stringSplit[1]));
        }
    }
}
#endif