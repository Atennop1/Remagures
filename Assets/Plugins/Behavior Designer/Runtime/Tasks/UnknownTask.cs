using UnityEngine;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class UnknownTask : Task
    {
        [HideInInspector]
        public string JSONSerialization;
        [HideInInspector]
        public List<int> fieldNameHash = new List<int>();
        [HideInInspector]
        public List<int> startIndex = new List<int>();
        [HideInInspector]
        public List<int> dataPosition = new List<int>();
        [HideInInspector]
        public List<Object> unityObjects = new List<Object>();
        [HideInInspector]
        public List<byte> byteData = new List<byte>();
    }
}