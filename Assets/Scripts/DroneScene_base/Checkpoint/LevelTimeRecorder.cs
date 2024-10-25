using CustomInspector;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LevelTimeRecorder
{
    [SerializeField] private int level;
    [SerializeField, ReadOnly] private List<float> checkpointsTime = new();

    [SerializeField] private bool useStarSystem;
    [SerializeField, ShowIf(nameof(useStarSystem)), Unit("s")] private float threeStarsTime;
    [SerializeField, ShowIf(nameof(useStarSystem)), Unit("s")] private float twoStarsTime;
    [SerializeField, ShowIf(nameof(useStarSystem)), Unit("s")] private float oneStarTime;

    [SerializeField, ReadOnly] private float lastPointTime;

    private CheckpointService _checkpointService;

    public void StartRecord(CheckpointService service)
    {
        _checkpointService = service;

        lastPointTime = Time.time;
        Debug.Log("Старт записи");

        GameStateController.GameEnd += EndRecord;
    }

    public void PointGetted()
    {
        if (lastPointTime == 0)
        {
            Debug.LogWarning("Checkpoint getted where LevelTimeRecorder not start");
            return;
        }

        checkpointsTime.Add(Time.time - lastPointTime);
        lastPointTime = Time.time;
        _checkpointService.uiService.AddTime(checkpointsTime.Last(), checkpointsTime.Count);
    }

    private void EndRecord(bool value)
    {
        PointGetted();

        if (value)
        {
            float currentTime = checkpointsTime.Sum();
            float lastSaveTime = SaveService.LoadLevelTime(level);

            Debug.Log($"CheckpointsTime.Sum: {currentTime}, LastSave: {lastSaveTime}");

            if (lastSaveTime > currentTime)
                DBConnect.SaveLevelDataAsync(level, currentTime).Forget();
        }

        GameStateController.GameEnd -= EndRecord;
    }
}
