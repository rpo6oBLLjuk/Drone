using CustomInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CopyTreeFromTerrain : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private Transform treeContainer;

    [SerializeField] private string basePath = "Assets/Prefabs/CombinedMesh";
    [SerializeField, Button(nameof(CreateTreeFromTerrain))] private bool hideTerrain;
    [SerializeField] private bool hideTrees;


    [SerializeField] private Dictionary<Transform, List<GameObject>> instantiatedTrees = new();


    private void CreateTreeFromTerrain()
    {
        // Удаляем все ранее инстанцированные деревья и контейнеры
        foreach (var container in instantiatedTrees.Keys)
        {
            if (container != null)
                DestroyImmediate(container.gameObject);
        }
        instantiatedTrees.Clear();

        TerrainData terrainData = terrain.terrainData;

        // Получаем все деревья на Terrain
        TreeInstance[] allTrees = terrainData.treeInstances;
        TreePrototype[] treePrototypes = terrainData.treePrototypes;

        // Локальный словарь для хранения контейнеров
        Dictionary<Vector2Int, Transform> gridContainers = new();

        // Проходим по каждому дереву и создаем экземпляр
        foreach (TreeInstance tree in allTrees)
        {
            // Находим префаб для текущего дерева
            int prototypeIndex = tree.prototypeIndex;
            GameObject treePrefab = treePrototypes[prototypeIndex].prefab;

            // Вычисляем мировую позицию дерева
            Vector3 worldPosition = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // Вычисляем, в какой квадрат 10x10 попадает дерево
            Vector2Int gridPos = new Vector2Int(
                Mathf.FloorToInt(worldPosition.x / 10),
                Mathf.FloorToInt(worldPosition.z / 10)
            );

            // Проверяем, существует ли уже контейнер для этой сетки
            if (!gridContainers.ContainsKey(gridPos))
            {
                // Если нет, создаем новый объект-контейнер
                GameObject container = new GameObject($"TreeGroup_{gridPos.x}_{gridPos.y}");
                container.transform.SetParent(treeContainer);
                container.transform.position = new Vector3(gridPos.x * 10, 0, gridPos.y * 10); // Позиция контейнера

                // Добавляем контейнер в локальный словарь
                gridContainers[gridPos] = container.transform;
                instantiatedTrees[container.transform] = new List<GameObject>(); // Создаем новый список для этого контейнера
            }

            // Создаем экземпляр дерева в соответствующем контейнере
            GameObject treeInstance = Instantiate(treePrefab, worldPosition, Quaternion.identity, gridContainers[gridPos]);

            // Добавляем дерево в список
            instantiatedTrees[gridContainers[gridPos]].Add(treeInstance);
        }

        Debug.Log($"Количество контейнеров: {instantiatedTrees.Count}");

        if (hideTerrain)
            terrain.gameObject.SetActive(false);

        CombineMesh();
    }



    [ExecuteInEditMode]
    private void CombineMesh()
    {
        if (instantiatedTrees.Count == 0)
            return;

        List<List<CombineInstance>> combineInstancesBySubMesh = new();

        foreach (List<GameObject> currentInstance in instantiatedTrees.Values)
        {
            foreach (GameObject tree in currentInstance)
            {
                combineInstancesBySubMesh.Add(new List<CombineInstance>());

                MeshFilter meshFilter = tree.GetComponentInChildren<MeshFilter>(true);
                if (meshFilter != null)
                {
                    Mesh mesh = meshFilter.sharedMesh;

                    for (int i = 0; i < mesh.subMeshCount; i++)
                    {
                        CombineInstance combine = new CombineInstance
                        {
                            mesh = mesh,
                            transform = meshFilter.transform.localToWorldMatrix,
                            subMeshIndex = i
                        };

                        // Добавляем в соответствующий список по индексу submesh
                        combineInstancesBySubMesh.Last().Add(combine);
                    }
                }
            }
        }


        // Создаем объект для каждого submesh
        for (int i = 0; i < combineInstancesBySubMesh.Count; i++)
        {
            List<CombineInstance> combineInstances = combineInstancesBySubMesh[i];

            // Создаем объект для комбинированного меша
            GameObject combinedMeshObject = new($"CombinedMesh_SubMesh_{i}");
            combinedMeshObject.transform.position = Vector3.zero;

            MeshFilter combinedMeshFilter = combinedMeshObject.AddComponent<MeshFilter>();
            MeshRenderer combinedMeshRenderer = combinedMeshObject.AddComponent<MeshRenderer>();

            Mesh combinedMesh = new();
            combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);

            combinedMeshFilter.mesh = combinedMesh;

            // Устанавливаем материал для комбинированного меша
            Material[] materials = instantiatedTrees.ElementAt(i).Value.First().GetComponentInChildren<MeshRenderer>().materials;
            combinedMeshRenderer.material = materials[i < materials.Length ? i : 0];

            SaveMeshAsset(combinedMesh, $"CombinedMesh_SubMesh_{i}.asset");
            Debug.Log($"Меш {combinedMeshObject.name} успешно создан.");
        }

        if (hideTrees)
            treeContainer.gameObject.SetActive(false);

        Debug.Log("Все меши деревьев успешно объединены.");
    }

    private void SaveMeshAsset(Mesh mesh, string fileName)
    {
        string path = basePath + fileName;

        if (AssetDatabase.LoadAssetAtPath<Mesh>(path) != null)
        {
            AssetDatabase.DeleteAsset(path);
        }

        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Меш сохранен по пути: {path}");
    }
}
