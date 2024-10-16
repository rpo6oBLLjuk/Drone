using System;
using System.IO;
using UnityEngine;

public static class SaveService
{
    private static readonly string baseLevelPath = Path.Combine(Application.persistentDataPath, "Saves/LevelData/");
    private static readonly string baseUserPath = Path.Combine(Application.persistentDataPath, "Saves/UserData/");


    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public float time;
    }

    [System.Serializable]
    public class UserData
    {
        public int user_id;
        public string login;

        public UserData(int user_id, string login)
        {
            this.user_id = user_id;
            this.login = login;
        }
    }


    public static void SaveLocalUser(int user_id, string login)
    {
        try
        {
            if (!Directory.Exists(baseUserPath))
            {
                Directory.CreateDirectory(baseUserPath);
            }

            UserData userData = new(user_id, login);

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


    public static void SaveLevelTime(string levelName, float time)
    {
        if (!Directory.Exists(baseLevelPath))
        {
            Directory.CreateDirectory(baseLevelPath);
        }

        LevelData levelData = new LevelData
        {
            levelName = levelName,
            time = time
        };

        string json = JsonUtility.ToJson(levelData);

        string filePath = Path.Combine(baseLevelPath, $"{levelName}.json");

        File.WriteAllText(filePath, json);
    }

    public static float LoadLevelTime(string levelName)
    {
        string filePath = Path.Combine(baseLevelPath, $"{levelName}.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            return levelData.time;
        }

        return 0f;
    }
}