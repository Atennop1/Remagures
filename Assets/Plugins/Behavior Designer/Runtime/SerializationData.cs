using UnityEngine;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class FieldSerializationData
    {
        [SerializeField]
        public List<string> typeName = new List<string>();
        [SerializeField]
        public List<int> fieldNameHash = new List<int>();
        [SerializeField]
        public List<int> startIndex = new List<int>();
        [SerializeField]
        public List<int> dataPosition = new List<int>();
        [SerializeField]
        public List<Object> unityObjects = new List<Object>();
        [SerializeField]
        public List<byte> byteData = new List<byte>();
        public byte[] byteDataArray;
    }
    [System.Serializable]
    public class TaskSerializationData
    {
        [SerializeField]
        public List<string> types = new List<string>();
        [SerializeField]
        public List<int> parentIndex = new List<int>();
        [SerializeField]
        public List<int> startIndex = new List<int>();
        [SerializeField]
        public List<int> variableStartIndex = new List<int>();
        [SerializeField]
        public string JSONSerialization;
        [SerializeField]
        public FieldSerializationData fieldSerializationData = new FieldSerializationData();
        [SerializeField]
        public string Version;
    }
    [System.Serializable]
    public class VariableSerializationData
    {
        [SerializeField]
        public List<int> variableStartIndex = new List<int>();
        [SerializeField]
        public string JSONSerialization = "";
        [SerializeField]
        public FieldSerializationData fieldSerializationData = new FieldSerializationData();
    }
}