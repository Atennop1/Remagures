using System.Collections.Generic;
using System.IO;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Tools
{
    public class GameSaver : MonoBehaviour
    {
        [SerializeField] private BoolValue _isNewGame;
        [SerializeField] private List<MonoBehaviour> savablesObjects;
    
        public IEnumerable<ISaver> Savables => _savables;
        private List<ISaver> _savables = new();

        public void Awake()
        {
            InitLists();
            LoadGame();
        
            if (File.Exists(Application.persistentDataPath + "/Good guy or bad guy. That is the question.txt")) return;
            var file = File.Create(Application.persistentDataPath + "/Good guy or bad guy. That is the question.txt");
        
            file.Close();
            File.WriteAllText(Application.persistentDataPath + "/Good guy or bad guy. That is the question.txt", "Hey, if you're reading this file, you're either very inquisitive or you want to modify the game's save files.\n\nI'm warning you. I have made sure that any attempt to change these files will either simply not work, or corrupt your saves and, as a result, delete all game progress.\n\nForewarned is forearmed.\n\n-Atennop");
        }

        private void InitLists()
        {
            _savables.Clear();
            foreach (var savable in savablesObjects)
                _savables.Add(savable as ISaver);
        }

        public void SaveGame()
        {
            CheckAllDirs();
            foreach (var savable in Savables)
                savable.Save();
        }

        private void LoadGame()
        {
            CheckAllDirs();
            foreach (var savable in Savables)
                savable.Load();
        }

        public void NewGame()
        {
            _isNewGame.Value = false;
            foreach (var savable in Savables)
                savable.NewGame();
        
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartCutscene");
        }

        private void CheckAllDirs()
        {
            foreach (var savable in Savables)
                CheckDir((savable as Saver)?.Path);
        }

        public void CheckDir(string path)
        {
            if (!Directory.Exists(Application.persistentDataPath + path))
                Directory.CreateDirectory(Application.persistentDataPath + path);
        }

        public void OnDisable()
        {
            SaveGame();
        }

        public void OnApplicationQuit()
        {
            SaveGame();
        }
    
        public void OnApplicationPause(bool pause)
        {
            if (Application.platform == RuntimePlatform.Android)
                SaveGame();
        }
    }
}