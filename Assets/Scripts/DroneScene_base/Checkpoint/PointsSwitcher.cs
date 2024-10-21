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
                Debug.LogWarning("Checkpoint Service �� ��������� ����������");
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

        ActivatePoints(false);
    }

    public void PointGetted()
    {
        Debug.LogWarning($"{GameStateController.GameEnded}");

        Point firstPoint = points.First();

        firstPoint.SetPointInactive(inactiveColor);
        points.Remove(firstPoint);

        Debug.Log("������ ���-�����");

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

                Debug.Log("����������� ��������� ���-�����");
            }
            else
            {
                points[0].SetPointActive(activeColor);
                Debug.Log("����������� ��������� ���-�����");
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

            Debug.Log("������� ��������");
        }
    }
}
