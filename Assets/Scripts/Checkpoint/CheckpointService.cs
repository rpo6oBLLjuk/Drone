using CustomInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CheckpointService : MonoBehaviour
{
    [Inject] readonly AudioService audioService;

    [SerializeField] private List<Point> points = new();

    [HorizontalLine("Colors")]
    [ColorUsage(true, true)]
    [SerializeField] private Color activeColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color preActiveColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color inactiveColor;

    private void Start()
    {
        foreach (var point in points)
        {
            point.pointService = this;
            point.SetPointInactive(inactiveColor);
        }

        ActivatePoints(false);
    }

    public void PointGetted()
    {
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
                audioService.PlayCheckpointCollected();
        }
        else
        {
            if (enableAudio)
                audioService.PlayLevelComplete();

            Debug.Log("������� ��������");
        }
    }
}
