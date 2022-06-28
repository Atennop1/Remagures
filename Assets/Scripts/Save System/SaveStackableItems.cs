using System.Collections.Generic;
using UnityEngine;

public class SaveStackableItems : SaveLoad, ISaver
{
    [SerializeField] private List<ScriptableObject> _stackableItems;
    public IReadOnlyList<ScriptableObject> StackableItems => _stackableItems;
    
    public void Save()
    {
        for (int i = 0; i < StackableItems.Count; i++)
        {
            FloatValue numberHeld = ScriptableObject.CreateInstance<FloatValue>();
            numberHeld.Value = (StackableItems[i] as BaseInventoryItem).ItemData.NumberHeld;
            SaveObjectToJson(Path + "/Item" + (i + 1) + ".json", numberHeld);
        }
    }
    
    public void Load()
    {
        for (int i = 0; i < StackableItems.Count; i++)
        {
            FloatValue numberHeld = ScriptableObject.CreateInstance<FloatValue>();
            LoadObjectFromJson(Path + "/Item" + (i + 1) + ".json", numberHeld);
            (StackableItems[i] as BaseInventoryItem).ItemData.NumberHeld = (int)numberHeld.Value;
        }
    }

    public void NewGame()
    {
        for (int i = 0; i < StackableItems.Count; i++)
        {
            (StackableItems[i] as BaseInventoryItem).ItemData.NumberHeld = 0;
            FloatValue numberHeld = ScriptableObject.CreateInstance<FloatValue>();
            numberHeld.Value = 0;
            SaveObjectToJson(Path + "/Item" + (i + 1) + ".json", numberHeld);
        }
    }

    public int IndexOf(ScriptableObject item)
    {
        return _stackableItems.IndexOf(item);
    }
}
