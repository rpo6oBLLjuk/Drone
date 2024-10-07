using CustomInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PointsSwitcher
{
    [SerializeField] private List<Point> points = new();

    [HorizontalLine("Colors")]
    [ColorUsage(true, true)]
    [SerializeField] private Color activeColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color preActiveColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color inactiveColor;

    private CheckpointService checkpointService;

    public void Start(CheckpointService service)
    {
        checkpointService = service;

        foreach (var point in points)
        {
            point.pointService = service;
            point.SetPointInactive(inactiveColor);
        }

        ActivatePoints(false);
    }

    public void PointGetted()
    {
        Point firstPoint = points.First();

        firstPoint.SetPointInactive(inactiveColor);
        points.Remove(firstPoint);

        Debug.Log("Собран чек-поинт");

        ActivatePoints(true);
    }

    private void ActivatePoints(bool enableAudio)
    {
        if (points.Count > 0)
        {
            if (points.First() != points.Last())
            {
                points[0].SetPointActive(activeColor);
                points[1].SetPointPreActive(preActiveColor);

                Debug.Log("Активирован следующий чек-поинт");
            }
            else
            {
                points[0].SetPointActive(activeColor);
                Debug.Log("Активирован последний чек-поинт");
            }

            if (enableAudio)
                checkpointService.audioService.PlayCheckpointCollected();
        }
        else
        {
            if (enableAudio)
                checkpointService.audioService.PlayLevelComplete();

            checkpointService.DroneInput.Drone.Disable();

            GameStateController.End(true);

            Debug.Log("Уровень завершён");
        }
    }
}
