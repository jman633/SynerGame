using UnityEngine;

public class PopupWindow : MonoBehaviour
{
    public GameObject popupWindow;

    public void OpenPopupWindow()
    {
        popupWindow.SetActive(true);
    }

    public void ClosePopupWindow()
    {
        popupWindow.SetActive(false);
    }
}