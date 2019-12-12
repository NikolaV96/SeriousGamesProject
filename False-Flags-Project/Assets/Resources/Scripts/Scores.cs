using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text ScoreText;
    private CurrentGameData m_GameData;
    private int m_FlagNumber;
    private int m_Scores;
    private int m_WrongScores;
    private bool Initialized = false;
    // Start is called before the first frame update
    void Start()
    {
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = 0;
        m_WrongScores = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Initialized)
        {
            if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SHORT_MODE)
                m_FlagNumber = 15;
            else
                m_FlagNumber = m_GameData.GetFlagNumber();
            DisplayScores();
            Initialized = true;
        }
    }

    public int GetCurrentScore() { return m_Scores; }
    public int GetCurrentWrongScore() { return m_WrongScores; }
    public int GetQuestionNumber() { return m_FlagNumber; }

    public void AddScores()
    {
        if(m_Scores < m_FlagNumber)
            m_Scores += 1;
        DisplayScores();
    }

    public void RemoveScores()
    {
        if (m_Scores > 0)
            m_Scores -= 1;
        DisplayScores();
    }
    public void AddWrongScores()
    {
        if (m_WrongScores < m_FlagNumber)
            m_WrongScores += 1;
    }

    void DisplayScores()
    {
        string DisplaySting = "Scores: " + m_Scores + "/" + m_FlagNumber;
        ScoreText.text = DisplaySting;
    }
}
