using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] List<GameObject> models;
    public static Dictionary<string, bool> modelsActive = new();
    public static Dictionary<string, List<Vector3>> modelsTranslates = new();

    private void Awake()
    {
        // Сохранение
        SaveData loadedData = SaveSystem.Load();
        if (loadedData != null && loadedData.modelsActive != null)
        {
            modelsActive = new Dictionary<string, bool>(loadedData.modelsActive);
            modelsTranslates = new Dictionary<string, List<Vector3>>(loadedData.modelsTranslates);
        }

        for (int i = 0; i < models.Count; i++)
        {
            if (modelsActive.ContainsKey(models[i].name))
            {
                models[i].SetActive(modelsActive[models[i].name]);
            } else
            {
                modelsActive.Add(models[i].name, false);
                models[i].SetActive(false);
            }
        }
    }

    void Start()
    {
        int id = 0;
        foreach (var activeSelf in modelsActive)
        {
            if (activeSelf.Value)
            {
                for (int i = 0; i < models[id].transform.childCount; i++)
                {
                    GameObject layer = models[id].transform.GetChild(i).gameObject;
                    layer.transform.Translate(modelsTranslates[activeSelf.Key][i]);
                }
            }
            id++;
        }
    }
}
