using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManagement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelDescription;
    [SerializeField] GameObject levelBlock;
    [SerializeField] Button bttnStartLevel;

    public string levelTheme;
    int levelId;

    public static int maxLevel = 0;

    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<GameObject> modelsGO;
    [SerializeField] List<Model> models;

    [Header("Camera")]
    [SerializeField] GameObject camera;
    Vector3 targetCam;
    float velocityCam = 50f;

    void Awake()
    {
        // Сохранение
        SaveData loadedData = SaveSystem.Load();
        if (loadedData != null)
        {
            maxLevel = loadedData.maxLevel;
        }

        foreach (var modelGO in modelsGO)
        {
            models.Add(modelGO.GetComponent<Model>());
        }

        levelId = 0;

        SetNewTheme(0);

        camera.transform.position = targetCam;
    }

    private void FixedUpdate()
    {
        camera.transform.position = Vector3.MoveTowards(camera.transform.position, models[levelId].cameraPos, Time.deltaTime * velocityCam);
    }

    public void SetNewTheme(int changer)
    {
        levelId += changer;
        if (levelId < 0)
        {
            levelId = 0;
        }
        if (levelId > 9)
        {
            levelId = 9;
        }

        if (levelId > maxLevel)
        {
            bttnStartLevel.interactable = false;
            levelBlock.GetComponentInChildren<Image>().sprite = sprites[0];
            levelBlock.GetComponentInChildren<TextMeshProUGUI>().text = "Закрыто";
            levelBlock.SetActive(true);

        } else
        {
            bttnStartLevel.interactable = true;
            if (modelsGO[levelId].activeSelf)
            {
                levelBlock.SetActive(false);
            }
            else
            {
                levelBlock.GetComponentInChildren<Image>().sprite = sprites[levelId + 1];
                levelBlock.GetComponentInChildren<TextMeshProUGUI>().text = "";
                levelBlock.SetActive(true);
            }
        }

        levelTheme = models[levelId].theme;
        levelDescription.text = models[levelId].levelDescription;
        targetCam = models[levelId].cameraPos;
        GameManager.levelTheme = levelTheme;
    }
}
