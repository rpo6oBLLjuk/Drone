using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimeLoader : MonoBehaviour
{
    [SerializeField] private LoadingWidget loadingWidget;

    [SerializeField] private string baseTimeString = "Time: ";
    [SerializeField] private List<TextMeshProUGUI> levelCount;
    [SerializeField] private List<float> levelTime;


    private void Awake()
    {
        LoadingTime();
        SetTimes();
    }

    private void LoadingTime()
    {
        levelTime = SaveService.LoadAllLevelTime();
    }

    private void SetTimes()
    {
        for (int i = 0; i < levelCount.Count; i++)
        {
            TimeSpan time = TimeSpan.FromSeconds(levelTime[i]);
            string formattedTime = $"{(int)time.TotalMinutes}:{time.Seconds:D2}:{time.Milliseconds:D3}";
            levelCount[i].text = baseTimeString + formattedTime;
        }
    }
}
