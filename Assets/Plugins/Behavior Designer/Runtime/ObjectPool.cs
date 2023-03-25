using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime
{
    public static class ObjectPool
    {
        private static Dictionary<Type, object> poolDictionary = new Dictionary<Type, object>();

        public static T Get<T>()
        {
            if (poolDictionary.ContainsKey(typeof(T))) {
                var pooledObjects = poolDictionary[typeof(T)] as Stack<T>;
                if (pooledObjects.Count > 0) {
                    return pooledObjects.Pop();
                }
            }
            return (T)TaskUtility.CreateInstance(typeof(T));
        }

        public static void Return<T>(T obj)
        {
            if (obj == null) {
                return;
            }
            object value;
            if (poolDictionary.TryGetValue(typeof(T), out value)) {
                var pooledObjects = value as Stack<T>;
                pooledObjects.Push(obj);
            } else {
                var pooledObjects = new Stack<T>();
                pooledObjects.Push(obj);
                poolDictionary.Add(typeof(T), pooledObjects);
            }
        }

        public static void Clear()
        {
            poolDictionary.Clear();
        }
    }
}