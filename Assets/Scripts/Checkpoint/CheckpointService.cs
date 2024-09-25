using CustomInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointService : MonoBehaviour
{
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
            point.SetColor(inactiveColor);
        }

        ActivatePoints();
    }

    public void PointGetted()
    {
        Point firstPoint = points.First();

        firstPoint.SetPointInactive();
        points.Remove(firstPoint);

        Debug.Log("Собран чек-поинт");

        ActivatePoints();
    }

    public void ActivatePoints()
    {
        if (points.Count > 0)
        {
            if (points.First() != points.Last())
            {
                points[0].SetPointActive(activeColor);
                points[1].SetPointPreActive(preActiveColor);
            }
            else
            {
                points[0].SetPointActive(activeColor);
                Debug.Log("Активирован последний чек-поинт");
            }
        }
        else
        {
            Debug.Log("Уровень завершён"); //заглушка
        }
    }
}
