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

    public void SetPointInactive(Color color)
    {
        SetColor(color);
        active = false;
    }

    private void SetColor(Color color)
    {
        meshRenderer.material.color = color;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            if (Physics.OverlapBox(transform.position, size, transform.rotation).Count() != 0)
                pointService.PointGetted();

        }
    }

    private void OnDrawGizmos()
    {
        // ��������� ������ �������
        Matrix4x4 oldMatrix = Gizmos.matrix;

        // ������������� ����� ������� � ���������, �������� � ��������� �������
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        // ������ ��������� ���
        Gizmos.DrawWireCube(Vector3.zero, size);

        // ��������������� ������ �������
        Gizmos.matrix = oldMatrix;
    }
}
