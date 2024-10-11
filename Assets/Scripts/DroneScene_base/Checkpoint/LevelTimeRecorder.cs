using CustomInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LevelTimeRecorder
{
    [SerializeField, ReadOnly] private List<float> checkpointsTime = new();

    [SerializeField] private bool useStarSystem;
    [SerializeField, ShowIf(nameof(useStarSystem))] private float threeStarsTime;
    [SerializeField, ShowIf(nameof(useStarSystem))] private float twoStarsTime;
    [SerializeField, ShowIf(nameof(useStarSystem))] private float oneStarTime;

    [SerializeField, ReadOnly] private float lastPointTime;

    private CheckpointService _checkpointService;


    public void Start(CheckpointService service)
    {
        _checkpointService = service;

        lastPointTime = Time.time;
        Debug.Log("Старт записи");
    }

    public void PointGetted()
    {
        checkpointsTime.Add(Time.time - lastPointTime);
        lastPointTime = Time.time;
        _checkpointService.uiService.AddTime(checkpointsTime.Last(), checkpointsTime.Count);
    }

    public void EndRecord()
    {
        checkpointsTime.Add(Time.time - lastPointTime);
    }
}
