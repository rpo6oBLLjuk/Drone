using CustomInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PointsSwitcher
{
    [SerializeField] private Transform pointsContainer;
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
        points = points.Where(point => point != null).ToList();

        if (points.Count == 0)
        {
            points = pointsContainer.GetComponentsInChildren<Point>().ToList();

            if (points.Count == 0)
            {
                Debug.LogWarning("Checkpoint Service не обнаружил чекпоинтов");
                return;
            }
        }

        checkpointService = service;

        foreach (var point in points)
        {
            if (point == null)
                continue;
            point.pointService = service;
            point.SetPointInactive(inactiveColor);
        }

        ActivatePoints(true);
    }

    /// <summary>
    /// Метод подбора чекпоинта
    /// </summary>
    /// <returns>Возвращаемое значение говорит о завершении игры (true - игра продолжается, false - чекпоинты кончились)</returns>
    public bool PointGetted()
    {
        Point firstPoint = points.First();
        firstPoint.SetPointInactive(inactiveColor);
        points.Remove(firstPoint);

        Debug.Log("Собран чек-поинт");
        return ActivatePoints(false);
    }

    private bool ActivatePoints(bool systemCall)
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

            if (systemCall)
                checkpointService.audioService.PlayCheckpointCollected();

            return true;
        }
        else
        {
            if (systemCall)
                checkpointService.audioService.PlayLevelComplete();

            checkpointService.DroneInput.Drone.Disable();

            GameStateController.End(true);

            Debug.Log("Уровень завершён");

            return false;
        }
    }
}
