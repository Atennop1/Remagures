using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
#if NETFX_CORE && !UNITY_EDITOR
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
#else
using System.Security.Cryptography;
#endif
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public static class BinaryDeserialization
{
    private static GlobalVariables globalVariables = null;
    private class ObjectFieldMap
    {
        public ObjectFieldMap(object o, FieldInfo f) { obj = o; fieldInfo = f; }
        public object obj;
        public FieldInfo fieldInfo;
    }
    private class ObjectFieldMapComparer : IEqualityComparer<ObjectFieldMap>
    {
        public bool Equals(ObjectFieldMap a, ObjectFieldMap b)
        {
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return false;
            return a.obj.Equals(b.obj) && a.fieldInfo.Equals(b.fieldInfo);
        }

        public int GetHashCode(ObjectFieldMap a)
        {
            return a != null ? (a.obj.ToString().GetHashCode() + a.fieldInfo.ToString().GetHashCode()) : 0;
        }
    }
    private static Dictionary<ObjectFieldMap, List<int>> taskIDs = null;

#if !NETFX_CORE || UNITY_EDITOR
    private static SHA1 shaHash;
#endif
    private static bool updatedSerialization; // 1.5.7
    private static bool shaHashSerialization; // 1.5.9
    private static bool strHashSerialization; // 1.5.11
    private static int animationCurveAdvance = 20;

    private static byte[] sBigEndianFourByteArray;
    private static byte[] sBigEndianEightByteArray;

    private static byte[] BigEndianFourByteArray { get { if (sBigEndianFourByteArray == null) { sBigEndianFourByteArray = new byte[4]; } return sBigEndianFourByteArray; } set { sBigEndianFourByteArray = value; } }
    private static byte[] BigEndianEightByteArray { get { if (sBigEndianEightByteArray == null) { sBigEndianEightByteArray = new byte[8]; } return sBigEndianEightByteArray; } set { sBigEndianEightByteArray = value; } }


    public static void Load(BehaviorSource behaviorSource)
    {
        Load(behaviorSource.TaskData, behaviorSource);
    }

    public static void Load(TaskSerializationData taskData, BehaviorSource behaviorSource)
    {
        behaviorSource.EntryTask = null;
        behaviorSource.RootTask = null;
        behaviorSource.DetachedTasks = null;
        behaviorSource.Variables = null;

        var taskSerializationData = taskData;

        FieldSerializationData fieldSerializationData;
        if (taskSerializationData == null || (fieldSerializationData = taskSerializationData.fieldSerializationData).byteData == null || fieldSerializationData.byteData.Count == 0) {
            return;
        }

        fieldSerializationData.byteDataArray = fieldSerializationData.byteData.ToArray();
        taskIDs = null;
        var treeVersion = new Version(taskData.Version);
        updatedSerialization = treeVersion.CompareTo(new Version("1.5.7")) >= 0;
        shaHashSerialization = strHashSerialization = false;
        if (updatedSerialization) {
            shaHashSerialization = treeVersion.CompareTo(new Version("1.5.9")) >= 0;
            if (shaHashSerialization) {
                strHashSerialization = treeVersion.CompareTo(new Version("1.5.11")) >= 0;
                if (strHashSerialization) {
                    animationCurveAdvance = treeVersion.CompareTo(new Version("1.5.12")) >= 0 ? 16 : 20;
                }
            }
        }

        if (taskSerializationData.variableStartIndex != null) {
            var variables = new List<SharedVariable>();
            var fieldIndexMap = ObjectPool.Get<Dictionary<int, int>>();
            for (int i = 0; i < taskSerializationData.variableStartIndex.Count; ++i) {
                int startIndex = taskSerializationData.variableStartIndex[i];
                int endIndex;
                if (i + 1 < taskSerializationData.variableStartIndex.Count) {
                    endIndex = taskSerializationData.variableStartIndex[i + 1];
                } else {
                    // tasks are added after the variables
                    if (taskSerializationData.startIndex != null && taskSerializationData.startIndex.Count > 0) {
                        endIndex = taskSerializationData.startIndex[0];
                    } else {
                        endIndex = fieldSerializationData.startIndex.Count;
                    }
                }
                // build a dictionary based off of the saved fields
                fieldIndexMap.Clear();
                for (int j = startIndex; j < endIndex; ++j) {
                    fieldIndexMap.Add(fieldSerializationData.fieldNameHash[j], fieldSerializationData.startIndex[j]);
                }

                var sharedVariable = BytesToSharedVariable(fieldSerializationData, fieldIndexMap, fieldSerializationData.byteDataArray, taskSerializationData.variableStartIndex[i], behaviorSource, false, 0);
                if (sharedVariable != null) {
                    variables.Add(sharedVariable);
                }
            }
            ObjectPool.Return(fieldIndexMap);
            behaviorSource.Variables = variables;
        }

        var taskList = new List<Task>();
        if (taskSerializationData.types != null) {
            for (int i = 0; i < taskSerializationData.types.Count; ++i) {
                LoadTask(taskSerializationData, fieldSerializationData, ref taskList, ref behaviorSource);
            }
        }

        // determine where the tasks are positioned
        if (taskSerializationData.parentIndex.Count != taskList.Count) {
            Debug.LogError("Deserialization Error: parent index count does not match task list count");
            return;
        }

        // Determine where the task is positioned
        for (int i = 0; i < taskSerializationData.parentIndex.Count; ++i) {
            if (taskSerializationData.parentIndex[i] == -1) {
                if (behaviorSource.EntryTask == null) { // the first task is always the entry task
                    behaviorSource.EntryTask = taskList[i];
                } else {
                    if (behaviorSource.DetachedTasks == null) {
                        behaviorSource.DetachedTasks = new List<Task>();
                    }
                    behaviorSource.DetachedTasks.Add(taskList[i]);
                }
            } else if (taskSerializationData.parentIndex[i] == 0) { // if the parent is the entry task then assign it as the root task. The entry task isn't a "real" parent task
                behaviorSource.RootTask = taskList[i];
            } else {
                // Add the child to the parent (if the parent index isn't -1)
                if (taskSerializationData.parentIndex[i] != -1) {
                    var parentTask = taskList[taskSerializationData.parentIndex[i]] as ParentTask;
                    if (parentTask != null) {
                        var childIndex = parentTask.Children == null ? 0 : parentTask.Children.Count;
                        parentTask.AddChild(taskList[i], childIndex);
                    }
                }
            }
        }

        if (taskIDs != null) {
            foreach (var objFieldMap in taskIDs.Keys) {
                var ids = taskIDs[objFieldMap] as List<int>;
                var fieldType = objFieldMap.fieldInfo.FieldType;
                if (typeof(IList).IsAssignableFrom(fieldType)) { // array
                    if (fieldType.IsArray) {
                        var elementType = fieldType.GetElementType();
                        var idCount = 0; // The task may be null.
                        for (int i = 0; i < ids.Count; ++i) {
                            var task = taskList[ids[i]];
                            if (!elementType.IsAssignableFrom(task.GetType())) {
                                continue;
                            }
                            idCount++;
                        }
                        var insertIndex = 0;
                        var objectArray = Array.CreateInstance(elementType, idCount);
                        for (int i = 0; i < objectArray.Length; ++i) {
                            var task = taskList[ids[i]];
                            if (!elementType.IsAssignableFrom(task.GetType())) {
                                continue;
                            }
                            objectArray.SetValue(task, insertIndex);
                            insertIndex++;
                        }
                        objFieldMap.fieldInfo.SetValue(objFieldMap.obj, objectArray);
                    } else {
                        var elementType = fieldType.GetGenericArguments()[0];
                        var objectList = TaskUtility.CreateInstance(typeof(List<>).MakeGenericType(elementType)) as IList;
                        for (int i = 0; i < ids.Count; ++i) {
                            var task = taskList[ids[i]];
                            if (!elementType.IsAssignableFrom(task.GetType())) {
                                continue;
                            }
                            objectList.Add(task);
                        }
                        objFieldMap.fieldInfo.SetValue(objFieldMap.obj, objectList);
                    }
                } else {
                    objFieldMap.fieldInfo.SetValue(objFieldMap.obj, taskList[ids[0]]);
                }
            }
        }
    }

    public static void Load(GlobalVariables globalVariables, string version)
    {
        if (globalVariables == null) {
            return;
        }

        globalVariables.Variables = null;
        FieldSerializationData fieldSerializationData;
        if (globalVariables.VariableData == null || (fieldSerializationData = globalVariables.VariableData.fieldSerializationData).byteData == null ||
                                                        fieldSerializationData.byteData.Count == 0) {
            return;
        }

        var variableData = globalVariables.VariableData;
        fieldSerializationData.byteDataArray = fieldSerializationData.byteData.ToArray();
        var variableVersion = new Version(globalVariables.Version);
        updatedSerialization = variableVersion.CompareTo(new Version("1.5.7")) >= 0;
        shaHashSerialization = strHashSerialization = false;
        if (updatedSerialization) {
            shaHashSerialization = variableVersion.CompareTo(new Version("1.5.9")) >= 0;
            if (shaHashSerialization) {
                strHashSerialization = variableVersion.CompareTo(new Version("1.5.11")) >= 0;
                if (strHashSerialization) {
                    animationCurveAdvance = variableVersion.CompareTo(new Version("1.5.12")) >= 0 ? 16 : 20;
                }
            }
        }

        if (variableData.variableStartIndex != null) {
            var variables = new List<SharedVariable>();
            var fieldIndexMap = ObjectPool.Get<Dictionary<int, int>>();
            for (int i = 0; i < variableData.variableStartIndex.Count; ++i) {
                int startIndex = variableData.variableStartIndex[i];
                int endIndex;
                if (i + 1 < variableData.variableStartIndex.Count) {
                    endIndex = variableData.variableStartIndex[i + 1];
                } else {
                    endIndex = fieldSerializationData.startIndex.Count;
                }
                // build a dictionary based off of the saved fields
                fieldIndexMap.Clear();
                for (int j = startIndex; j < endIndex; ++j) {
                    fieldIndexMap.Add(fieldSerializationData.fieldNameHash[j], fieldSerializationData.startIndex[j]);
                }

                var sharedVariable = BytesToSharedVariable(fieldSerializationData, fieldIndexMap, fieldSerializationData.byteDataArray, variableData.variableStartIndex[i], globalVariables, false, 0);
                if (sharedVariable != null) {
                    variables.Add(sharedVariable);
                }
            }
            ObjectPool.Return(fieldIndexMap);
            globalVariables.Variables = variables;
        }
    }

    public static void LoadTask(TaskSerializationData taskSerializationData, FieldSerializationData fieldSerializationData, ref List<Task> taskList, ref BehaviorSource behaviorSource)
    {
        int taskIndex = taskList.Count;
        int startIndex = taskSerializationData.startIndex[taskIndex];
        int endIndex;
        if (taskIndex + 1 < taskSerializationData.startIndex.Count) {
            endIndex = taskSerializationData.startIndex[taskIndex + 1];
        } else {
            endIndex = fieldSerializationData.startIndex.Count;
        }
        // build a dictionary based off of the saved fields
        var fieldIndexMap = ObjectPool.Get<Dictionary<int, int>>();
        fieldIndexMap.Clear(); 
        for (int i = startIndex; i < endIndex; ++i) {
            if (fieldIndexMap.ContainsKey(fieldSerializationData.fieldNameHash[i])) {
                continue;
            }
            fieldIndexMap.Add(fieldSerializationData.fieldNameHash[i], fieldSerializationData.startIndex[i]);
        }
        Task task = null;
        var type = TaskUtility.GetTypeWithinAssembly(taskSerializationData.types[taskIndex]);
        // Change the type to an unknown type if the type doesn't exist anymore.
        if (type == null) {
            bool isUnknownParent = false;
            for (int i = 0; i < taskSerializationData.parentIndex.Count; ++i) {
                if (taskIndex == taskSerializationData.parentIndex[i]) {
                    isUnknownParent = true;
                    break;
                }
            }
            if (isUnknownParent) {
                type = typeof(UnknownParentTask);
            } else {
                type = typeof(UnknownTask);
            }
        }
        task = TaskUtility.CreateInstance(type) as Task;
        if (task is UnknownTask) {
            var unknownTask = task as UnknownTask;
            for (int i = startIndex; i < endIndex; ++i) {
                unknownTask.fieldNameHash.Add(fieldSerializationData.fieldNameHash[i]);
                unknownTask.startIndex.Add(fieldSerializationData.startIndex[i] - fieldSerializationData.startIndex[startIndex]);
            }
            for (int i = fieldSerializationData.startIndex[startIndex]; i <= fieldSerializationData.startIndex[endIndex - 1]; ++i) {
                unknownTask.dataPosition.Add(fieldSerializationData.dataPosition[i] - fieldSerializationData.dataPosition[fieldSerializationData.startIndex[startIndex]]);
            }
            if (taskIndex + 1 < taskSerializationData.startIndex.Count && taskSerializationData.startIndex[taskIndex + 1] < fieldSerializationData.dataPosition.Count) {
                endIndex = fieldSerializationData.dataPosition[taskSerializationData.startIndex[taskIndex + 1]];
            } else {
                endIndex = fieldSerializationData.byteData.Count;
            }
            for (int i = fieldSerializationData.dataPosition[fieldSerializationData.startIndex[startIndex]]; i < endIndex; ++i) {
                unknownTask.byteData.Add(fieldSerializationData.byteData[i]);
            }
            unknownTask.unityObjects = fieldSerializationData.unityObjects;
        }

        task.Owner = behaviorSource.Owner.GetObject() as Behavior;
        taskList.Add(task);

        task.ID = (int)LoadField(fieldSerializationData, fieldIndexMap, typeof(int), "ID", 0, null);
        task.FriendlyName = (string)LoadField(fieldSerializationData, fieldIndexMap, typeof(string), "FriendlyName", 0, null);
        task.IsInstant = (bool)LoadField(fieldSerializationData, fieldIndexMap, typeof(bool), "IsInstant", 0, null);
        object disabled;
        if ((disabled = LoadField(fieldSerializationData, fieldIndexMap, typeof(bool), "Disabled", 0, null)) != null) {
            task.Disabled = (bool)disabled;
        }

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
        LoadNodeData(fieldSerializationData, fieldIndexMap, taskList[taskIndex]);

        // give a little warning if the task is an unknown type
        if (task.GetType().Equals(typeof(UnknownTask)) || task.GetType().Equals(typeof(UnknownParentTask))) {
            if (!task.FriendlyName.Contains("Unknown ")) {
                task.FriendlyName = string.Format("Unknown {0}", task.FriendlyName);
            }
            task.NodeData.Comment = "Unknown Task. Right click and Replace to locate new task.";
        }
#endif
        LoadFields(fieldSerializationData, fieldIndexMap, taskList[taskIndex], 0, behaviorSource);
        ObjectPool.Return(fieldIndexMap);
    }

#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
    private static void LoadNodeData(FieldSerializationData fieldSerializationData, Dictionary<int, int> fieldIndexMap, Task task)
    {
        var nodeData = new NodeData();
        nodeData.Offset = (Vector2)LoadField(fieldSerializationData, fieldIndexMap, typeof(Vector2), "NodeDataOffset", 0, null);
        nodeData.Comment = (string)LoadField(fieldSerializationData, fieldIndexMap, typeof(string), "NodeDataComment", 0, null);
        nodeData.IsBreakpoint = (bool)LoadField(fieldSerializationData, fieldIndexMap, typeof(bool), "NodeDataIsBreakpoint", 0, null);
        nodeData.Collapsed = (bool)LoadField(fieldSerializationData, fieldIndexMap, typeof(bool), "NodeDataCollapsed", 0, null);
        var value = LoadField(fieldSerializationData, fieldIndexMap, typeof(int), "NodeDataColorIndex", 0, null);
        if (value != null) {
            nodeData.ColorIndex = (int)value;
        }
        value = LoadField(fieldSerializationData, fieldIndexMap, typeof(List<string>), "NodeDataWatchedFields", 0, null);
        if (value != null) {
            nodeData.WatchedFieldNames = new List<string>();
            nodeData.WatchedFields = new List<FieldInfo>();

            var objectValues = value as IList;
            for (int i = 0; i < objectValues.Count; ++i) {
                var field = task.GetType().GetField((string)objectValues[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (field != null) {
                    nodeData.WatchedFieldNames.Add(field.Name);
                    nodeData.WatchedFields.Add(field);
                }
            }
        }
        task.NodeData = nodeData;
    }
#endif

    private static void LoadFields(FieldSerializationData fieldSerializationData, Dictionary<int, int> fieldIndexMap, object obj, int hashPrefix, IVariableSource variableSource)
    {
        var fields = TaskUtility.GetSerializableFields(obj.GetType());
        for (int i = 0; i < fields.Length; ++i) {
            // there are a variety of reasons why we can't deserialize a field
            if (TaskUtility.HasAttribute(fields[i], typeof(NonSerializedAttribute)) ||
                ((fields[i].IsPrivate || fields[i].IsFamily) && !TaskUtility.HasAttribute(fields[i], typeof(SerializeField))) ||
                (obj is ParentTask) && fields[i].Name.Equals("children")) {
                continue;
            }
            var value = LoadField(fieldSerializationData, fieldIndexMap, fields[i].FieldType, fields[i].Name, hashPrefix, variableSource, obj, fields[i]);
            if (value != null && !ReferenceEquals(value, null) && !value.Equals(null) && fields[i].FieldType.IsAssignableFrom(value.GetType())) {
                fields[i].SetValue(obj, value);
            }
        }
    }

    private static object LoadField(FieldSerializationData fieldSerializationData, Dictionary<int, int> fieldIndexMap, Type fieldType, string fieldName, int hashPrefix, IVariableSource variableSource, object obj = null, FieldInfo fieldInfo = null)
    {
        var fieldHash = hashPrefix;
        if (shaHashSerialization) {
            fieldHash += StringHash(fieldType.Name.ToString(), strHashSerialization) + StringHash(fieldName, strHashSerialization);
        } else {
            fieldHash += fieldType.Name.GetHashCode() + fieldName.GetHashCode();
        }
        int fieldIndex;
        if (!fieldIndexMap.TryGetValue(fieldHash, out fieldIndex)) {
#if NETFX_CORE && !UNITY_EDITOR
            if (fieldType.GetTypeInfo().IsAbstract) {
#else
            if (fieldType.IsAbstract) {
#endif
                return null;
            }

            if (typeof(SharedVariable).IsAssignableFrom(fieldType)) {
                var sharedVariable = TaskUtility.CreateInstance(fieldType) as SharedVariable;
                var sharedVariableValue = (fieldInfo.GetValue(obj) as SharedVariable);
                if (sharedVariableValue != null) {
                    sharedVariable.SetValue(sharedVariableValue.GetValue());
                }
                return sharedVariable;
            }

            return null;
        }
        object value = null;
        if (typeof(IList).IsAssignableFrom(fieldType)) { // array
            int elementCount = BytesToInt(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
            if (fieldType.IsArray) {
                var elementType = fieldType.GetElementType();
                if (elementType == null) {
                    return null;
                }
                var objectArray = Array.CreateInstance(elementType, elementCount);
                for (int i = 0; i < elementCount; ++i) {
                    var objectValue = LoadField(fieldSerializationData, fieldIndexMap, elementType, i.ToString(), fieldHash / (updatedSerialization ? (i + 1) : 1), variableSource, obj, fieldInfo);
                    objectArray.SetValue((ReferenceEquals(objectValue, null) || objectValue.Equals(null)) ? null : objectValue, i);
                }
                value = objectArray;
            } else {
                var baseFieldType = fieldType;
#if NETFX_CORE && !UNITY_EDITOR
                while (!baseFieldType.IsGenericType()) {
                    baseFieldType = baseFieldType.BaseType();
                }
#else
                while (!baseFieldType.IsGenericType) {
                    baseFieldType = baseFieldType.BaseType;
                }
#endif
                var elementType = baseFieldType.GetGenericArguments()[0];
                IList objectList;
#if NETFX_CORE && !UNITY_EDITOR
                if (fieldType.IsGenericType()) {
#else
                if (fieldType.IsGenericType) {
#endif
                    objectList = TaskUtility.CreateInstance(typeof(List<>).MakeGenericType(elementType)) as IList;
                } else {
                    objectList = TaskUtility.CreateInstance(fieldType) as IList;
                }
                for (int i = 0; i < elementCount; ++i) {
                    var objectValue = LoadField(fieldSerializationData, fieldIndexMap, elementType, i.ToString(), fieldHash / (updatedSerialization ? (i + 1) : 1), variableSource, obj, fieldInfo);
                    objectList.Add((ReferenceEquals(objectValue, null) || objectValue.Equals(null)) ? null : objectValue);
                }
                value = objectList;
            }
        } else if (typeof(Task).IsAssignableFrom(fieldType)) {
            if (fieldInfo != null && TaskUtility.HasAttribute(fieldInfo, typeof(InspectTaskAttribute))) {
                var taskTypeName = BytesToString(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex], GetFieldSize(fieldSerializationData, fieldIndex));
                if (!string.IsNullOrEmpty(taskTypeName)) {
                    var taskType = TaskUtility.GetTypeWithinAssembly(taskTypeName);
                    if (taskType != null) {
                        value = TaskUtility.CreateInstance(taskType);
                        LoadFields(fieldSerializationData, fieldIndexMap, value, fieldHash, variableSource);
                    }
                }
            } else { // restore the task ids
                if (taskIDs == null) {
                    taskIDs = new Dictionary<ObjectFieldMap, List<int>>(new ObjectFieldMapComparer());
                }
                int taskID = BytesToInt(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
                // Add the task id
                var map = new ObjectFieldMap(obj, fieldInfo);
                if (taskIDs.ContainsKey(map)) {
                    taskIDs[map].Add(taskID);
                } else {
                    var taskIDList = new List<int>();
                    taskIDList.Add(taskID);
                    taskIDs.Add(map, taskIDList);
                }
            }
        } else if (typeof(SharedVariable).IsAssignableFrom(fieldType)) {
            value = BytesToSharedVariable(fieldSerializationData, fieldIndexMap, fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex], variableSource, true, fieldHash);
        } else if (typeof(UnityEngine.Object).IsAssignableFrom(fieldType)) {
            int unityObjectIndex = BytesToInt(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
            value = IndexToUnityObject(unityObjectIndex, fieldSerializationData);
#if !UNITY_EDITOR && NETFX_CORE
        } else if (fieldType.Equals(typeof(int)) || fieldType.GetTypeInfo().IsEnum) {
#else
        } else if (fieldType.Equals(typeof(int))) {
#endif
            value = BytesToInt(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.IsEnum) {
            value = Enum.ToObject(fieldType, BytesToInt(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]));
        } else if (fieldType.Equals(typeof(uint))) {
            value = BytesToUInt(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(float))) {
            value = BytesToFloat(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(double))) {
            value = BytesToDouble(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(long))) {
            value = BytesToLong(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(bool))) {
            value = BytesToBool(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(string))) {
            value = BytesToString(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex], GetFieldSize(fieldSerializationData, fieldIndex));
        } else if (fieldType.Equals(typeof(byte))) {
            value = BytesToByte(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(Vector2))) {
            value = BytesToVector2(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(Vector3))) {
            value = BytesToVector3(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(Vector4))) {
            value = BytesToVector4(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(Quaternion))) {
            value = BytesToQuaternion(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(Color))) {
            value = BytesToColor(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(Rect))) {
            value = BytesToRect(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(Matrix4x4))) {
            value = BytesToMatrix4x4(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(AnimationCurve))) {
            value = BytesToAnimationCurve(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
        } else if (fieldType.Equals(typeof(LayerMask))) {
            value = BytesToLayerMask(fieldSerializationData.byteDataArray, fieldSerializationData.dataPosition[fieldIndex]);
#if !UNITY_EDITOR && NETFX_CORE
        } else if (fieldType.GetTypeInfo().IsClass) {
#else
        } else if (fieldType.IsClass || (fieldType.IsValueType && !fieldType.IsPrimitive)) {
#endif
            value = TaskUtility.CreateInstance(fieldType);
            LoadFields(fieldSerializationData, fieldIndexMap, value, fieldHash, variableSource);
            return value;
        }
        return value;
    }
    
    public static int StringHash(string value, bool fastHash)
    {
        if (String.IsNullOrEmpty(value)) {
            return 0;
        }

        if (fastHash) { // 1.5.11 and later, from https://stackoverflow.com/questions/5154970/how-do-i-create-a-hashcode-in-net-c-for-a-string-that-is-safe-to-store-in-a
            var hash = 23;
            var length = value.Length;
            for (int i = 0; i < length; ++i) {
                hash = hash * 31 + value[i];
            }
            return hash;
        } else { // Pre 1.5.11
#if NETFX_CORE && !UNITY_EDITOR
            var valueData = CryptographicBuffer.ConvertStringToBinary(value, BinaryStringEncoding.Utf8);
            var shaHash = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
            var hashBuffer = shaHash.HashData(valueData);
        
            byte[] hash;
            CryptographicBuffer.CopyToByteArray(hashBuffer, out hash);
#else
            var valueData = Encoding.UTF8.GetBytes(value);
            if (shaHash == null) {
                shaHash = new SHA1Managed();
            }
            var hash = shaHash.ComputeHash(valueData);
#endif
            return BitConverter.ToInt32(hash, 0);
        }
    }

    private static int GetFieldSize(FieldSerializationData fieldSerializationData, int fieldIndex)
    {
        return (fieldIndex + 1 < fieldSerializationData.dataPosition.Count ? fieldSerializationData.dataPosition[fieldIndex + 1] : fieldSerializationData.byteData.Count) - fieldSerializationData.dataPosition[fieldIndex];
    }

    private static int BytesToInt(byte[] bytes, int dataPosition)
    {
        if (!BitConverter.IsLittleEndian) {
            Array.Copy(bytes, dataPosition, BigEndianFourByteArray, 0, 4);
            Array.Reverse(BigEndianFourByteArray);
            return BitConverter.ToInt32(BigEndianFourByteArray, 0);
        }
        return BitConverter.ToInt32(bytes, dataPosition);
    }

    private static uint BytesToUInt(byte[] bytes, int dataPosition)
    {
        if (!BitConverter.IsLittleEndian) {
            Array.Copy(bytes, dataPosition, BigEndianFourByteArray, 0, 4);
            Array.Reverse(BigEndianFourByteArray);
            return BitConverter.ToUInt32(BigEndianFourByteArray, 0);
        }
        return BitConverter.ToUInt32(bytes, dataPosition);
    }

    private static float BytesToFloat(byte[] bytes, int dataPosition)
    {
        if (!BitConverter.IsLittleEndian) {
            Array.Copy(bytes, dataPosition, BigEndianFourByteArray, 0, 4);
            Array.Reverse(BigEndianFourByteArray);
            return BitConverter.ToSingle(BigEndianFourByteArray, 0);
        }
        return BitConverter.ToSingle(bytes, dataPosition);
    }

    private static double BytesToDouble(byte[] bytes, int dataPosition)
    {
        if (!BitConverter.IsLittleEndian) {
            Array.Copy(bytes, dataPosition, BigEndianEightByteArray, 0, 8);
            Array.Reverse(BigEndianEightByteArray);
            return BitConverter.ToDouble(BigEndianEightByteArray, 0);
        }
        return BitConverter.ToDouble(bytes, dataPosition);
    }

    private static long BytesToLong(byte[] bytes, int dataPosition)
    {
        if (!BitConverter.IsLittleEndian) {
            Array.Copy(bytes, dataPosition, BigEndianEightByteArray, 0, 8);
            Array.Reverse(BigEndianEightByteArray);
            return BitConverter.ToInt64(BigEndianEightByteArray, 0);
        }
        return BitConverter.ToInt64(bytes, dataPosition);
    }

    private static bool BytesToBool(byte[] bytes, int dataPosition)
    {
        return BitConverter.ToBoolean(bytes, dataPosition);
    }

    private static string BytesToString(byte[] bytes, int dataPosition, int dataSize)
    {
        if (dataSize == 0)
            return "";
        return Encoding.UTF8.GetString(bytes, dataPosition, dataSize);
    }

    private static byte BytesToByte(byte[] bytes, int dataPosition)
    {
        return bytes[dataPosition];
    }

    private static Color BytesToColor(byte[] bytes, int dataPosition)
    {
        var color = Color.black;
        color.r = BytesToFloat(bytes, dataPosition);
        color.g = BytesToFloat(bytes, dataPosition + 4);
        color.b = BytesToFloat(bytes, dataPosition + 8);
        color.a = BytesToFloat(bytes, dataPosition + 12);
        return color;
    }

    private static Vector2 BytesToVector2(byte[] bytes, int dataPosition)
    {
        var vector2 = Vector2.zero;
        vector2.x = BytesToFloat(bytes, dataPosition);
        vector2.y = BytesToFloat(bytes, dataPosition + 4);
        return vector2;
    }

    private static Vector3 BytesToVector3(byte[] bytes, int dataPosition)
    {
        var vector3 = Vector3.zero;
        vector3.x = BytesToFloat(bytes, dataPosition);
        vector3.y = BytesToFloat(bytes, dataPosition + 4);
        vector3.z = BytesToFloat(bytes, dataPosition + 8);
        return vector3;
    }

    private static Vector4 BytesToVector4(byte[] bytes, int dataPosition)
    {
        var vector4 = Vector4.zero;
        vector4.x = BytesToFloat(bytes, dataPosition);
        vector4.y = BytesToFloat(bytes, dataPosition + 4);
        vector4.z = BytesToFloat(bytes, dataPosition + 8);
        vector4.w = BytesToFloat(bytes, dataPosition + 12);
        return vector4;
    }

    private static Quaternion BytesToQuaternion(byte[] bytes, int dataPosition)
    {
        var quaternion = Quaternion.identity;
        quaternion.x = BytesToFloat(bytes, dataPosition);
        quaternion.y = BytesToFloat(bytes, dataPosition + 4);
        quaternion.z = BytesToFloat(bytes, dataPosition + 8);
        quaternion.w = BytesToFloat(bytes, dataPosition + 12);
        return quaternion;
    }

    private static Rect BytesToRect(byte[] bytes, int dataPosition)
    {
        var rect = new Rect();
        rect.x = BytesToFloat(bytes, dataPosition);
        rect.y = BytesToFloat(bytes, dataPosition + 4);
        rect.width = BytesToFloat(bytes, dataPosition + 8);
        rect.height = BytesToFloat(bytes, dataPosition + 12);
        return rect;
    }

    private static Matrix4x4 BytesToMatrix4x4(byte[] bytes, int dataPosition)
    {
        var matrix4x4 = Matrix4x4.identity;
        matrix4x4.m00 = BytesToFloat(bytes, dataPosition);
        matrix4x4.m01 = BytesToFloat(bytes, dataPosition + 4);
        matrix4x4.m02 = BytesToFloat(bytes, dataPosition + 8);
        matrix4x4.m03 = BytesToFloat(bytes, dataPosition + 12);
        matrix4x4.m10 = BytesToFloat(bytes, dataPosition + 16);
        matrix4x4.m11 = BytesToFloat(bytes, dataPosition + 20);
        matrix4x4.m12 = BytesToFloat(bytes, dataPosition + 24);
        matrix4x4.m13 = BytesToFloat(bytes, dataPosition + 28);
        matrix4x4.m20 = BytesToFloat(bytes, dataPosition + 32);
        matrix4x4.m21 = BytesToFloat(bytes, dataPosition + 36);
        matrix4x4.m22 = BytesToFloat(bytes, dataPosition + 40);
        matrix4x4.m23 = BytesToFloat(bytes, dataPosition + 44);
        matrix4x4.m30 = BytesToFloat(bytes, dataPosition + 48);
        matrix4x4.m31 = BytesToFloat(bytes, dataPosition + 52);
        matrix4x4.m32 = BytesToFloat(bytes, dataPosition + 56);
        matrix4x4.m33 = BytesToFloat(bytes, dataPosition + 60);
        return matrix4x4;
    }

    private static AnimationCurve BytesToAnimationCurve(byte[] bytes, int dataPosition)
    {
        var animationCurve = new AnimationCurve();
        var keyCount = BytesToInt(bytes, dataPosition);
        for (int i = 0; i < keyCount; ++i) {
            var keyframe = new Keyframe();
            keyframe.time = BytesToFloat(bytes, dataPosition + 4);
            keyframe.value = BytesToFloat(bytes, dataPosition + 8);
            keyframe.inTangent = BytesToFloat(bytes, dataPosition + 12);
            keyframe.outTangent = BitConverter.ToSingle(bytes, dataPosition + 16);
            animationCurve.AddKey(keyframe);
            dataPosition += animationCurveAdvance;
        }
        animationCurve.preWrapMode = (WrapMode)BytesToInt(bytes, dataPosition + 4);
        animationCurve.postWrapMode = (WrapMode)BytesToInt(bytes, dataPosition + 8);
        return animationCurve;
    }

    private static LayerMask BytesToLayerMask(byte[] bytes, int dataPosition)
    {
        var layerMask = new LayerMask();
        layerMask.value = BytesToInt(bytes, dataPosition);
        return layerMask;
    }

    private static UnityEngine.Object IndexToUnityObject(int index, FieldSerializationData activeFieldSerializationData)
    {
        if (index < 0 || index >= activeFieldSerializationData.unityObjects.Count) {
            return null;
        }

        return activeFieldSerializationData.unityObjects[index];
    }

    private static SharedVariable BytesToSharedVariable(FieldSerializationData fieldSerializationData, Dictionary<int, int> fieldIndexMap, byte[] bytes, int dataPosition, IVariableSource variableSource, bool fromField, int hashPrefix)
    {
        SharedVariable sharedVariable = null;
        var variableTypeName = (string)LoadField(fieldSerializationData, fieldIndexMap, typeof(string), "Type", hashPrefix, null);
        if (string.IsNullOrEmpty(variableTypeName)) {
            return null;
        }
        var variableName = (string)LoadField(fieldSerializationData, fieldIndexMap, typeof(string), "Name", hashPrefix, null);
        var isShared = Convert.ToBoolean(LoadField(fieldSerializationData, fieldIndexMap, typeof(bool), "IsShared", hashPrefix, null));
        var isGlobal = Convert.ToBoolean(LoadField(fieldSerializationData, fieldIndexMap, typeof(bool), "IsGlobal", hashPrefix, null));
        var isDynamic = Convert.ToBoolean(LoadField(fieldSerializationData, fieldIndexMap, typeof(bool), "IsDynamic", hashPrefix, null));

        if (isShared && (!isDynamic || Application.isPlaying) && fromField) {
            if (!isGlobal) {
                sharedVariable = variableSource.GetVariable(variableName);
            } else {
                if (globalVariables == null) {
                    globalVariables = GlobalVariables.Instance;
                }
                if (globalVariables != null) {
                    sharedVariable = globalVariables.GetVariable(variableName);
                }
            }
        }

        var variableType = TaskUtility.GetTypeWithinAssembly(variableTypeName);
        if (variableType == null) {
            return null;
        }

        bool typesEqual = true;
        if (sharedVariable == null || !(typesEqual = sharedVariable.GetType().Equals(variableType))) {
            sharedVariable = TaskUtility.CreateInstance(variableType) as SharedVariable;
            sharedVariable.Name = variableName;
            sharedVariable.IsShared = isShared;
            sharedVariable.IsGlobal = isGlobal;
            sharedVariable.IsDynamic = isDynamic;
            sharedVariable.NetworkSync = Convert.ToBoolean(LoadField(fieldSerializationData, fieldIndexMap, typeof(bool), "NetworkSync", hashPrefix, null));
            if (!isGlobal) {
                sharedVariable.PropertyMapping = (string)LoadField(fieldSerializationData, fieldIndexMap, typeof(string), "PropertyMapping", hashPrefix, null);
                sharedVariable.PropertyMappingOwner = (GameObject)LoadField(fieldSerializationData, fieldIndexMap, typeof(GameObject), "PropertyMappingOwner", hashPrefix, null);
                sharedVariable.InitializePropertyMapping(variableSource as BehaviorSource);
            }

            // if the types are not equal then this shared variable used to be a different type so it should be shared
            if (!typesEqual) {
                sharedVariable.IsShared = true;
            }

            if (isDynamic && Application.isPlaying) {
                // The new dynamic variable has been created.
                variableSource.SetVariable(variableName, sharedVariable);
            }

            LoadFields(fieldSerializationData, fieldIndexMap, sharedVariable, hashPrefix, variableSource);
        }

        return sharedVariable;
    }
}
