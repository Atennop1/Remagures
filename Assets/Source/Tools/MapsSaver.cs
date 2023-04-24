using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Tools
{
    public sealed class MapsSaver
    {
        private readonly List<Texture2D> _maps;
        private readonly string _path;

        public MapsSaver(List<Texture2D> maps, string path)
        {
            _maps = maps ?? throw new ArgumentNullException(nameof(maps));
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public void Save()
        {
            for (var i = 0; i < _maps.Count; i++)
            {
                var bytes = _maps[i].EncodeToPNG();
                File.WriteAllBytes(Application.persistentDataPath + _path + "/Map" + i + ".json", bytes);
            }
        }
    
        public void Load()
        {
            for (var i = 0; i < _maps.Count; i++)
            {
                if (!File.Exists(Application.persistentDataPath + _path + "/Map" + i + ".json")) continue;
            
                var bytes = File.ReadAllBytes(Application.persistentDataPath + _path + "/Map" + i + ".json");
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
