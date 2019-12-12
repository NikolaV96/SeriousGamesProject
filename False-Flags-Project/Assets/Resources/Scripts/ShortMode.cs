using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortMode : MonoBehaviour
{

    public List<GameObject> Questions;
    public Sprite correctSprite;
    public Sprite wrongSprite;
    public GameObject GameOverPanel;
    public GameObject CorrectGuessedText;
    public GameObject WrongGuessedText;

    private Scores m_Scores;
    private CurrentGameData m_GameData;
    private int QuestionNumber = 0;
    private int maxQuestionsNumbert = 15;

    // Start is called before the first frame update
    void Start()
    {
        maxQuestionsNumbert = Questions.Count;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;

        if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SHORT_MODE)
        {
            this.enabled = true;
            foreach (GameObject o in Questions)
            {
                o.SetActive(true);
            }
        }
        else
            this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool ShouldFinishGame()
    {
        if (QuestionNumber == (maxQuestionsNumbert - 1))
            return true;
        return false;
    }

    public void Rotate(bool IsCorrect)
    {
        if (ShouldFinishGame())
        {
            GameOverPanel.SetActive(true);
            CorrectGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentScore().ToString();
            WrongGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentWrongScore().ToString();
            m_GameData.SetGameOver();
            foreach (GameObject o in Questions)
            {
                o.SetActive(false);
            }
        }
        StartCoroutine(Rotation(IsCorrect));
    }

    IEnumerator Rotation(bool IsCorrect)
    {
        bool assigned = false;
        if (assigned == false)
        {
            if (IsCorrect)
                Questions[QuestionNumber].GetComponent<Image>().sprite = correctSprite;
            else
                Questions[QuestionNumber].GetComponent<Image>().sprite = wrongSprite;
        }
        yield return null;
        QuestionNumber++;
    }
}
