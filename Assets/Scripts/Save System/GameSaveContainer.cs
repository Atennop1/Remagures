using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSaveContainer : MonoBehaviour
{
    [SerializeField] private BoolValue _isNewGame;
    [SerializeField] private List<Object> savablesObjects;

    private List<ISaver> _savables = new List<ISaver>();
    public IReadOnlyList<ISaver> Savables => _savables;

    public void Awake()
    {
        InitLists();
        LoadGame();
        if (!File.Exists(Application.persistentDataPath + "/Good guy or bad guy. That is the question.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/Good guy or bad guy. That is the question.txt");
            file.Close();
            File.WriteAllText(Application.persistentDataPath + "/Good guy or bad guy. That is the question.txt", "Hey, if you're reading this file, you're either very inquisitive or you want to modify the game's save files.\n\nI'm warning you. I have made sure that any attempt to change these files will either simply not work, or corrupt your saves and, as a result, delete all game progress.\n\nForewarned is forearmed.\n\n-Atennop");
        }
    }

    public void InitLists()
    {
        _savables.Clear();
        for (int i = 0; i < savablesObjects.Count; i++)
            _savables.Add(savablesObjects[i] as ISaver);
    }

    public void SaveGame()
    {
        CheckAllDirs();
        foreach (ISaver savable in Savables)
            savable.Save();
    }

    public void LoadGame()
    {
        CheckAllDirs();
        foreach (ISaver savable in Savables)
            savable.Load();
    }

    public void NewGame()
    {
        _isNewGame.Value = false;
        foreach (ISaver savable in Savables)
            savable.NewGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartCutscene");
    }

    private void CheckAllDirs()
    {
        foreach (ISaver savable in Savables)
            CheckDir((savable as Saver).Path);
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