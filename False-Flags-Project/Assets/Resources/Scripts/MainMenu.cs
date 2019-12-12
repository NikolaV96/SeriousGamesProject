using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionsPanel;
    public GameObject mainMenuPanel;
    public GameObject selectionPanel;
    public GameObject gameModePanel;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
        FindObjectOfType<AudioManager>().Play("Button_Pressed");
    }

    public void StartTimeTrial()
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.TIME_TRAIL_MODE);
        LoadScene(GameSettings.Instance.GetContinentSceneName());
    }

    public void StartSurvivalMode()
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.SURVIVAL_MODE);
        LoadScene(GameSettings.Instance.GetContinentSceneName());
    }

    public void ShortGameMode()
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.SHORT_MODE);
        LoadScene(GameSettings.Instance.GetContinentSceneName());
    }

    public void PlayBtnFunction()
    {
        SelectionPanel();
        FindObjectOfType<AudioManager>().Play("Button_Pressed");
    }

    public void OptionsBtnFuntion()
    {
        OptionsPanel();
        FindObjectOfType<AudioManager>().Play("Button_Pressed");
    }

    public void GameModeFunction()
    {
        GameModePanel();
        FindObjectOfType<AudioManager>().Play("Button_Pressed");
    }
    public void CreditsBtnFuntion()
    {
        Application.LoadLevel("Credits");
        FindObjectOfType<AudioManager>().Play("Button_Pressed");
    }

    public void QuitGameBtnFunction()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetQualitySettings(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void OptionsPanel()
    {
        if (mainMenuPanel.activeSelf)
        {
            mainMenuPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }
        else if(!mainMenuPanel.activeSelf)
        {
            mainMenuPanel.SetActive(true);
            optionsPanel.SetActive(false);
        }
    }
    private void SelectionPanel()
    {
        if (mainMenuPanel.activeSelf)
        {
            mainMenuPanel.SetActive(false);
            selectionPanel.SetActive(true);
        }
        else if (!mainMenuPanel.activeSelf)
        {
            mainMenuPanel.SetActive(true);
            selectionPanel.SetActive(false);
        }
    }
    private void GameModePanel()
    {
        if (selectionPanel.activeSelf)
        {
            selectionPanel.SetActive(false);
            gameModePanel.SetActive(true);
        }
        else if (!selectionPanel.activeSelf)
        {
            selectionPanel.SetActive(true);
            gameModePanel.SetActive(false);
        }
    }
}
