using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Globalization;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime
{
    public class JSONDeserialization : UnityEngine.Object
    {
        public struct TaskField
        {
            public TaskField(Task t, FieldInfo f) { task = t; fieldInfo = f; }
            public Task task;
            public FieldInfo fieldInfo;
        }

        private static Dictionary<TaskField, List<int>> taskIDs = null;
        public static Dictionary<TaskField, List<int>> TaskIDs { get { return taskIDs; } set { taskIDs = value; } }
        private static GlobalVariables globalVariables = null;
        public static bool updatedSerialization = true;

        private static Dictionary<int, Dictionary<string, object>> serializationCache = new Dictionary<int, Dictionary<string, object>>();

        public static void Load(TaskSerializationData taskData, BehaviorSource behaviorSource)
        {
            behaviorSource.EntryTask = null;
            behaviorSource.RootTask = null;
            behaviorSource.DetachedTasks = null;
            behaviorSource.Variables = null;
            Dictionary<string, object> dict;
            if (!serializationCache.TryGetValue(taskData.JSONSerialization.GetHashCode(), out dict)) {
                dict = MiniJSON.Deserialize(taskData.JSONSerialization) as Dictionary<string, object>;
                serializationCache.Add(taskData.JSONSerialization.GetHashCode(), dict);
            }
            if (dict == null) {
                Debug.Log("Failed to deserialize");
                return;
            }
            taskIDs = new Dictionary<TaskField, List<int>>();
            var treeVersion = new Version(taskData.Version);
            updatedSerialization = treeVersion.CompareTo(new Version("1.5.7")) >= 0;
            var IDtoTask = new Dictionary<int, Task>();

            // deserialize the variables first so the tasks can reference them
            DeserializeVariables(behaviorSource, dict, taskData.fieldSerializationData.unityObjects);

            if (dict.ContainsKey("EntryTask")) {
                behaviorSource.EntryTask = DeserializeTask(behaviorSource, dict["EntryTask"] as Dictionary<string, object>, ref IDtoTask, taskData.fieldSerializationData.unityObjects);
            }

            if (dict.ContainsKey("RootTask")) {
                behaviorSource.RootTask = DeserializeTask(behaviorSource, dict["RootTask"] as Dictionary<string, object>, ref IDtoTask, taskData.fieldSerializationData.unityObjects);
            }

            if (dict.ContainsKey("DetachedTasks")) {
                var detachedTasks = new List<Task>();
                foreach (Dictionary<string, object> detachedTaskDict in (dict["DetachedTasks"] as IEnumerable)) {
                    detachedTasks.Add(DeserializeTask(behaviorSource, detachedTaskDict, ref IDtoTask, taskData.fieldSerializationData.unityObjects));
                }
                behaviorSource.DetachedTasks = detachedTasks;
            }

            // deserialization is complete besides assigning the correct tasks based off of the id
            if (taskIDs != null && taskIDs.Count > 0) {
                foreach (TaskField taskField in taskIDs.Keys) {
                    var idList = taskIDs[taskField] as List<int>;
                    var fieldType = taskField.fieldInfo.FieldType;
                    if (taskField.fieldInfo.FieldType.IsArray) { // task array
                        var count = 0;
                        for (int i = 0; i < idList.Count; ++i) {
                            var task = IDtoTask[idList[i]];
                            if (task.GetType().Equals(fieldType.GetElementType()) || task.GetType().IsSubclassOf(fieldType.GetElementType())) {
                                count++;
                            }
                        }
                        var taskArray = Array.CreateInstance(fieldType.GetElementType(), count);
                        int index = 0;
                        for (int i = 0; i < idList.Count; ++i) {
                            var task = IDtoTask[idList[i]];
                            if (task.GetType().Equals(fieldType.GetElementType()) || task.GetType().IsSubclassOf(fieldType.GetElementType())) {
                                taskArray.SetValue(task, index);
                                index++;
                            }
                        }
                        taskField.fieldInfo.SetValue(taskField.task, taskArray);
                    } else { // single task
                        var task = IDtoTask[idList[0]];
                        if (task.GetType().Equals(taskField.fieldInfo.FieldType) || task.GetType().IsSubclassOf(taskField.fieldInfo.FieldType)) {
                            taskField.fieldInfo.SetValue(taskField.task, task);
                        }
                    }
                }
                taskIDs = null;
            }
        }

        public static void Load(string serialization, GlobalVariables globalVariables, string version)
        {
            if (globalVariables == null)
                return;

            var dict = MiniJSON.Deserialize(serialization) as Dictionary<string, object>;
            if (dict == null) {
                Debug.Log("Failed to deserialize");
                return;
            }

            if (globalVariables.VariableData == null) {
                globalVariables.VariableData = new VariableSerializationData();
            }
            var variableVersion = new Version(globalVariables.Version);
            updatedSerialization = variableVersion.CompareTo(new Version("1.5.7")) >= 0;

            DeserializeVariables(globalVariables, dict, globalVariables.VariableData.fieldSerializationData.unityObjects);
        }

        private static void DeserializeVariables(IVariableSource variableSource, Dictionary<string, object> dict, List<UnityEngine.Object> unityObjects)
        {
            object dictObj;
            if (dict.TryGetValue("Variables", out dictObj)) {
                var variables = new List<SharedVariable>();
                var variablesList = dictObj as IList;
                for (int i = 0; i < variablesList.Count; ++i) {
                    var sharedVariable = DeserializeSharedVariable(variablesList[i] as Dictionary<string, object>, variableSource, true, unityObjects);
                    variables.Add(sharedVariable);
                }
                variableSource.SetAllVariables(variables);
            }
        }

        public static Task DeserializeTask(BehaviorSource behaviorSource, Dictionary<string, object> dict, ref Dictionary<int, Task> IDtoTask, List<UnityEngine.Object> unityObjects)
        {
            Task task = null;
            try {
                var type = TaskUtility.GetTypeWithinAssembly(dict["Type"] as string);
                // Change the type to an unknown type if the type doesn't exist anymore.
                if (type == null) {
                    if (dict.ContainsKey("Children")) {
                        type = typeof(UnknownParentTask);
                    } else {
                        type = typeof(UnknownTask);
                    }
                }
                task = TaskUtility.CreateInstance(type) as Task;
                if (task is UnknownTask) {
                    var unknownTask = task as UnknownTask;
                    unknownTask.JSONSerialization = MiniJSON.Serialize(dict);
                }
            }
            catch (Exception /*e*/) { }

            // What happened?
            if (task == null) {
                return null;
            }

            task.Owner = behaviorSource.Owner.GetObject() as Behavior;
            task.ID = Convert.ToInt32(dict["ID"], CultureInfo.InvariantCulture);
            object dictObj;
            if (dict.TryGetValue("Name", out dictObj)) {
                task.FriendlyName = (string)dictObj;
            }
            if (dict.TryGetValue("Instant", out dictObj)) {
                task.IsInstant = Convert.ToBoolean(dictObj, CultureInfo.InvariantCulture);
            }
            if (dict.TryGetValue("Disabled", out dictObj)) {
                task.Disabled = Convert.ToBoolean(dictObj, CultureInfo.InvariantCulture);
            }
            IDtoTask.Add(task.ID, task);
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
            task.NodeData = DeserializeNodeData(dict["NodeData"] as Dictionary<string, object>, task);

            // give a little warning if the task is an unknown type
            if (task.GetType().Equals(typeof(UnknownTask)) || task.GetType().Equals(typeof(UnknownParentTask))) {
                if (!task.FriendlyName.Contains("Unknown ")) {
                    task.FriendlyName = string.Format("Unknown {0}", task.FriendlyName);
                }
                task.NodeData.Comment = "Unknown Task. Right click and Replace to locate new task.";
            }
#endif
            DeserializeObject(task, task, dict, behaviorSource, unityObjects);

            if (task is ParentTask && dict.TryGetValue("Children", out dictObj)) {
                var parentTask = task as ParentTask;
                if (parentTask != null) {
                    foreach (Dictionary<string, object> childDict in (dictObj as IEnumerable)) {
                        var child = DeserializeTask(behaviorSource, childDict, ref IDtoTask, unityObjects);
                        int index = (parentTask.Children == null ? 0 : parentTask.Children.Count);
                        parentTask.AddChild(child, index);
                    }
                }
            }

            return task;
        }
        
#if UNITY_EDITOR || DLL_DEBUG || DLL_RELEASE
        private static NodeData DeserializeNodeData(Dictionary<string, object> dict, Task task)
        {
            var nodeData = new NodeData();
            object dictObj;
            if (dict.TryGetValue("Offset", out dictObj)) {
                nodeData.Offset = StringToVector2((string)dictObj);
            }
            if (dict.TryGetValue("FriendlyName", out dictObj)) {
                task.FriendlyName = (string)dictObj;
            }
            if (dict.TryGetValue("Comment", out dictObj)) {
                nodeData.Comment = (string)dictObj;
            }
            if (dict.TryGetValue("IsBreakpoint", out dictObj)) {
                nodeData.IsBreakpoint = Convert.ToBoolean(dictObj, CultureInfo.InvariantCulture);
            }
            if (dict.TryGetValue("Collapsed", out dictObj)) {
                nodeData.Collapsed = Convert.ToBoolean(dictObj, CultureInfo.InvariantCulture);
            }
            if (dict.TryGetValue("ColorIndex", out dictObj)) {
                nodeData.ColorIndex = Convert.ToInt32(dictObj, CultureInfo.InvariantCulture);
            }
            if (dict.TryGetValue("WatchedFields", out dictObj)) {
                nodeData.WatchedFieldNames = new List<string>();
                nodeData.WatchedFields = new List<FieldInfo>();

                var objectValues = dictObj as IList;
                for (int i = 0; i < objectValues.Count; ++i) {
                    var field = task.GetType().GetField((string)objectValues[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (field != null) {
                        nodeData.WatchedFieldNames.Add(field.Name);
                        nodeData.WatchedFields.Add(field);
                    }
                }
            }
            return nodeData;
        }
#endif

        // If the serialized variable is from the source then just create a new variable and don't check to see if it already exists
        private static SharedVariable DeserializeSharedVariable(Dictionary<string, object> dict, IVariableSource variableSource, bool fromSource, List<UnityEngine.Object> unityObjects)
        {
            if (dict == null) {
                return null;
            }

            SharedVariable sharedVariable = null;
            object nameObj;
            // the shared variable may be referencing the variable within the behavior
            if (!fromSource && variableSource != null && dict.TryGetValue("Name", out nameObj) && (Application.isPlaying || !dict.ContainsKey("IsDynamic"))) {
                object globalObj;
                dict.TryGetValue("IsGlobal", out globalObj);
                if (!dict.TryGetValue("IsGlobal", out globalObj) || Convert.ToBoolean(globalObj, CultureInfo.InvariantCulture) == false) {
                    sharedVariable = variableSource.GetVariable(nameObj as string);
                } else {
                    if (globalVariables == null) {
                        globalVariables = GlobalVariables.Instance;
                    }
                    if (globalVariables != null) {
                        sharedVariable = globalVariables.GetVariable(nameObj as string);
                    }
                }
            }

            var variableType = TaskUtility.GetTypeWithinAssembly(dict["Type"] as string);
            if (variableType == null) {
                return null;
            }
            bool typesEqual = true;
            if (sharedVariable == null || !(typesEqual = sharedVariable.GetType().Equals(variableType))) {
                sharedVariable = TaskUtility.CreateInstance(variableType) as SharedVariable;
                sharedVariable.Name = dict["Name"] as string;
                object dictValue;
                if (dict.TryGetValue("IsShared", out dictValue)) {
                    sharedVariable.IsShared = Convert.ToBoolean(dictValue, CultureInfo.InvariantCulture);
                }
                if (dict.TryGetValue("IsGlobal", out dictValue)) {
                    sharedVariable.IsGlobal = Convert.ToBoolean(dictValue, CultureInfo.InvariantCulture);
                }
                if (dict.TryGetValue("IsDynamic", out dictValue)) {
                    sharedVariable.IsDynamic = Convert.ToBoolean(dictValue, CultureInfo.InvariantCulture);

                    // The new dynamic variable has been created.
                    if (Application.isPlaying) {
                        variableSource.SetVariable(sharedVariable.Name, sharedVariable);
                    }
                }
                if (dict.TryGetValue("NetworkSync", out dictValue)) {
                    sharedVariable.NetworkSync = Convert.ToBoolean(dictValue, CultureInfo.InvariantCulture);
                }
                if (!sharedVariable.IsGlobal && dict.TryGetValue("PropertyMapping", out dictValue)) {
                    sharedVariable.PropertyMapping = dictValue as string;
                    if (dict.TryGetValue("PropertyMappingOwner", out dictValue)) {
                        sharedVariable.PropertyMappingOwner = IndexToUnityObject(Convert.ToInt32(dictValue, CultureInfo.InvariantCulture), unityObjects) as GameObject;
                    }
                    sharedVariable.InitializePropertyMapping(variableSource as BehaviorSource);
                }

                // if the types are not equal then this shared variable used to be a different type so it should be shared
                if (!typesEqual) {
                    sharedVariable.IsShared = true;
                }

                DeserializeObject(null, sharedVariable, dict, variableSource, unityObjects);
            }

            return sharedVariable;
        }

        private static void DeserializeObject(Task task, object obj, Dictionary<string, object> dict, IVariableSource variableSource, List<UnityEngine.Object> unityObjects)
        {
            if (dict == null)
                return;

            var fields = TaskUtility.GetSerializableFields(obj.GetType());
            for (int i = 0; i < fields.Length; ++i) {
                object dictObject;
                var fieldKey = updatedSerialization ? (fields[i].FieldType.Name + fields[i].Name) : (fields[i].FieldType.Name.GetHashCode() + fields[i].Name.GetHashCode()).ToString();
                if (dict.TryGetValue(fieldKey, out dictObject)) {
                    if (typeof(IList).IsAssignableFrom(fields[i].FieldType)) {
                        var objectValues = dictObject as IList;
                        if (objectValues != null) {
                            Type type;
                            if (fields[i].FieldType.IsArray) {
                                type = fields[i].FieldType.GetElementType();
                            } else {
                                var baseFieldType = fields[i].FieldType;
#if NETFX_CORE && !UNITY_EDITOR
                                while (!baseFieldType.IsGenericType()) {
                                    baseFieldType = baseFieldType.BaseType();
                                }
#else
                                while (!baseFieldType.IsGenericType) {
                                    baseFieldType = baseFieldType.BaseType;
                                }
#endif
                                type = baseFieldType.GetGenericArguments()[0];
                            }
                            bool isIDList = type.Equals(typeof(Task)) || type.IsSubclassOf(typeof(Task));
                            if (isIDList) {
                                if (taskIDs != null) {
                                    var objectList = new List<int>();
                                    for (int j = 0; j < objectValues.Count; ++j) {
                                        objectList.Add(Convert.ToInt32(objectValues[j], CultureInfo.InvariantCulture));
                                    }
                                    taskIDs.Add(new TaskField(task, fields[i]), objectList);
                                }
                            } else {
                                if (fields[i].FieldType.IsArray) {
                                    var objectArray = Array.CreateInstance(type, objectValues.Count);
                                    for (int j = 0; j < objectValues.Count; ++j) {
                                        if (objectValues[j] == null) {
                                            objectArray.SetValue(null, j);
                                            continue;
                                        }
                                        objectArray.SetValue(ValueToObject(task, type, objectValues[j], variableSource, unityObjects), j);
                                    }
                                    fields[i].SetValue(obj, objectArray);
                                } else {
                                    IList objectList;
#if NETFX_CORE && !UNITY_EDITOR
                                    if (fields[i].FieldType.IsGenericType()) {
#else
                                    if (fields[i].FieldType.IsGenericType) {
#endif
                                        objectList = TaskUtility.CreateInstance(typeof(List<>).MakeGenericType(type)) as IList;
                                    } else {
                                        objectList = TaskUtility.CreateInstance(fields[i].FieldType) as IList;
                                    }
                                    for (int j = 0; j < objectValues.Count; ++j) {
                                        if (objectValues[j] == null) {
                                            objectList.Add(null);
                                            continue;
                                        }
                                        var value = ValueToObject(task, type, objectValues[j], variableSource, unityObjects);
                                        if (value != null && !value.Equals(null)) {
                                            objectList.Add(ValueToObject(task, type, objectValues[j], variableSource, unityObjects));
                                        } else {
                                            objectList.Add(null);
                                        }
                                    }
                                    fields[i].SetValue(obj, objectList);
                                }
                            }
                        }
                    } else {
                        var type = fields[i].FieldType;
                        // If the type is a Task then the task ID was stored. Because the task may not exist yet, wait to reference the task until all deserialization has been completed
                        if (type.Equals(typeof(Task)) || type.IsSubclassOf(typeof(Task))) {
                            if (TaskUtility.HasAttribute(fields[i], typeof(InspectTaskAttribute))) {
                                var referencedTaskDict = dictObject as Dictionary<string, object>;
                                var referencedTaskType = TaskUtility.GetTypeWithinAssembly(referencedTaskDict["Type"] as string);
                                if (referencedTaskType != null) {
                                    var referencedTask = TaskUtility.CreateInstance(referencedTaskType) as Task;
                                    DeserializeObject(referencedTask, referencedTask, referencedTaskDict, variableSource, unityObjects);
                                    fields[i].SetValue(task, referencedTask);
                                }
                            } else if (taskIDs != null) {
                                var idList = new List<int>();
                                idList.Add(Convert.ToInt32(dictObject, CultureInfo.InvariantCulture));
                                taskIDs.Add(new TaskField(task, fields[i]), idList);
                            }
                        } else {
                            var value = ValueToObject(task, type, dictObject, variableSource, unityObjects);
                            if (value != null && !value.Equals(null) && type.IsAssignableFrom(value.GetType())) {
                                fields[i].SetValue(obj, value);
                            }
                        }
                    }
#if NETFX_CORE && !UNITY_EDITOR
                } else if (typeof(SharedVariable).IsAssignableFrom(fields[i].FieldType) && !fields[i].FieldType.GetTypeInfo().IsAbstract) {
#else
                } else if (typeof(SharedVariable).IsAssignableFrom(fields[i].FieldType) && !fields[i].FieldType.IsAbstract) {
#endif
                    if (dict.TryGetValue((fields[i].FieldType.Name.GetHashCode() + fields[i].Name.GetHashCode()).ToString(), out dictObject)) {
                        var sharedVariable = TaskUtility.CreateInstance(fields[i].FieldType) as SharedVariable;
                        sharedVariable.SetValue(ValueToObject(task, fields[i].FieldType, dictObject, variableSource, unityObjects));
                        fields[i].SetValue(obj, sharedVariable);
                    } else {
                        var sharedVariable = TaskUtility.CreateInstance(fields[i].FieldType) as SharedVariable;
                        var sharedVariableValue = (fields[i].GetValue(obj) as SharedVariable);
                        if (sharedVariableValue != null) {
                            sharedVariable.SetValue(sharedVariableValue.GetValue());
                        }
                        fields[i].SetValue(obj, sharedVariable);
                    }
                }
            }
        }

        private static object ValueToObject(Task task, Type type, object obj, IVariableSource variableSource, List<UnityEngine.Object> unityObjects)
        {
            if (typeof(SharedVariable).IsAssignableFrom(type)) {
                var value = DeserializeSharedVariable(obj as Dictionary<string, object>, variableSource, false, unityObjects);
                if (value == null) {
                    // Create a blank shared variable so a value is assigned
                    value = TaskUtility.CreateInstance(type) as SharedVariable;
                }
                return value;
            } else if (type.Equals(typeof(UnityEngine.Object)) || type.IsSubclassOf(typeof(UnityEngine.Object))) {
                return IndexToUnityObject(Convert.ToInt32(obj, CultureInfo.InvariantCulture), unityObjects);
#if NETFX_CORE && !UNITY_EDITOR
            } else if (type.IsPrimitive() || type.Equals(typeof(string)) ) {
#else
            } else if (type.IsPrimitive || type.Equals(typeof(string)) ) {
#endif
                try {
                    return Convert.ChangeType(obj, type);
                }
                catch (Exception /*e*/) {
                    return null;
                }
            } else if (type.IsSubclassOf(typeof(Enum))) {
                try {
                    return Enum.Parse(type, (string)obj);
                }
                catch (Exception /*e*/) {
                    return null;
                }
            } else if (type.Equals(typeof(Vector2))) {
                return StringToVector2((string)obj);
            } else if (type.Equals(typeof(Vector3))) {
                return StringToVector3((string)obj);
            } else if (type.Equals(typeof(Vector4))) {
                return StringToVector4((string)obj);
            } else if (type.Equals(typeof(Quaternion))) {
                return StringToQuaternion((string)obj);
            } else if (type.Equals(typeof(Matrix4x4))) {
                return StringToMatrix4x4((string)obj);
            } else if (type.Equals(typeof(Color))) {
                return StringToColor((string)obj);
            } else if (type.Equals(typeof(Rect))) {
                return StringToRect((string)obj);
            } else if (type.Equals(typeof(LayerMask))) {
                return ValueToLayerMask(Convert.ToInt32(obj, CultureInfo.InvariantCulture));
            } else if (type.Equals(typeof(AnimationCurve))) {
                return ValueToAnimationCurve((Dictionary<string, object>)obj);
            } else {
                var unknownObj = TaskUtility.CreateInstance(type);
                DeserializeObject(task, unknownObj, obj as Dictionary<string, object>, variableSource, unityObjects);
                return unknownObj;
            }
        }

        private static Vector2 StringToVector2(string vector2String)
        {
            var stringSplit = vector2String.Substring(1, vector2String.Length - 2).Split(',');
            return new Vector2(float.Parse(stringSplit[0], CultureInfo.InvariantCulture), float.Parse(stringSplit[1], CultureInfo.InvariantCulture));
        }

        private static Vector3 StringToVector3(string vector3String)
        {
            var stringSplit = vector3String.Substring(1, vector3String.Length - 2).Split(',');
            return new Vector3(float.Parse(stringSplit[0], CultureInfo.InvariantCulture), float.Parse(stringSplit[1], CultureInfo.InvariantCulture), float.Parse(stringSplit[2], CultureInfo.InvariantCulture));
        }

        private static Vector4 StringToVector4(string vector4String)
        {
            var stringSplit = vector4String.Substring(1, vector4String.Length - 2).Split(',');
            return new Vector4(float.Parse(stringSplit[0], CultureInfo.InvariantCulture), float.Parse(stringSplit[1], CultureInfo.InvariantCulture), float.Parse(stringSplit[2], CultureInfo.InvariantCulture), float.Parse(stringSplit[3], CultureInfo.InvariantCulture));
        }

        private static Quaternion StringToQuaternion(string quaternionString)
        {
            var stringSplit = quaternionString.Substring(1, quaternionString.Length - 2).Split(',');
            return new Quaternion(float.Parse(stringSplit[0]), float.Parse(stringSplit[1], CultureInfo.InvariantCulture), float.Parse(stringSplit[2], CultureInfo.InvariantCulture), float.Parse(stringSplit[3], CultureInfo.InvariantCulture));
        }

        private static Matrix4x4 StringToMatrix4x4(string matrixString)
        {
            var stringSplit = matrixString.Split(null);
            var matrix = new Matrix4x4();
            matrix.m00 = float.Parse(stringSplit[0], CultureInfo.InvariantCulture);
            matrix.m01 = float.Parse(stringSplit[1], CultureInfo.InvariantCulture);
            matrix.m02 = float.Parse(stringSplit[2], CultureInfo.InvariantCulture);
            matrix.m03 = float.Parse(stringSplit[3], CultureInfo.InvariantCulture);
            matrix.m10 = float.Parse(stringSplit[4], CultureInfo.InvariantCulture);
            matrix.m11 = float.Parse(stringSplit[5], CultureInfo.InvariantCulture);
            matrix.m12 = float.Parse(stringSplit[6], CultureInfo.InvariantCulture);
            matrix.m13 = float.Parse(stringSplit[7], CultureInfo.InvariantCulture);
            matrix.m20 = float.Parse(stringSplit[8], CultureInfo.InvariantCulture);
            matrix.m21 = float.Parse(stringSplit[9], CultureInfo.InvariantCulture);
            matrix.m22 = float.Parse(stringSplit[10], CultureInfo.InvariantCulture);
            matrix.m23 = float.Parse(stringSplit[11], CultureInfo.InvariantCulture);
            matrix.m30 = float.Parse(stringSplit[12], CultureInfo.InvariantCulture);
            matrix.m31 = float.Parse(stringSplit[13], CultureInfo.InvariantCulture);
            matrix.m32 = float.Parse(stringSplit[14], CultureInfo.InvariantCulture);
            matrix.m33 = float.Parse(stringSplit[15], CultureInfo.InvariantCulture);
            return matrix;
        }

        private static Color StringToColor(string colorString)
        {
            var stringSplit = colorString.Substring(5, colorString.Length - 6).Split(',');
            return new Color(float.Parse(stringSplit[0], CultureInfo.InvariantCulture), float.Parse(stringSplit[1], CultureInfo.InvariantCulture), float.Parse(stringSplit[2], CultureInfo.InvariantCulture), float.Parse(stringSplit[3], CultureInfo.InvariantCulture));
        }

        private static Rect StringToRect(string rectString)
        {
            var stringSplit = rectString.Substring(1, rectString.Length - 2).Split(',');
            return new Rect(float.Parse(stringSplit[0].Substring(2, stringSplit[0].Length - 2), CultureInfo.InvariantCulture), //x:0.00
                            float.Parse(stringSplit[1].Substring(3, stringSplit[1].Length - 3), CultureInfo.InvariantCulture), // y:0.00
                            float.Parse(stringSplit[2].Substring(7, stringSplit[2].Length - 7), CultureInfo.InvariantCulture), // width:0.00
                            float.Parse(stringSplit[3].Substring(8, stringSplit[3].Length - 8), CultureInfo.InvariantCulture)); // height:0.00
        }

        private static LayerMask ValueToLayerMask(int value)
        {
            var layerMask = new LayerMask();
            layerMask.value = value;
            return layerMask;
        }

        private static AnimationCurve ValueToAnimationCurve(Dictionary<string, object> value)
        {
            var animationCurve = new AnimationCurve();
            object obj;
            if (value.TryGetValue("Keys", out obj)) {
                var keys = obj as List<object>;
                for (int i = 0; i < keys.Count; ++i) {
                    var keyValue = keys[i] as List<object>;
                    var keyframe = new Keyframe((float)Convert.ChangeType(keyValue[0], typeof(float), CultureInfo.InvariantCulture), (float)Convert.ChangeType(keyValue[1], typeof(float), CultureInfo.InvariantCulture),
                                                (float)Convert.ChangeType(keyValue[2], typeof(float), CultureInfo.InvariantCulture), (float)Convert.ChangeType(keyValue[3], typeof(float), CultureInfo.InvariantCulture));
                    animationCurve.AddKey(keyframe);
                }
            }
            if (value.TryGetValue("PreWrapMode", out obj)) {
                animationCurve.preWrapMode = (WrapMode)Enum.Parse(typeof(WrapMode), (string)obj);
            }
            if (value.TryGetValue("PostWrapMode", out obj)) {
                animationCurve.postWrapMode = (WrapMode)Enum.Parse(typeof(WrapMode), (string)obj);
            }
            return animationCurve;
        }

        private static UnityEngine.Object IndexToUnityObject(int index, List<UnityEngine.Object> unityObjects)
        {
            if (index < 0 || index >= unityObjects.Count) {
                return null;
            }

            return unityObjects[index];
        }
    }
}