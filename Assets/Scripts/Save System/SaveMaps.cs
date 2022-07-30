using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveMaps : Saver, ISaver
{
    [SerializeField] private List<Texture2D> _maps;

    public void Save()
    {
        for (int i = 0; i < _maps.Count; i++)
        {
            byte[] bytes = _maps[i].EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + Path + "/Map" + i + ".json", bytes);
        }
    }
    
    public void Load()
    {
        for (int i = 0; i < _maps.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + Path + "/Map" + i + ".json"))
            {
                byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + Path + "/Map" + i + ".json");
                _maps[i].LoadImage(bytes);
            }
        }
    }

    public void NewGame()
    {
        foreach (Texture2D map in _maps)
        {
            var fillColorArray = map.GetPixels();
 
            for(var i = 0; i < fillColorArray.Length; i++)
                fillColorArray[i] = Color.black;
  
            map.SetPixels(fillColorArray);
            map.Apply();
        }
    }
}
