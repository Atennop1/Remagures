using System.Reflection;
using System.Collections.Generic;
using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime
{
    public class TaskUtility
    {
        public static char[] TrimCharacters = new char[] { '/' };

        private static Dictionary<string, Type> typeLookup = new Dictionary<string, Type>();
        private static List<Assembly> loadedAssemblies = null;
        private static Dictionary<Type, FieldInfo[]> allFieldsLookup = new Dictionary<Type, FieldInfo[]>();
        private static Dictionary<Type, FieldInfo[]> serializableFieldsLookup = new Dictionary<Type, FieldInfo[]>();
        private static Dictionary<Type, FieldInfo[]> publicFieldsLookup = new Dictionary<Type, FieldInfo[]>();
        private static Dictionary<FieldInfo, Dictionary<Type, bool>> hasFieldLookup = new Dictionary<FieldInfo, Dictionary<Type, bool>>();

        public static object CreateInstance(Type t)
        {
#if NETFX_CORE && !UNITY_EDITOR
            if (t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(Nullable<>)) {
#else
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)) {
#endif
                t = Nullable.GetUnderlyingType(t);
            }
#if NETFX_CORE && !UNITY_EDITOR
            return Activator.CreateInstance(t);
#else
            return Activator.CreateInstance(t, true);
#endif
        }

        public static FieldInfo[] GetAllFields(Type t)
        {
            FieldInfo[] fieldInfo = null;
            if (!allFieldsLookup.TryGetValue(t, out fieldInfo)) {
                var fieldList = ObjectPool.Get<List<FieldInfo>>();
                fieldList.Clear();
#if NETFX_CORE && !UNITY_EDITOR
                var flags = System.BindingFlags.Public | System.BindingFlags.NonPublic | System.BindingFlags.Instance | System.BindingFlags.DeclaredOnly;
#else
                var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
#endif
                GetFields(t, ref fieldList, (int)flags);
                fieldInfo = fieldList.ToArray();
                ObjectPool.Return(fieldList);
                allFieldsLookup.Add(t, fieldInfo);
            }
            return fieldInfo;
        }

        public static FieldInfo[] GetPublicFields(Type t)
        {
            FieldInfo[] fieldInfo = null;
            if (!publicFieldsLookup.TryGetValue(t, out fieldInfo)) {
                var fieldList = ObjectPool.Get<List<FieldInfo>>();
                fieldList.Clear();
#if NETFX_CORE && !UNITY_EDITOR
                var flags = System.BindingFlags.Public | System.BindingFlags.Instance | System.BindingFlags.DeclaredOnly;
#else
                var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
#endif
                GetFields(t, ref fieldList, (int)flags);
                fieldInfo = fieldList.ToArray();
                ObjectPool.Return(fieldList);
                publicFieldsLookup.Add(t, fieldInfo);
            }
            return fieldInfo;
        }

        public static FieldInfo[] GetSerializableFields(Type t)
        {
            FieldInfo[] fieldInfo = null;
            if (!serializableFieldsLookup.TryGetValue(t, out fieldInfo)) {
                var fieldList = ObjectPool.Get<List<FieldInfo>>();
                fieldList.Clear();

#if NETFX_CORE && !UNITY_EDITOR
                var flags = System.BindingFlags.Public | System.BindingFlags.NonPublic | System.BindingFlags.Instance | System.BindingFlags.DeclaredOnly;
#else
                var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
#endif
                GetSerializableFields(t, fieldList, (int)flags);
                fieldInfo = fieldList.ToArray();
                ObjectPool.Return(fieldList);
                serializableFieldsLookup.Add(t, fieldInfo);
            }
            return fieldInfo;
        }

        private static void GetSerializableFields(Type t, IList<FieldInfo> fieldList, int flags)
        {
            if (t == null || t.Equals(typeof(ParentTask)) || t.Equals(typeof(Task)) || t.Equals(typeof(SharedVariable)))
                return;

#if NETFX_CORE && !UNITY_EDITOR
            var fields = t.GetFields((System.BindingFlags)flags);
#else
            var fields = t.GetFields((BindingFlags)flags);
#endif
            for (int i = 0; i < fields.Length; ++i) {
                if (!fields[i].IsPublic && !HasAttribute(fields[i], typeof(UnityEngine.SerializeField))) {
                    continue;
                }

                fieldList.Add(fields[i]);
            }
#if NETFX_CORE && !UNITY_EDITOR
            GetSerializableFields(t.BaseType(), fieldList, flags);
#else
            GetSerializableFields(t.BaseType, fieldList, flags);
#endif
        }

        private static void GetFields(Type t, ref List<FieldInfo> fieldList, int flags)
        {
            if (t == null || t.Equals(typeof(ParentTask)) || t.Equals(typeof(Task)) || t.Equals(typeof(SharedVariable)))
                return;

#if NETFX_CORE && !UNITY_EDITOR
            var fields = t.GetFields((System.BindingFlags)flags);
#else
            var fields = t.GetFields((BindingFlags)flags);
#endif
            for (int i = 0; i < fields.Length; ++i) {
                fieldList.Add(fields[i]);
            }
#if NETFX_CORE && !UNITY_EDITOR
            GetFields(t.BaseType(), ref fieldList, flags);
#else
            GetFields(t.BaseType, ref fieldList, flags);
#endif
        }

        public static Type GetTypeWithinAssembly(string typeName)
        {
            Type type;
            // cache the results for quicker repeated lookup
            if (typeLookup.TryGetValue(typeName, out type)) {
                return type;
            }

            type = Type.GetType(typeName);
            // look in the loaded assemblies
            if (type == null) {
                if (loadedAssemblies == null) {
#if NETFX_CORE && !UNITY_EDITOR
                    loadedAssemblies = GetStorageFileAssemblies(typeName).Result;
#else
                    loadedAssemblies = new List<Assembly>();
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    for (int i = 0; i < assemblies.Length; ++i) {
                        loadedAssemblies.Add(assemblies[i]);
                    }
#endif
                }
                for (int i = 0; i < loadedAssemblies.Count; ++i) {
                    type = loadedAssemblies[i].GetType(typeName);
                    if (type != null) {
                        break;
                    }
                }
            }
            if (type != null) {
                typeLookup.Add(typeName, type);
            } else if (typeName.Contains("BehaviorDesigner.Runtime.Tasks.Basic")) { // Updated the task namespace with Behavior Designer 1.6.1.
                return GetTypeWithinAssembly(typeName.Replace("BehaviorDesigner.Runtime.Tasks.Basic", "BehaviorDesigner.Runtime.Tasks.Unity"));
            }
            return type;
        }

#if NETFX_CORE && !UNITY_EDITOR
        private static async System.Threading.Tasks.Task<List<string>> GetStorageFileAssemblies(string typeName) {
            var loadedAssemblies = new List<string>();
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            foreach (var file in await folder.GetFilesAsync()) {
                if (file.FileType.Equals(".dll")) {
                    loadedAssemblies.Add(System.IO.Path.GetFileNameWithoutExtension(file.Name));
                }
            }
            return loadedAssemblies;
        }
#endif

        public static bool CompareType(Type t, string typeName)
        {
            var type = System.Type.GetType(typeName + ", Assembly-CSharp");
            if (type == null) {
                type = System.Type.GetType(typeName + ", Assembly-CSharp-firstpass");
            }
            return t.Equals(type);
        }

        public static bool HasAttribute(FieldInfo field, Type attribute)
        {
            if (field == null) {
                return false;
            }

            bool hasAttribute;
            Dictionary<Type, bool> typeLookup;
            if (!hasFieldLookup.TryGetValue(field, out typeLookup)) {
                typeLookup = new Dictionary<Type, bool>();
                hasFieldLookup.Add(field, typeLookup);
            }

            if (!typeLookup.TryGetValue(attribute, out hasAttribute)) {

#if NETFX_CORE && !UNITY_EDITOR
                hasAttribute = field.GetCustomAttribute(attribute) != null;
#else
                hasAttribute = field.GetCustomAttributes(attribute, false).Length > 0;
#endif
                typeLookup.Add(attribute, hasAttribute);
            }

            return hasAttribute;
        }
    }
}