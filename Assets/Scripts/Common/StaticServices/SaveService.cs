using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveService
{
    private static readonly string baseLevelPath = Path.Combine(Application.persistentDataPath, "Saves/LevelData/");
    private static readonly string baseUserPath = Path.Combine(Application.persistentDataPath, "Saves/UserData/");


    [Serializable]
    public class LevelsData
    {
        public List<float> times;

        public LevelsData(List<float> times)
        {
            this.times = times;
        }
    }

    [Serializable]
    public class UserData
    {
        public int user_id;
        public string login;
        public string password;

        public UserData(int user_id, string login, string password)
        {
            this.user_id = user_id;
            this.login = login;
            this.password = password;
        }
    }


    public static void SaveLocalUser(int user_id, string login, string password)
    {
        try
        {
            if (!Directory.Exists(baseUserPath))
            {
                Directory.CreateDirectory(baseUserPath);
            }

            UserData userData = new(user_id, login, password);

            string filePath = Path.Combine(baseUserPath, $"userdata.json");
            string json = JsonUtility.ToJson(userData);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public static UserData LoadLocalUser()
    {
        string filePath = Path.Combine(baseUserPath, "userdata.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            UserData userData = JsonUtility.FromJson<UserData>(json);

            return userData;
        }

        return null;
    }


    public static void SaveLevelTime(int level, float time)
    {
        string levelName = $"Level{level}";

        if (!Directory.Exists(baseLevelPath))
        {
            Directory.CreateDirectory(baseLevelPath);
        }

        LevelsData levelData = new(LoadAllLevelTime());
        levelData.times[level - 1] = time;

        string json = JsonUtility.ToJson(levelData);

        string filePath = Path.Combine(baseLevelPath, $"LevelsData.json");

        File.WriteAllText(filePath, json);

        Debug.Log($"Level {level} was save with time: {time}");
    }

    public static float LoadLevelTime(int level)
    {
        string filePath = Path.Combine(baseLevelPath, $"LevelsData.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LevelsData levelData = JsonUtility.FromJson<LevelsData>(json);

            return levelData.times[level - 1];
        }

        return 0f;
    }

    public static void SaveAllLevelTime(List<float> times)
    {
        string filePath = Path.Combine(baseLevelPath, $"LevelsData.json");

        if (!Directory.Exists(baseLevelPath))
        {
            Directory.CreateDirectory(baseLevelPath);
        }

        LevelsData levelData = new(times);

        string json = JsonUtility.ToJson(levelData);

        File.WriteAllText(filePath, json);
    }

    public static List<float> LoadAllLevelTime()
    {
        List<float> returnedValue = new();
        string filePath = Path.Combine(baseLevelPath, $"LevelsData.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LevelsData levelData = JsonUtility.FromJson<LevelsData>(json);

            return levelData.times;
        }

        return returnedValue;
    }
}