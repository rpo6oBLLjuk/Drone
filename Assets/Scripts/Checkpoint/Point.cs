using CustomInspector;
using UnityEngine;

public class Point : MonoBehaviour
{
    [HideInInspector] public CheckpointService pointService;
    [SerializeField, SelfFill(true)] private MeshRenderer meshRenderer;

    bool isActive = false;

    public void SetPointActive(Color color)
    {
        SetColor(color);
        isActive = true;
    }

    public void SetPointPreActive(Color color)
    {
        SetColor(color);
    }

    public void SetPointInactive(Color color)
    {
        SetColor(color);
        isActive = false;
    }

    private void SetColor(Color color)
    {
        meshRenderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            pointService.PointGetted();
        }
    }
}
