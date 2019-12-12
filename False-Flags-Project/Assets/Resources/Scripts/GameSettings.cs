using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private Dictionary<EContinentType, string> _SceneName = new Dictionary<EContinentType, string>();

    public enum EGameMode
    {
        NOTE_SET,
        TIME_TRAIL_MODE,
        SURVIVAL_MODE,
        SHORT_MODE,
    }

    public enum EContinentType
    {
        E_NOT_SET = 0,
        E_EUROPE,
        E_ASIA,
        E_AFRICA,
        E_NORTH_AMERICA,
        E_SOUTH_AMERICA,
        E_OCEANIA,
    };

    private EGameMode _GameMode;
    private EContinentType _Continent;
    private string _ContinentName;

    public static GameSettings Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
    }

    void Start()
    {
        SetSceneNameAndType();
        _GameMode = EGameMode.NOTE_SET;
        _Continent = EContinentType.E_NOT_SET;
    }

    private void SetSceneNameAndType()
    {
        _SceneName.Add(EContinentType.E_EUROPE, "Game");
        _SceneName.Add(EContinentType.E_ASIA, "Game");
        _SceneName.Add(EContinentType.E_AFRICA, "Game");
        _SceneName.Add(EContinentType.E_NORTH_AMERICA, "Game");
        _SceneName.Add(EContinentType.E_SOUTH_AMERICA, "Game");
        _SceneName.Add(EContinentType.E_OCEANIA, "Game");
    }

    public string GetContinentSceneName()
    {
        string name;
        if(_SceneName.TryGetValue(_Continent, out name))
        {
            return name;
        }
        else
        {
            Debug.Log("Continent not found");
            return ("Continent not found");
        }
    }

    public void SetContinentType(EContinentType type)
    {
        _Continent = type;
    }

    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }

    public EGameMode GetGameMode()
    {
        return _GameMode;
    }

    public EContinentType GetContinentType()
    {
        return _Continent;
    }

    public void SetContinentName(string name)
    {
        SetContinentType(GetContinentTypeFromString(name));
        _ContinentName = name;
    }

    private EContinentType GetContinentTypeFromString(string type)
    {
        switch(type)
        {
            case "EUROPE": return EContinentType.E_EUROPE;
            case "ASIA": return EContinentType.E_ASIA;
            case "AFRICA": return EContinentType.E_AFRICA;
            case "NORTHAMERICA": return EContinentType.E_NORTH_AMERICA;
            case "SOUTHAMERICA": return EContinentType.E_SOUTH_AMERICA;
            case "OCEANIA": return EContinentType.E_OCEANIA;
            default: return EContinentType.E_NOT_SET;
        }
    }
}
