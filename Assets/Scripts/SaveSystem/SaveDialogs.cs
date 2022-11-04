using System;
using System.Collections.Generic;
using System.IO;
using Remagures.DialogSystem.Core;
using Remagures.SaveSystem.Abstraction;
using Remagures.SO.PlayerStuff;
using UnityEngine;

namespace Remagures.SaveSystem
{
    public class SaveDialogs : Saver, ISaver
    {
        [SerializeField] private List<DialogDatabase> _databases;

        public void Save()
        {
            for (var i = 0; i < _databases.Count; i++)
            {
                var stringValue = ScriptableObject.CreateInstance<StringValue>();
                stringValue.Value = _databases[i].CurrentNodeGUID;
                SaveObjectToJson(Path + "/CurrentDialog" + (i + 1) + "NodeGUID.json", stringValue);
            }
        }

        public void Load()
        {
            for (var i = 0; i < _databases.Count; i++)
            {
                if (!File.Exists(Application.persistentDataPath + Path + "/CurrentDialog" + (i + 1) + "NodeGUID.json"))
                {
                    _databases[i].CurrentNodeGUID = _databases[i].BaseNodeGUID;
                }
                else
                {
                    var stringValue = ScriptableObject.CreateInstance<StringValue>();
                    LoadObjectFromJson(Path + "/CurrentDialog" + (i + 1) + "NodeGUID.json", stringValue);
                    _databases[i].CurrentNodeGUID = stringValue.Value;
                }
            }
        }
    
        public void NewGame()
        {
            foreach (var database in _databases)
                database.CurrentNodeGUID = database.BaseNodeGUID;
        }
    }
}
