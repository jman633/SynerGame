using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerController : MonoBehaviour
{
    private static SoundManagerController instance;
    private AudioSource audioSource;
    private bool isPlaying = false;

    [SerializeField]
    private string targetSceneName;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName)
        {
            // Переключаем музыку на целевой сцене, если она еще не играет
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
        }
        else
        {
            // Останавливаем музыку на остальных сценах
            audioSource.Stop();
            isPlaying = false;
        }
    }
}