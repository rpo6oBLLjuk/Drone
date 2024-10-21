using CustomInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]

public class CopyTreeFromTerrain : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private Transform treeContainer;
    [SerializeField] private Transform combinesContainer;

    [SerializeField] private string basePath = "Assets/Prefabs/CombinedMesh";
    [SerializeField, Button(nameof(CreateTreeFromTerrain))] private bool hideTerrain;
    [SerializeField, Button(nameof(CombineMesh))] private bool hideTrees;

    [SerializeField] private Dictionary<Transform, List<GameObject>> instantiatedTrees = new();

    [SerializeField, Range(1, 200)] private float gridSize = 50;


    [ExecuteInEditMode]
    private void CreateTreeFromTerrain()
    {
        if (treeContainer != null)
            DestroyImmediate(treeContainer.gameObject);
        treeContainer = new GameObject("Instantiated Trees container").transform;

        instantiatedTrees.Clear();


        TerrainData terrainData = terrain.terrainData;

        // �������� ��� ������� �� Terrain
        TreeInstance[] allTrees = terrainData.treeInstances;
        TreePrototype[] treePrototypes = terrainData.treePrototypes;

        // ��������� ������� ��� �������� �����������
        Dictionary<Vector2Int, Transform> gridContainers = new();

        // �������� �� ������� ������ � ������� ���������
        foreach (TreeInstance tree in allTrees)
        {
            int prototypeIndex = tree.prototypeIndex;
            GameObject treePrefab = treePrototypes[prototypeIndex].prefab;

            Vector3 worldPosition = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            Vector2Int gridPos = new Vector2Int(
                Mathf.FloorToInt(worldPosition.x / gridSize),
                Mathf.FloorToInt(worldPosition.z / gridSize)
            );

            // ���������, ���������� �� ��� ��������� ��� ���� �����
            if (!gridContainers.ContainsKey(gridPos))
            {
                // ���� ���, ������� ����� ������-���������
                GameObject container = new GameObject($"TreeGroup_{gridPos.x}_{gridPos.y}");
                container.transform.SetParent(treeContainer);
                container.transform.position = terrain.transform.position + new Vector3(gridSize * (gridPos.x - 1), 0, gridSize * (gridPos.y - 1));

                // ��������� ��������� � ��������� �������
                gridContainers[gridPos] = container.transform;
                instantiatedTrees[container.transform] = new List<GameObject>(); // ������� ����� ������ ��� ����� ����������
            }

            // ������� ��������� ������ � ��������������� ����������
            GameObject treeInstance = Instantiate(treePrefab, worldPosition, Quaternion.identity, gridContainers[gridPos]);

            // ��������� ������ � ������
            instantiatedTrees[gridContainers[gridPos]].Add(treeInstance);
        }

        Debug.Log($"���������� �����������: {instantiatedTrees.Count}");

        if (hideTerrain)
            terrain.gameObject.SetActive(false);
    }

    [ExecuteInEditMode]
    private void CombineMesh()
    {
        if (instantiatedTrees.Count == 0)
            return;

        if (combinesContainer != null)
            DestroyImmediate(combinesContainer.gameObject);
        combinesContainer = new GameObject("Combines container").transform;

#if UNITY_EDITOR
        AssetDatabase.DeleteAsset($"{basePath}{SceneManager.GetActiveScene().name}/");
#endif

        List<(List<CombineInstance> barks, List<CombineInstance> leaves)> combineInstancesBySubMesh = new();

        foreach (List<GameObject> currentInstance in instantiatedTrees.Values)
        {
            combineInstancesBySubMesh.Add((new List<CombineInstance>(), new List<CombineInstance>()));

            foreach (GameObject tree in currentInstance)
            {
                MeshFilter meshFilter = tree.GetComponentInChildren<MeshFilter>(true);
                if (meshFilter != null)
                {
                    Mesh mesh = meshFilter.sharedMesh;

                    CombineInstance barksCombine = new()
                    {
                        mesh = mesh,
                        transform = meshFilter.transform.localToWorldMatrix,
                        subMeshIndex = 0
                    };
                    combineInstancesBySubMesh.Last().barks.Add(barksCombine);

                    CombineInstance leavesCombine = new()
                    {
                        mesh = mesh,
                        transform = meshFilter.transform.localToWorldMatrix,
                        subMeshIndex = 1
                    };
                    combineInstancesBySubMesh.Last().leaves.Add(leavesCombine);
                }
            }
        }

        // ������� ������ ��� ������� submesh
        for (int i = 0; i < combineInstancesBySubMesh.Count; i++)
        {
            CreateMesh(combineInstancesBySubMesh[i].barks, i, "bark");
            CreateMesh(combineInstancesBySubMesh[i].leaves, i, "leaves");
        }

        if (hideTrees)
            treeContainer.gameObject.SetActive(false);

        Debug.Log("��� ���� �������� ������� ����������.");
    }

    private void CreateMesh(List<CombineInstance> combineInstances, int i, string combineSubname)
    {
        string objName = $"CombinedMesh_SubMesh_{i}_{combineSubname}";
        Vector3 targetPos = instantiatedTrees.Keys.ElementAt(i).transform.position;

        GameObject CombMeshObjContainer = new($"{objName}_container");
        GameObject combinedMeshObject = new(objName);

        CombMeshObjContainer.transform.position = targetPos;
        combinedMeshObject.transform.position = Vector3.zero;

        CombMeshObjContainer.transform.parent = combinesContainer;
        combinedMeshObject.transform.parent = CombMeshObjContainer.transform;

        MeshFilter combinedMeshFilter = combinedMeshObject.AddComponent<MeshFilter>();
        MeshRenderer combinedMeshRenderer = combinedMeshObject.AddComponent<MeshRenderer>();

        Mesh combinedMesh = new();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);

        combinedMeshFilter.mesh = combinedMesh;
        combinedMeshRenderer.sharedMaterial = instantiatedTrees.Values
            .SelectMany(trees => trees)
            .ToList()[i]
            .GetComponentInChildren<MeshRenderer>()
            .sharedMaterials[combineSubname == "bark" ? 0 : 1];

        LODGroup lodGroup = CombMeshObjContainer.AddComponent<LODGroup>();

        LOD[] lods = new LOD[3];
        lods[0] = new LOD(0.5f, new Renderer[] { combinedMeshRenderer }); // ������� 0, ������������ �� ������� �����������
        lods[1] = new LOD(0.25f, new Renderer[] { combinedMeshRenderer }); // ������� 1
        lods[2] = new LOD(0.1f, new Renderer[] { combinedMeshRenderer });  // ������� 2 (�� ������� �����������)

        lodGroup.SetLODs(lods);
        lodGroup.size = 5;

        SaveMeshAsset(combinedMesh, objName);
        Debug.Log($"��� {combinedMeshObject.name} ������� ������.");
    }

    private void SaveMeshAsset(Mesh mesh, string fileName)
    {
        string path = $"{basePath}_{SceneManager.GetActiveScene().name}/{fileName}.asset";

        string directory = System.IO.Path.GetDirectoryName(path);
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }

#if UNITY_EDITOR
        if (AssetDatabase.LoadAssetAtPath<Mesh>(path) != null)
        {
            AssetDatabase.DeleteAsset(path);
        }

        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif

        Debug.Log($"��� �������� �� ����: {path}");
    }

    private void OnDrawGizmos()
    {
        float gridXCount = 1000 / gridSize; // ���������� ������ �� ������
        float gridZCount = 1000 / gridSize; // ���������� ������ �� ������

        Gizmos.color = Color.green; // ���� ����� �����

        // ������ ������������ �����
        for (int x = 0; x <= gridXCount; x++)
        {
            Vector3 start = terrain.transform.position + new Vector3(x * gridSize, 0, 0);
            Vector3 end = start + new Vector3(0, 0, gridZCount * gridSize);
            Gizmos.DrawLine(start, end);
        }

        // ������ �������������� �����
        for (int z = 0; z <= gridZCount; z++)
        {
            Vector3 start = terrain.transform.position + new Vector3(0, 0, z * gridSize);
            Vector3 end = start + new Vector3(gridXCount * gridSize, 0, 0);
            Gizmos.DrawLine(start, end);
        }
    }
}
