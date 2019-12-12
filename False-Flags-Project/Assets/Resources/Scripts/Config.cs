using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Linq;

public class Config : MonoBehaviour
{
    static string dir = Directory.GetCurrentDirectory();
    static string file = @"/flags.ini";
    static string path = dir + file;

    private static bool DebugMode = true;

    private static int numberOfScoresRecord;
    public static List<int> ScoresList;

    private static List<int> ContinentScores = new int[6].ToList();
    private static List<int> ContinentLengths;
    private const int NumberOfContinents = 6;

    public static int GetEuropeScores() { return ContinentScores[0]; }
    public static int GetAsiaScores() { return ContinentScores[1]; }
    public static int GetAfricaScores() { return ContinentScores[2]; }
    public static int GetNorthAmericaScores() { return ContinentScores[3]; }
    public static int GetSouthAmericaScores() { return ContinentScores[4]; }
    public static int GetOceaniaScores() { return ContinentScores[5]; }


    public static void CreateScoreFile()
    {
        ContinentScores.ForEach(item => { item = 0; });
        ContinentLengths = new int[NumberOfContinents]
        {
            GameData.Instance.EuropeCountryDataSet.Length,
            GameData.Instance.AsiaCountryDataSet.Length,
            GameData.Instance.AfricaCountryDataSet.Length,
            GameData.Instance.NorthAmericaCountryDataSet.Length,
            GameData.Instance.SouthAmericaCountryDataSet.Length,
            GameData.Instance.OceaniaCountryDataSet.Length,
        }.ToList();

        foreach(var continent_length in ContinentLengths)
        {
            numberOfScoresRecord += continent_length;
        }

        numberOfScoresRecord += NumberOfContinents;
        ScoresList = new int[numberOfScoresRecord].ToList();

        if(File.Exists(path) == false)
        {
            SaveScoreList();
        }
        UpdateScoreList();
    }

    public static void SaveScoreList()
    {
        int current_continent = 0;
        int flag_index = 0;

        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);

        for(int i = 0; i < numberOfScoresRecord; i++)
        {
            if ((i == 0) && ((current_continent <= NumberOfContinents) && (flag_index == ContinentLengths[current_continent])))
            {
                if (DebugMode)
                {
                    string DebugContinentName = "";

                    if (i == 0)
                        DebugContinentName = "//EUROPE";
                    else
                    {
                        switch (current_continent)
                        {
                            case 0: DebugContinentName = "//ASIA"; break;
                            case 1: DebugContinentName = "//AFRICA"; break;
                            case 2: DebugContinentName = "//NORHTAMERICA"; break;
                            case 3: DebugContinentName = "//SOUTHAMERICA"; break;
                            case 4: DebugContinentName = "//OCEANIA"; break;
                        }
                    }

                    writer.WriteLine("#. " + current_continent.ToString() + DebugContinentName);
                }
                else
                    writer.WriteLine("#. " + current_continent.ToString());
                if (i > 0)
                    current_continent++;
                flag_index = 0;
            }
            else
            {
                if (DebugMode)
                {
                    string DebugCountryName = "";
                    switch (current_continent)
                    {
                        case 0: DebugCountryName = GameData.Instance.EuropeCountryDataSet[flag_index].Name; break;
                        case 1: DebugCountryName = GameData.Instance.AsiaCountryDataSet[flag_index].Name; break;
                        case 2: DebugCountryName = GameData.Instance.AfricaCountryDataSet[flag_index].Name; break;
                        case 3: DebugCountryName = GameData.Instance.NorthAmericaCountryDataSet[flag_index].Name; break;
                        case 4: DebugCountryName = GameData.Instance.SouthAmericaCountryDataSet[flag_index].Name; break;
                        case 5: DebugCountryName = GameData.Instance.OceaniaCountryDataSet[flag_index].Name; break;
                    }
                    writer.WriteLine(i.ToString() + "." + ScoresList[i].ToString() + "D" + "       //" + DebugCountryName);
                }
                else
                    writer.WriteLine(i.ToString() + "." + ScoresList[i].ToString() + "D");

                flag_index++;
            }
        }

        writer.Close();
    }

    public static void UpdateScoreList()
    {
        StreamReader file = new StreamReader(path);
        string line;
        while((line = file.ReadLine()) != null)
        {
            for(int i = 0; i < numberOfScoresRecord; i++)
            {
                if (line != "#")
                {
                    string[] line_part = line.Split('.');
                    if (line_part[0] == i.ToString())
                    {
                        string[] part_substring = Regex.Split(line_part[1], "D");
                        int score;
                        if (int.TryParse(part_substring[0], out score))
                            ScoresList[i] = score;
                        else
                            ScoresList[i] = 0;
                    }
                }
                else
                    ScoresList[i] = 4;
            }
        }

        file.Close();
        UpdateContinentScores();
    }

    private static void UpdateContinentScores()
    {
        ContinentScores.ForEach(item => { item = 0; });
        int SearchingContinent = 0;
        int lastPosition = FindLastPositionInContinent(SearchingContinent);

        for(int i = 0; i < numberOfScoresRecord; i++)
        {
            if (i <= lastPosition && (ScoresList[i] == 1))
                ContinentScores[SearchingContinent]++;
            else if(i > lastPosition)
            {
                if (SearchingContinent < 5)
                    SearchingContinent++;

                lastPosition = FindLastPositionInContinent(SearchingContinent);
                lastPosition += SearchingContinent;
            }
        }
    }

    private static int FindLastPositionInContinent(int continent_index)
    {
        int position = 0;
        for(int i = 0; i < continent_index; i++)
        {
            position += ContinentLengths[i];
        }
        return position;
    }

    public static void SaveScore(int FlagIndex, bool Correct, int ContinentIndex)
    {
        int FirstPosition = FindPositionOfFirstFlagInContinent();

        if(ContinentIndex == 1)
        {
            if (Correct && (ScoresList[FirstPosition + FlagIndex] == 0))
                ScoresList[FirstPosition + FlagIndex] = 1;
        }
        else
        {
            if (Correct && (ScoresList[FirstPosition + (FlagIndex + ContinentIndex)] == 0))
                ScoresList[FirstPosition + (FlagIndex + ContinentIndex)] = 1;
        }
    }

    private static int FindPositionOfFirstFlagInContinent()
    {
        int ContinentIndex = (int)GameSettings.Instance.GetContinentType() - 1;
        int position = 0;

        for(int i = 0; i < ContinentIndex; i++)
        {
            position += ContinentLengths[i];
        }
        if (ContinentIndex == 0)
            position += 1;

        return position;
    }

    public static void ResetGameProgress()
    {
        ScoresList.ForEach(item => { item = 0; });
        SaveScoreList();
        UpdateScoreList();
    }
}
