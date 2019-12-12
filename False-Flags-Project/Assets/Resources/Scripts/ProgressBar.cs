using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextIndicator;

    public GameSettings.EContinentType ContinentType;

    private float TargetAmount;
    private float CurrentAmount;
    private float Speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        CurrentAmount = 0.0f;
        TextIndicator.GetComponent<Text>().text = (0).ToString() + "%";
        switch (ContinentType)
        {
            case GameSettings.EContinentType.E_EUROPE:
                {
                    float currentFlagsPrc = ((int)Config.GetEuropeScores() / (float)GameData.Instance.EuropeCountryDataSet.Length);
                    TargetAmount = (float)currentFlagsPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_AFRICA:
                {
                    float currentFlagsPrc = ((int)Config.GetAfricaScores() / (float)GameData.Instance.AfricaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagsPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_ASIA:
                {
                    float currentFlagsPrc = ((int)Config.GetAsiaScores() / (float)GameData.Instance.AsiaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagsPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_NORTH_AMERICA:
                {
                    float currentFlagsPrc = ((int)Config.GetNorthAmericaScores() / (float)GameData.Instance.NorthAmericaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagsPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_SOUTH_AMERICA:
                {
                    float currentFlagsPrc = ((int)Config.GetSouthAmericaScores() / (float)GameData.Instance.SouthAmericaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagsPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_OCEANIA:
                {
                    float currentFlagsPrc = ((int)Config.GetOceaniaScores() / (float)GameData.Instance.OceaniaCountryDataSet.Length);
                    TargetAmount = (float)currentFlagsPrc * 100.0f;
                }
                break;
            case GameSettings.EContinentType.E_NOT_SET:
                {
                    TargetAmount = 0.0f;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentAmount < TargetAmount)
        {
            CurrentAmount += Speed * Time.deltaTime;
            TextIndicator.GetComponent<Text>().text = (((int)CurrentAmount).ToString() + "%");
            LoadingBar.GetComponent<Image>().fillAmount = (float)CurrentAmount / 100.0f;
        }
    }
}
