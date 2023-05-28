using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public void LoadScene(int id)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) {
            SaveSystem.Save();
        }
        SceneManager.LoadScene(id);
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Save();
    }
}
