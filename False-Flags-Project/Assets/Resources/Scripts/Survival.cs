using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Survival : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject mySlider;
    public GameObject CorrectGuessedText;
    public GameObject WrongGuessedText;
    public Slider Slider;
    private int lifesLeft = 3;
    private CurrentGameData m_GameData;
    private Scores m_Scores;

    // Start is called before the first frame update
    void Start()
    {
        if(GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SURVIVAL_MODE)
        {
            this.enabled = true;
            mySlider.SetActive(true);
            Slider.value = lifesLeft;
        }
        else
        {
            this.enabled = false;
        }
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoveLife()
    {
        if(lifesLeft > 0)
        {
            lifesLeft--;
            Slider.value = lifesLeft;
        }
        if(lifesLeft == 0)
        {
            CorrectGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentScore().ToString();
            WrongGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentWrongScore().ToString();
            GameOverPanel.SetActive(true);
            m_GameData.SetGameOver();
        }
    }
}
