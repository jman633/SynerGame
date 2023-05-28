using UnityEngine;

public class FirstTimeWindow : MonoBehaviour
{
    private const string FirstTimeKey = "FirstTime";

    public GameObject firstTimeWindow;

    private void Start()
    {
        // Проверяем, является ли текущий запуск первым запуском игры
        if (PlayerPrefs.GetInt(FirstTimeKey, 0) == 0)
        {
            // Показываем окно только при первом запуске
            firstTimeWindow.SetActive(true);

            // Устанавливаем флаг первого запуска в значение "1"
            PlayerPrefs.SetInt(FirstTimeKey, 1);
        }
        else
        {
            // Остальные запуски игры
            firstTimeWindow.SetActive(false);
        }
    }
}