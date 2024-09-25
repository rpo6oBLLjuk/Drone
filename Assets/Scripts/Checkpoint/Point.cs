using CustomInspector;
using System.Linq;
using UnityEngine;

public class Point : MonoBehaviour
{
    [HideInInspector] public CheckpointService pointService;
    [SerializeField, SelfFill] private MeshRenderer meshRenderer;
    [SerializeField] private Vector3 size;

    bool active = false;

    public void SetPointActive(Color color)
    {
        SetColor(color);
        active = true;
    }

    public void SetPointPreActive(Color color)
    {
        SetColor(color);
    }

    public void SetPointInactive()
    {
        SetColor(Color.black);
        active = false;
    }

    public void SetColor(Color color)
    {
        meshRenderer.material.color = color;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            if (Physics.OverlapBox(transform.position, size).Count() != 0)
                pointService.PointGetted();

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }
}
