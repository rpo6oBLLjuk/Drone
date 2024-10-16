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

                    // ��������� � ��������������� ������ �� ������� submesh
                    combineInstancesBySubMesh[i].Add(combine);
                }
            }
        }

        // ������� ������ ��� ������� submesh
        for (int i = 0; i < combineInstancesBySubMesh.Count; i++)
        {
            List<CombineInstance> combineInstances = combineInstancesBySubMesh[i];

            // ������� ������ ��� ���������������� ����
            GameObject combinedMeshObject = new($"CombinedMesh_SubMesh_{i}");
            combinedMeshObject.transform.position = Vector3.zero;

            MeshFilter combinedMeshFilter = combinedMeshObject.AddComponent<MeshFilter>();
            MeshRenderer combinedMeshRenderer = combinedMeshObject.AddComponent<MeshRenderer>();

            Mesh combinedMesh = new();
            combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);

            combinedMeshFilter.mesh = combinedMesh;

            // ������������� �������� ��� ���������������� ����
            Material[] materials = firstMeshFilter.GetComponentInChildren<MeshRenderer>().materials;
            combinedMeshRenderer.material = materials[i < materials.Length ? i : 0];

            if (hideTrees)
                treeContainer.gameObject.SetActive(false);

            Debug.Log($"��� {combinedMeshObject.name} ������� ������.");
        }

        Debug.Log("��� ���� �������� ������� ����������.");
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
        Debug.Log($"��� �������� �� ����: {path}");
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
