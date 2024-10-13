using System.IO;
using UnityEngine;

public static class LevelSaverService
{
    private static string basePath = "Saves/LevelData/";

    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public float time;
    }

    // ����� ��� ���������� ������� ������
    public static void SaveLevelTime(string levelName, float time)
    {
        // ������ ����������, ���� ��� �� ����������
        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }

        LevelData levelData = new LevelData
        {
            levelName = levelName,
            time = time
        };

        string json = JsonUtility.ToJson(levelData);

        string filePath = Path.Combine(basePath, $"{levelName}.json");

        File.WriteAllText(filePath, json);
    }

    public static float LoadLevelTime(string levelName)
    {
        string filePath = Path.Combine(basePath, $"{levelName}.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            return levelData.time;
        }

        return 0f;
    }
}