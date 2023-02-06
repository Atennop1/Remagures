using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Tools
{
    public class SaveMaps : Saver, ISaver
    {
        [SerializeField] private List<Texture2D> _maps;

        public void Save()
        {
            for (var i = 0; i < _maps.Count; i++)
            {
                var bytes = _maps[i].EncodeToPNG();
                File.WriteAllBytes(Application.persistentDataPath + Path + "/Map" + i + ".json", bytes);
            }
        }
    
        public void Load()
        {
            for (var i = 0; i < _maps.Count; i++)
            {
                if (!File.Exists(Application.persistentDataPath + Path + "/Map" + i + ".json")) continue;
            
                var bytes = File.ReadAllBytes(Application.persistentDataPath + Path + "/Map" + i + ".json");
                _maps[i].LoadImage(bytes);
            }
        }
        
        public void NewGame()
        {
            foreach (var map in _maps)
            {
                var fillColorArray = map.GetPixels();
 
                for(var i = 0; i < fillColorArray.Length; i++)
                    fillColorArray[i] = Color.black;
                
                for (var i = 0; i < SceneManager.sceneCount; i++)
                    PlayerPrefs.DeleteKey("Visited" + SceneManager.GetSceneAt(i).path);
  
                map.SetPixels(fillColorArray);
                map.Apply();
            }
        }
    }
}
