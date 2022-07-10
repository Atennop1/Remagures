using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public abstract class Saver : MonoBehaviour
{
    [field: SerializeField] public string Path { get; private set; }
    
    public void SaveObjectToJson(string path, ScriptableObject toSave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + path);
        string json = JsonUtility.ToJson(toSave);
        bf.Serialize(file, json);
        file.Close();
    }
    
    public void LoadObjectFromJson(string path, ScriptableObject toLoad)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + path))
        {
            FileStream file = File.Open(Application.persistentDataPath + path, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), toLoad);
            file.Close();
        }
    }
}
