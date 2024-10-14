using CustomInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ModelsTestSceneController : MonoBehaviour
{
    [HorizontalLine("Models replacer")]
    [SerializeField] private Vector3 offsetVector;
    [SerializeField] private List<GameObject> modelContainers;

    [SerializeField] private bool drawGizmos;
    [SerializeField, ShowIf(nameof(drawGizmos))] private Color gizmosColor;

    [SerializeField] private Bounds[] renderersBounds;
    [SerializeField] private List<Bounds[]> forGizmosDraw = new();

    [Button(nameof(CalculateVertexCount))]
    [SerializeField] private bool showMeshStatistic;
    [SerializeField] private bool showCollisionStatistic;

    private void ReplaceModels()
    {
        forGizmosDraw.Clear();

        foreach (var model in modelContainers)
        {
            var childTransforms = model.GetComponentsInChildren<Transform>(true)
                .Where(t => t.parent != null && t.parent.parent == model.transform && t.gameObject.activeInHierarchy)
                .ToList();

            renderersBounds = new Bounds[childTransforms.Count];
            forGizmosDraw.Add(renderersBounds);

            Vector3 currentOffset = Vector3.zero;

            for (int i = 0; i < childTransforms.Count; i++)
            {
                var childTransform = childTransforms[i];

                // Собираем все MeshRenderer внутри текущего дочернего объекта
                MeshRenderer[] childRenderers = childTransform.GetComponentsInChildren<MeshRenderer>(true);

                if (childRenderers.Length > 0)
                {
                    // Начальное значение комбинированного Bounds — это Bounds первого MeshRenderer
                    Bounds combinedBounds = childRenderers[0].bounds;

                    // Объединяем все Bounds в один для текущего объекта
                    foreach (var renderer in childRenderers)
                    {
                        combinedBounds.Encapsulate(renderer.bounds);
                    }

                    // Сохраняем комбинированные Bounds
                    renderersBounds[i] = combinedBounds;

                    // Вычисляем смещение для установки новой позиции
                    float xSign = Mathf.Sign(offsetVector.x);
                    float zSign = Mathf.Sign(offsetVector.z);

                    if (i > 0)
                    {
                        // Смещаем позиции только по X и Z
                        if (offsetVector.x != 0)
                        {
                            currentOffset.x += (renderersBounds[i - 1].extents.x + combinedBounds.extents.x + Mathf.Abs(offsetVector.x)) * xSign;
                        }

                        if (offsetVector.z != 0)
                        {
                            currentOffset.z += (renderersBounds[i - 1].extents.z + combinedBounds.extents.z + Mathf.Abs(offsetVector.z)) * zSign;
                        }
                    }

                    Vector3 centerOffset = new Vector3(
                        (childTransform.position.x - combinedBounds.center.x),
                        0,
                        (childTransform.position.z - combinedBounds.center.z)
                    );

                    // Устанавливаем позицию с учетом смещения
                    childTransform.position = model.transform.position + currentOffset + centerOffset;
                }
            }
        }
    }

    private void CalculateVertexCount()
    {

        if (showMeshStatistic)
        {
            int totalVertices = 0;
            MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();

            foreach (MeshFilter meshFilter in meshFilters)
            {
                if (meshFilter.sharedMesh != null)
                {
                    totalVertices += meshFilter.sharedMesh.vertexCount;
                }
            }
            Debug.Log($"<color=Blue>Mesh vertices</color>: {totalVertices:N0}\n");
        }

        if (showCollisionStatistic)
        {
            string debugString = string.Empty;

            int totalCollisionVertices = 0;
            int totalCollisionPolygons = 0;

            MeshCollider[] meshColliders = FindObjectsOfType<MeshCollider>();

            foreach (MeshCollider meshCollider in meshColliders)
            {
                if (meshCollider.sharedMesh != null)
                {
                    totalCollisionVertices += meshCollider.sharedMesh.vertexCount;
                    totalCollisionPolygons += meshCollider.sharedMesh.triangles.Length / 3;
                }
            }
            debugString += $"<color=green>Collision vertices</color>: {totalCollisionVertices:N0}\n";
            debugString += $"[{System.DateTime.Now:HH:mm:ss}] <color=yellow>Collision polygons</color>: {totalCollisionPolygons:N0}";

            Debug.Log(debugString);
        }
    }

    private void OnDrawGizmos()
    {
        ReplaceModels();

        if (drawGizmos && forGizmosDraw != null && forGizmosDraw.Count > 0)
        {
            Gizmos.color = gizmosColor;

            foreach (Bounds[] boundsMassif in forGizmosDraw)
            {
                foreach (Bounds bounds in boundsMassif)
                {
                    Gizmos.DrawWireCube(bounds.center, bounds.size);
                    Gizmos.DrawWireSphere(bounds.center, 0.1f);
                }
            }
        }
    }
}
