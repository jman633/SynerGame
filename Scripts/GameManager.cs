using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool isGame = false;

    [SerializeField] GameObject finishButton;
    [SerializeField] GameObject checkAnswerButton;
    [SerializeField] GameObject camera;

    [Header("Datas")]
    DataQuestions dataQuestions;
    DataModels dataModels;

    [Header("Question")]
    public static int numberQuestion;
    [SerializeField] GameObject questionPanel;
    [SerializeField] TextMeshProUGUI questionText;
    public List<bool> playersAnswers = new() { };
    List<bool> correctAnswers = new() { };
    [SerializeField] GameObject bttnAnswerPrefab;
    [SerializeField] GameObject mainPanelBttnAnswer;

    [Header("static")]
    public static string levelTheme;

    [Header("CloseAndOpenPanel")]
    const int frames = 30;
    const float timeFrame = 0.02f;
    float sizeChange = 1f / frames;

    [Header("Models")]
    [SerializeField] GameObject modelMainObject;
    GameObject model;

    private void Awake()
    {
        dataQuestions = GameObject.FindGameObjectWithTag("MND").GetComponentInChildren<DataQuestions>();
        dataModels = GameObject.FindGameObjectWithTag("MND").GetComponentInChildren<DataModels>();
    }

    void Start()
    {
        // Сохранение
        SaveData loadedData = SaveSystem.Load();
        if (loadedData != null)
        {
            isGame = loadedData.isGame;
            if (isGame)
            {
                numberQuestion = loadedData.numberQuestion;
                levelTheme = loadedData.levelTheme;
            }
            else
            {
                isGame = true;
                numberQuestion = 0;
            }
        }
        else
        {
            isGame = true;
            numberQuestion = 0;
        }

        finishButton.SetActive(false);
        CreateModel();
        GetQuestion();
        StartCoroutine("OpenPanel");
    }

    public void AddAnswer(int id)
    {
        playersAnswers[id] = !playersAnswers[id];
    }

    public void FinishLayer()
    {
        bool isAns = false;
        foreach (var ans in playersAnswers)
        {
            if (ans)
            {
                isAns = true;
                break;
            }
        }
        if (!isAns)
        {
            return;
        }
        Debug.Log("Finish Layer");
        StartCoroutine("ShowCorrectAnswer");

        //sum
        int sum = 0;
        for (int i = 0; i < playersAnswers.Count; i++)
        {
            if (playersAnswers[i] != correctAnswers[i]) sum -= 1;
        }

        int factor = (int)Mathf.Pow(-1, Random.Range(0, 10));
        float smoothness = Random.Range(1, 2);
        Vector3 dirVector = new Vector3(sum * factor * smoothness, 0, 0);

        // Change Layer on GameScene
        GameObject layer = model.transform.GetChild(numberQuestion).gameObject;
        layer.transform.Translate(dirVector);
        layer.SetActive(true);

        // Change Layer on LevelScene
        Builder.modelsTranslates[levelTheme][numberQuestion] = dirVector;
        Builder.modelsActive[levelTheme] = true;

        if (numberQuestion + 1 >= dataModels.GetLevelLayers(levelTheme))
        {
            finishButton.SetActive(true);
            isGame = false;
            LevelManagement.maxLevel += 1;
        }
    }

    public void NextLevel()
    {
        numberQuestion += 1;
        GetQuestion();
        CloseAndOpenPanel();
    }

    void ClearMainBttnPanel()
    {
        for (int i = 0; i < mainPanelBttnAnswer.transform.childCount; i++)
        {
            Destroy(mainPanelBttnAnswer.transform.GetChild(i).gameObject);
        }
    }

    void CreateModel()
    {
        camera.transform.position = dataModels.GetCamPos(levelTheme);

        if (!Builder.modelsTranslates.ContainsKey(levelTheme))
        {
            Builder.modelsTranslates.Add(levelTheme, new List<Vector3>(new Vector3[dataModels.GetLevelLayers(levelTheme)]));
        }
        model = Instantiate(dataModels.GetModel(levelTheme), modelMainObject.transform);
        for (int i = 0; i < numberQuestion; i++)
        {
            GameObject layer = model.transform.GetChild(i).gameObject;
            layer.transform.Translate(SaveSystem.Load().modelsTranslates[levelTheme][i]);
        }
        for (int i = numberQuestion; i < model.transform.childCount; i++)
        {
            model.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void GetQuestion()
    {
        string question = dataQuestions.GetQuestion(levelTheme)[numberQuestion];
        questionText.text = question;

        List<string> corAns = dataQuestions.GetCorrectAnswers(levelTheme, numberQuestion);
        List<string> wroAns = dataQuestions.GetWrongAnswers(levelTheme, numberQuestion);

        int numCorAns = 0;
        int numWroAns = 0;

        int countAnswers = corAns.Count + wroAns.Count;
        playersAnswers = new List<bool>(new bool[countAnswers]);
        correctAnswers = new List<bool>(new bool[countAnswers]);

        for (int i = 0; i < countAnswers; i++)
        {
            string ans;
            GameObject bttnAnswer = Instantiate(bttnAnswerPrefab, mainPanelBttnAnswer.transform);
            bttnAnswer.GetComponent<ButtonAnswer>().SetNumber(i);
            TextMeshProUGUI textOnBttnAnswer = bttnAnswer.GetComponentInChildren<TextMeshProUGUI>();

            if (numWroAns >= wroAns.Count)
            {
                textOnBttnAnswer.text = corAns[numCorAns];
                numCorAns++;
                correctAnswers[i] = true;
                continue;
            }

            if (numCorAns >= corAns.Count)
            {
                textOnBttnAnswer.text = wroAns[numWroAns];
                numWroAns++;
                continue;
            }

            if (Random.Range(0, 10) >= 5)
            {
                ans = corAns[numCorAns];
                numCorAns++;
                correctAnswers[i] = true;
            }
            else
            {
                ans = wroAns[numWroAns];
                numWroAns++;
            }
            textOnBttnAnswer.text = ans;

        }
    }

    public void CloseAndOpenPanel()
    {
        if (questionPanel.activeSelf)
        {
            StartCoroutine("ClosePanel");
        }
        else
        {
            StartCoroutine("OpenPanel");
        }
    }

    IEnumerator ShowCorrectAnswer()
    {
        float time = 2f;
        checkAnswerButton.SetActive(false);

        for (int i = 0; i < mainPanelBttnAnswer.transform.childCount; i++) {
            Image buttonImage = mainPanelBttnAnswer.transform.GetChild(i).gameObject.GetComponent<Image>();
            ButtonAnswer buttonAns = mainPanelBttnAnswer.transform.GetChild(i).gameObject.GetComponent<ButtonAnswer>();

            if (correctAnswers[i])
            {
                buttonImage.color = buttonAns.correctAnswer;
            }
            if (!correctAnswers[i] && playersAnswers[i])
            {
                buttonImage.color = buttonAns.unCorrectAnswer;
            }
        }
        yield return new WaitForSeconds(time);
        CloseAndOpenPanel();
        ClearMainBttnPanel();
        checkAnswerButton.SetActive(true);
    }

    IEnumerator ClosePanel()
    {
        questionPanel.transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < frames; i++)
        {
            questionPanel.transform.localScale -= new Vector3(sizeChange, sizeChange, 0);
            yield return new WaitForSeconds(timeFrame);
        }
        questionPanel.transform.localScale = new Vector3(0, 0, 1);
        questionPanel.SetActive(false);
    }

    IEnumerator OpenPanel()
    {
        questionPanel.transform.localScale = new Vector3(0, 0, 1);
        questionPanel.SetActive(true);
        for (int i = 0; i < frames; i++)
        {
            questionPanel.transform.localScale += new Vector3(sizeChange, sizeChange, 0);
            yield return new WaitForSeconds(timeFrame);
        }
        questionPanel.transform.localScale = new Vector3(1, 1, 1);
    }
}
