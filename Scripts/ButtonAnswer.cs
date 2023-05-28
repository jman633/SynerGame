using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnswer : MonoBehaviour
{
    GameManager gameManager;
    bool isSelected;

    [SerializeField] public Color selectedColor;
    [SerializeField] public Color unSelectedColor;
    [SerializeField] public Color correctAnswer;
    [SerializeField] public Color unCorrectAnswer;

    void Start()
    {
        isSelected = true;
        ChangeColor();

        gameManager = GameObject.FindGameObjectWithTag("MND").GetComponentInChildren<GameManager>();   
    }

    public void SetNumber(int id)
    {
        GetComponent<Button>().onClick.AddListener(() => gameManager.AddAnswer(id));
    }

    public void ChangeColor()
    {
        isSelected = !isSelected;
        if (isSelected)
        {
            GetComponent<Image>().color = selectedColor;
        } else
        {
            GetComponent<Image>().color = unSelectedColor;
        }
    }
}
