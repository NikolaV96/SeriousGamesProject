using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonScriprt : MonoBehaviour
{
    Animator myAnim;
    bool soundPlayed;

    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();

        soundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Btn_Hightlight"))
        {
            if (!soundPlayed)
                FindObjectOfType<AudioManager>().Play("Button_Hover");

            soundPlayed = true;
        }
        else if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Btn_Idle"))
        {
            soundPlayed = false;
        }
    }
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }
    public void LoadLevelandClearData(string level)
    {
        Application.LoadLevel(level);
        CurrentGameData.AllowDestroyOnLoad = true;
    }
}
