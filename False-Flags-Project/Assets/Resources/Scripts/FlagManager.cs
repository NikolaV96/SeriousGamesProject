using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : MonoBehaviour
{

    public GameObject[] FlagObjects;
    public Text DisplayedText;
    private int NumberOfFlagsObjects = 0;

    private CurrentGameData m_GameData;

    private bool IsFirstRun = false;
    // Start is called before the first frame update
    void Start()
    {
        NumberOfFlagsObjects = FlagObjects.Length;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>();
        IsFirstRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsFirstRun) FirstRun();
    }

    void FirstRun()
    {
        AssignFlags();
        UpdateDisplayedText();
        IsFirstRun = false;
    }

    public void LoadNextGame()
    {
        m_GameData.GetNewCountries();
        AssignFlags();
        UpdateDisplayedText();
    }

    public void AssignFlags()
    {
        int FinalFlagPosition = (int)Random.Range(0, NumberOfFlagsObjects);

        switch (FinalFlagPosition)
        {
            case 0:
                FlagObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());
                FlagObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());

                FlagObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                FlagObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                break;
            case 1:
                FlagObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());
                FlagObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());

                FlagObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                FlagObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                break;
            case 2:
                FlagObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFirstFlagIndex());
                FlagObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetSecondFlagIndex());
                FlagObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetFlagSpriteIndex(m_GameData.GetFinalFlagIndex());

                FlagObjects[0].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFirstFlagIndex());
                FlagObjects[1].GetComponent<Flag>().SetFlagIndex(m_GameData.GetSecondFlagIndex());
                FlagObjects[2].GetComponent<Flag>().SetFlagIndex(m_GameData.GetFinalFlagIndex());
                break;
        }
    }

    void UpdateDisplayedText()
    {
        DisplayedText.text = m_GameData.GetFlagName(m_GameData.GetFinalFlagIndex());
    }
}
