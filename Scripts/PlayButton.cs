using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    SceneManagment sceneManagment;

    private void Awake()
    {
        sceneManagment = GameObject.FindGameObjectWithTag("SceneManagement").GetComponent<SceneManagment>();
    }

    private void Start()
    {
        // Сохранение
        SaveData loadedData = SaveSystem.Load();
        if (loadedData != null)
        {
            GameManager.isGame = loadedData.isGame;
        }

        if (GameManager.isGame)
        {
            gameObject.SetActive(true);
            GetComponent<Button>().onClick.AddListener(() => sceneManagment.LoadScene(2));
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
