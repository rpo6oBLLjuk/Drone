using CustomInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CopyTreeFromTerrain : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private Transform treeContainer;

    [SerializeField, Button(nameof(CreateTreeFromTerrain))] private bool hideTerrain;
    [SerializeField, Button(nameof(CombineMesh))] private bool hideTrees;

    [SerializeField] private List<GameObject> instantiatedTrees = new();


    private void Start()
    {

    }

    [ExecuteInEditMode]
    private void CombineMesh()
    {
        if (instantiatedTrees.Count == 0)
            return;

        MeshFilter firstMeshFilter = instantiatedTrees[0].GetComponentInChildren<MeshFilter>(true);
        Mesh firstMesh = firstMeshFilter.sharedMesh;

        List<List<CombineInstance>> combineInstancesBySubMesh = new();
        for (int i = 0; i < firstMesh.subMeshCount; i++)
        {
            combineInstancesBySubMesh.Add(new List<CombineInstance>());
        }

        foreach (GameObject tree in instantiatedTrees)
        {
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
                    combineInstancesBySubMesh[i].Add(combine);
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
            Material[] materials = firstMeshFilter.GetComponentInChildren<MeshRenderer>().materials;
            combinedMeshRenderer.material = materials[i < materials.Length ? i : 0];

            if (hideTrees)
                treeContainer.gameObject.SetActive(false);

            Debug.Log($"Меш {combinedMeshObject.name} успешно создан.");
        }

        Debug.Log("Все меши деревьев успешно объединены.");
    }


    private void SaveMeshAsset(Mesh mesh, string path)
    {
        if (AssetDatabase.LoadAssetAtPath<Mesh>(path) != null)
        {
            AssetDatabase.DeleteAsset(path);
        }

        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Меш сохранен по пути: {path}");
    }

    private void CreateTreeFromTerrain()
    {
        foreach (GameObject tree in instantiatedTrees)
        {
            if (tree != null)
                DestroyImmediate(tree);
        }
        instantiatedTrees.Clear();

        TerrainData data = terrain.terrainData;

        // Получаем данные Terrain
        TerrainData terrainData = terrain.terrainData;

        // Получаем все деревья, созданные на Terrain
        TreeInstance[] allTrees = terrainData.treeInstances;

        // Получаем все префабы деревьев
        TreePrototype[] treePrototypes = terrainData.treePrototypes;

        // Проходим по каждому дереву и создаем экземпляр
        foreach (TreeInstance tree in allTrees)
        {
            // Находим префаб для текущего дерева
            int prototypeIndex = tree.prototypeIndex;
            GameObject treePrefab = treePrototypes[prototypeIndex].prefab;

            // Вычисляем мировую позицию дерева
            Vector3 worldPosition = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // Создаем экземпляр дерева
            GameObject treeInstance = Instantiate(treePrefab, worldPosition, Quaternion.identity, treeContainer);

            // Добавляем дерево в список
            instantiatedTrees.Add(treeInstance);
        }

        Debug.Log($"Количество инстанцированных деревьев: {instantiatedTrees.Count}");

        if (hideTerrain)
            terrain.gameObject.SetActive(false);
    }
}
