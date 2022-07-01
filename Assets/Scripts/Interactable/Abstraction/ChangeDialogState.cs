using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChangeDialogState : MonoBehaviour
{
    [SerializeField] private DialogValue _dialogValue;
    [SerializeField] private List<DialogDatabase> _databases;
    [SerializeField, NonReorderable] private List<StringStringDictionary> _oldNewNodes;

    public void ChangeDatabaseState()
    {
        for (int i = 0; i < _databases.Count; i++)
            if (_oldNewNodes[i].ContainsKey(_databases[i].CurrentNodeGUID))
                _databases[i].CurrentNodeGUID = _oldNewNodes[i][_databases[i].CurrentNodeGUID]; 
    }
}

[System.Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> { }