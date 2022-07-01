using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDialogs : SaveLoad, ISaver
{
    [SerializeField] private List<DialogDatabase> _databases;

    public void Save()
    {
        for (int i = 0; i < _databases.Count; i++)
        {
            StringValue stringValue = ScriptableObject.CreateInstance<StringValue>();
            stringValue.Value = _databases[i].CurrentNodeGUID;
            SaveObjectToJson(Path + "/CurrentDialog" + (i + 1) + "NodeGUID.json", stringValue);
        }
    }

    public void Load()
    {
        for (int i = 0; i < _databases.Count; i++)
        {
            if (!File.Exists(Application.persistentDataPath + Path + "/CurrentDialog" + (i + 1) + "NodeGUID.json"))
            {
                _databases[i].CurrentNodeGUID = _databases[i].BaseNodeGUID;
            }
            else
            {
                StringValue stringValue = ScriptableObject.CreateInstance<StringValue>();
                LoadObjectFromJson(Path + "/CurrentDialog" + (i + 1) + "NodeGUID.json", stringValue);
                _databases[i].CurrentNodeGUID = stringValue.Value;
            }
        }
    }
    
    public void NewGame()
    {
        for (int i = 0; i < _databases.Count; i++)
        {
            _databases[i].CurrentNodeGUID = _databases[i].BaseNodeGUID;
        }
    }
}
