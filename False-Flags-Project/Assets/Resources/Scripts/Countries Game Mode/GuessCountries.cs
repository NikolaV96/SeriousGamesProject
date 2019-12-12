using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuessCountries : MonoBehaviour
{
    public GameObject[] Buttons;
    public GameObject[] FlagHolders;
    public GameObject Button;
    public Sprite[] FlagSprites;
    public Text CorrectScoreText;
    public Text WrongScoreText;
    public GameObject ScorePanel;
    public Sprite CorrectSprite;
    public Sprite WrongSprite;
    public Image currentFlag;
    public Text currentTextFlag;


    int currentIndex;
    int currentMaxIndex;
    [HideInInspector]
    public int correctGuessed;
    [HideInInspector]
    public int wrongGuessed;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        if (SceneManager.GetActiveScene().name == "Europe") { currentMaxIndex = 46; Debug.Log(currentMaxIndex); }    
        if (SceneManager.GetActiveScene().name == "Asia") { currentMaxIndex = 48; Debug.Log(currentMaxIndex); }            
        if (SceneManager.GetActiveScene().name == "NorthAmerica") { currentMaxIndex = 22; Debug.Log(currentMaxIndex); }            
        if (SceneManager.GetActiveScene().name == "SouthAmerica") { currentMaxIndex = 12; Debug.Log(currentMaxIndex); }
        CorrectScoreText.text = correctGuessed.ToString();
        WrongScoreText.text = wrongGuessed.ToString();
        currentFlag.sprite = FlagSprites[currentIndex];
        currentTextFlag.text = FlagSprites[currentIndex].name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                //Debug.Log(hit.collider.name);
                foreach (GameObject flagName in FlagHolders)
                {
                    if (flagName.name == hit.collider.name)
                    {
                        Debug.Log("found " + flagName.name);
                        var newFlag = flagName.GetComponent<SpriteRenderer>();
                        //var newLayer = flagName.GetComponent<SpriteRenderer>().sortingOrder;
                        newFlag.sprite = FlagSprites[currentIndex];
                        //newLayer = hit.collider.GetComponent<SpriteRenderer>().sortingOrder;
                    }
                }

            }
        }
        MouseScroll();
    }

    void MouseScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown("right"))
        {
            currentIndex++;
            if (currentIndex > currentMaxIndex)
                currentIndex = currentMaxIndex;
            Debug.Log(FlagSprites[currentIndex].name);
            currentFlag.sprite = FlagSprites[currentIndex];
            currentTextFlag.text = FlagSprites[currentIndex].name;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown("left"))
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = 0;
            Debug.Log(FlagSprites[currentIndex].name);
            currentFlag.sprite = FlagSprites[currentIndex];
            currentTextFlag.text = FlagSprites[currentIndex].name;
        }
    }

    public void FinishGame ()
    {
        Button.SetActive(false);
        for(int i = 0; i < FlagHolders.Length && i < FlagSprites.Length && i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
            //selected.name = FlagHolders[i].GetComponent<SpriteRenderer>().sprite.name;
            if (FlagSprites[i].name == FlagHolders[i].GetComponent<SpriteRenderer>().sprite.name)
            {
                correctGuessed++;
                CorrectScoreText.text = correctGuessed.ToString();
                FlagHolders[i].GetComponent<SpriteRenderer>().sprite = CorrectSprite;
            }
            else
            {
                wrongGuessed++;
                WrongScoreText.text = wrongGuessed.ToString();
                FlagHolders[i].GetComponent<SpriteRenderer>().sprite = WrongSprite;
            }
        }
        ScorePanel.SetActive(true);
    }

    public void CorrectAnswers()
    {
        for(int i = 0; i < FlagHolders.Length && i < FlagSprites.Length; i++)
        {
            FlagHolders[i].GetComponent<SpriteRenderer>().sprite = FlagSprites[i];
        }
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void MainMenu(string level)
    {
        Application.LoadLevel(level);
    }
}
