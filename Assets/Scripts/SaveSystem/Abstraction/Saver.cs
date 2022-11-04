using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Remagures.SaveSystem.Abstraction
{
    public abstract class Saver : MonoBehaviour
    {
        [field: SerializeField] public string Path { get; private set; }

        protected void SaveObjectToJson(string path, Object toSave)
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Create(Application.persistentDataPath + path);
        
            var json = JsonUtility.ToJson(toSave);
            binaryFormatter.Serialize(file, json);
            file.Close();
        }
    
        protected void LoadObjectFromJson(string path, Object toLoad)
        {
            var binaryFormatter = new BinaryFormatter();
            if (!File.Exists(Application.persistentDataPath + path)) return;
        
            var file = File.Open(Application.persistentDataPath + path, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), toLoad);
            file.Close();
        }
    }
}
