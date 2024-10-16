using CustomInspector;
using System.Collections;
using System.Collections.Generic;
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
        // ������ ��� �������� ���� �����
        List<CombineInstance> combineInstances = new();

        foreach (GameObject tree in instantiatedTrees)
        {
            MeshFilter meshFilter = tree.GetComponentInChildren<MeshFilter>(true);
            if (meshFilter != null)
            {
                CombineInstance combine = new CombineInstance
                {
                    mesh = meshFilter.sharedMesh,  // �������� ��� ������
                    transform = meshFilter.transform.localToWorldMatrix  // �������������� � ������� �������
                };
                combineInstances.Add(combine);
            }
        }


        GameObject combinedMeshObject = new GameObject("CombinedMesh");
        combinedMeshObject.transform.position = Vector3.zero;

        MeshFilter combinedMeshFilter = combinedMeshObject.AddComponent<MeshFilter>();
        MeshRenderer combinedMeshRenderer = combinedMeshObject.AddComponent<MeshRenderer>();

        Mesh combinedMesh = new();

        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);

        combinedMeshFilter.mesh = combinedMesh;
        combinedMeshRenderer.material = instantiatedTrees[0].GetComponentInChildren<MeshRenderer>(true).sharedMaterial;

        if (hideTrees)
            foreach (GameObject tree in instantiatedTrees)
            {
                tree.SetActive(false);
            }

        Debug.Log("���� �������� ������� ����������.");
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

        // �������� ������ Terrain
        TerrainData terrainData = terrain.terrainData;

        // �������� ��� �������, ��������� �� Terrain
        TreeInstance[] allTrees = terrainData.treeInstances;

        // �������� ��� ������� ��������
        TreePrototype[] treePrototypes = terrainData.treePrototypes;

        // �������� �� ������� ������ � ������� ���������
        foreach (TreeInstance tree in allTrees)
        {
            // ������� ������ ��� �������� ������
            int prototypeIndex = tree.prototypeIndex;
            GameObject treePrefab = treePrototypes[prototypeIndex].prefab;

            // ��������� ������� ������� ������
            Vector3 worldPosition = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // ������� ��������� ������
            GameObject treeInstance = Instantiate(treePrefab, worldPosition, Quaternion.identity, treeContainer);

            // ��������� ������ � ������
            instantiatedTrees.Add(treeInstance);
        }

        Debug.Log($"���������� ���������������� ��������: {instantiatedTrees.Count}");

        if (hideTerrain)
            terrain.gameObject.SetActive(false);
    }
}
