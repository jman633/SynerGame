using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataModels : MonoBehaviour
{

    [SerializeField] List<GameObject> models = new();

    private Dictionary<string, int> levelLayers = new()
    {
        { "1812", 6 },
        { "Архангельский собор", 5 },
        { "МГУ", 10 },
        { "Минин и Пожарский", 5 },
        { "Москва-Сити", 10 },
        { "Останкинская башня", 6 },
        { "Спасская башня", 7 },
        { "Храм Василия", 7 },
        { "Храм Христа Спасителя", 6 },
        { "Царь-колокол", 7 }
    };

    private Dictionary<string, int> themesId = new();

    private Dictionary<string, Vector3> camPos = new()
    {
        { "1812", new Vector3(0, 8, 13) },
        { "Архангельский собор", new Vector3(0, 8, 13) },
        { "МГУ", new Vector3(0, 13, 20) },
        { "Минин и Пожарский", new Vector3(0, 9, 13) },
        { "Москва-Сити", new Vector3(0, 11, 20) },
        { "Останкинская башня", new Vector3(0, 30, 30) },
        { "Спасская башня", new Vector3(0, 15, 20) },
        { "Храм Василия", new Vector3(0, 20, 26) },
        { "Храм Христа Спасителя", new Vector3(0, 13, 20) },
        { "Царь-колокол", new Vector3(0, 8, 13) }
    };

    private void Awake()
    {
        int id = 0;
        foreach (var level in levelLayers)
        {
            themesId.Add(level.Key, id);
            id++;
        }
    }


    public Vector3 GetCamPos(string levelTheme)
    {
        return camPos[levelTheme];
    }

    public int GetLevelLayers(string levelTheme)
    {
        return levelLayers[levelTheme];
    }

    public GameObject GetModel(string levelTheme)
    {
        int id = themesId[levelTheme];
        return models[id];
    }
}
