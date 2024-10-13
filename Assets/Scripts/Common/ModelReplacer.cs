using CustomInspector;
using System.Collections.Generic;
using UnityEngine;

public class ModelReplacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> modelContainers;
    [SerializeField, Hook(nameof(ReplaceModels))] private Vector3 offsetVector;

    [SerializeField] private bool drawGizmos;
    [SerializeField, ShowIf(nameof(drawGizmos))] private Color gizmosColor;

    private MeshRenderer[] renderers;

    private void ReplaceModels()
    {
        foreach (var model in modelContainers)
        {
            renderers = model.GetComponentsInChildren<MeshRenderer>(true);
            Vector3 currentOffset = Vector3.zero;

            for (int i = 0; i < renderers.Length; i++)
            {
                MeshRenderer renderer = renderers[i];
                Bounds bounds = renderer.bounds;

                float xSign = Mathf.Sign(offsetVector.x);
                float zSign = Mathf.Sign(offsetVector.z);

                if (i > 0)
                {
                    if (offsetVector.x != 0)
                    {
                        // Учитываем только длину по X
                        currentOffset.x += (renderers[i - 1].bounds.extents.x + bounds.extents.x + Mathf.Abs(offsetVector.x)) * xSign;
                    }

                    if (offsetVector.z != 0)
                    {
                        // Учитываем только длину по Z
                        currentOffset.z += (renderers[i - 1].bounds.extents.z + bounds.extents.z + Mathf.Abs(offsetVector.z)) * zSign;
                    }
                }

                Vector3 centerOffset =
                    new(
                        (renderer.transform.position.x - renderer.bounds.center.x),
                        0,
                        (renderer.transform.position.z - renderer.bounds.center.z)
                    );

                // Устанавливаем позицию с учетом смещения
                renderer.transform.position = model.transform.position + currentOffset + centerOffset;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = gizmosColor;

            if (renderers.Length != 0)
                foreach (MeshRenderer renderer in renderers)
                {
                    Gizmos.DrawWireCube(renderer.bounds.center, renderer.bounds.size);
                    Gizmos.DrawWireSphere(renderer.bounds.center, 0.1f);
                }
        }
    }
}
