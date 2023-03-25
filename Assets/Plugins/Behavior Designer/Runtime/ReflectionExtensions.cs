#if NETFX_CORE && !UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    [Flags]
    public enum BindingFlags
    {
        Default,
        Public,
        Instance,
        InvokeMethod,
        NonPublic,
        Static,
        FlattenHierarchy,
        DeclaredOnly
    }

    public static class ReflectionExtensions
    {
        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

#if UNITY_WSA_8_0 || UNITY_WSA_8_1
        public static FieldInfo GetField(this Type type, string name)
        {
            return type.GetRuntimeField(name);
        }
#endif

        public static FieldInfo[] GetFields(this Type type)
        {
            return GetFields(type, BindingFlags.Default);
        }

        public static FieldInfo[] GetFields(this Type t, BindingFlags flags)
        {
            if (!flags.HasFlag(BindingFlags.Instance) && !flags.HasFlag(BindingFlags.Static)) return null;

            var ti = t.GetTypeInfo();
            var origFields = ti.DeclaredFields;
            var results = new List<FieldInfo>();
            foreach (var field in origFields)
            {
                var isValid = (flags.HasFlag(BindingFlags.Public) && field.IsPublic) || (flags.HasFlag(BindingFlags.NonPublic) && !field.IsPublic);
                isValid &= (flags.HasFlag(BindingFlags.Static) && field.IsStatic) || (flags.HasFlag(BindingFlags.Instance) && !field.IsStatic);
                if (flags.HasFlag(BindingFlags.DeclaredOnly))
                    isValid &= field.DeclaringType == t;

                results.Add(field);
            }
            return results.ToArray();
        }

#if UNITY_WSA_8_0 || UNITY_WSA_8_1
        public static MethodInfo GetMethod(this Type type, string name, BindingFlags flags)
        {
            return type.GetTypeInfo().GetDeclaredMethod(name);
        }

        public static MethodInfo GetMethod(this Type type, string name, Type[] types)
        {
            var allMethods = type.GetTypeInfo().GetDeclaredMethods(name);
            if (allMethods == null) {
                return null;
            }
            foreach (var method in allMethods) {
                var parameters = method.GetParameters();
                if (types.Length != parameters.Length) {
                    continue;
                }
                bool match = true;
                for (int j = 0; j < parameters.Length; ++j) {
                    if (!types[j].Equals(parameters[j].ParameterType)) {
                        match = false;
                        break;
                    }
                }
                if (match) {
                    return method;
                }
            }
            return null;
        }

        public static MethodInfo GetMethod(this Type type, string name)
        {
            return type.GetTypeInfo().GetDeclaredMethod(name);
        }

        public static PropertyInfo GetProperty(this Type type, string propertyName)
        {
            return type.GetTypeInfo().GetDeclaredProperty(propertyName);
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

        public static bool IsAssignableFrom(this Type type, Type toCompare)
        {
            return type.GetTypeInfo().IsAssignableFrom(toCompare.GetTypeInfo());
        }
#endif

        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(Boolean)) return true;
            if (type == typeof(Byte)) return true;
            if (type == typeof(SByte)) return true;
            if (type == typeof(Int16)) return true;
            if (type == typeof(UInt16)) return true;
            if (type == typeof(Int32)) return true;
            if (type == typeof(UInt32)) return true;
            if (type == typeof(Int64)) return true;
            if (type == typeof(UInt64)) return true;
            if (type == typeof(IntPtr)) return true;
            if (type == typeof(UIntPtr)) return true;
            if (type == typeof(Char)) return true;
            if (type == typeof(Double)) return true;
            if (type == typeof(Single)) return true;
            return false;
        }

        public static Type BaseType(this Type type)
        {
            return type.GetTypeInfo().BaseType;
        }

        public static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        /**
         * Missing IsSubclassOf, this works well
         */
        public static bool IsSubclassOf(this Type type, System.Type parent)
        {
            return parent.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

#if UNITY_WSA_8_0 || UNITY_WSA_8_1
        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetMethod;
        }

        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.SetMethod;
        }
#endif
    }
}
#endif