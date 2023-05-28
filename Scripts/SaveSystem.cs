using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class SaveSystem
{
    public static void Save()
    {
        SaveData data = new SaveData(
                1f,
                LevelManagement.maxLevel,
                Builder.modelsActive,
                Builder.modelsTranslates,
                GameManager.isGame,
                GameManager.numberQuestion,
                GameManager.levelTheme);

        string json = JsonConvert.SerializeObject(
            data,
            new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
        );

        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<SaveData>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public float settingsVolume;
    public int maxLevel;
    public Dictionary<string, bool> modelsActive;
    public Dictionary<string, List<Vector3>> modelsTranslates;
    public bool isGame;
    public int numberQuestion = 0;
    public string levelTheme = "";

    public SaveData(
        float settingsVolume,
        int maxLevel,
        Dictionary<string, bool> modelsActive,
        Dictionary<string, List<Vector3>> modelsTranslates,
        bool isGame,
        int numberQuestion = 0,
        string levelTheme = "")
    {
        this.settingsVolume = settingsVolume;
        this.maxLevel = maxLevel;
        this.modelsActive = modelsActive;
        this.modelsTranslates = modelsTranslates;
        this.isGame = isGame;
        this.numberQuestion = numberQuestion;
        this.levelTheme = levelTheme;
    }
}
