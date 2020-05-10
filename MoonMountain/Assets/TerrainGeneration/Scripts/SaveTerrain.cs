#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class SaveTerrain : MonoBehaviour
{
    public KeyCode saveKey = KeyCode.F12;
    public string saveName = "SavedMesh";
    public GameObject[] selectedGameObject;

    void Update()
    {
        if (Input.GetKeyDown(saveKey))
        {
            SaveAsset();
        }
    }

    void SaveAsset()
    {
        Mesh terrainChunckMesh;

        for (int i = 0; i < selectedGameObject.Length; i++)
        {
            MeshFilter mf = selectedGameObject[i].transform.GetComponent<MeshFilter>();
            if (mf != null)
            {
                string savePath = "Assets/Resources/SavedMeshes/Meshes/TerrainMesh (" + i + ").asset";
                AssetDatabase.CreateAsset(mf.mesh, savePath);
                string terrainChunk = "Assets/Resources/SavedMeshes/GameObjects/TerrainChunk (" + i + ").prefab";
                terrainChunckMesh = Resources.Load<Mesh>("SavedMeshes/Meshes/TerrainMesh (" + i + ")");
                selectedGameObject[i].GetComponent<MeshFilter>().sharedMesh = terrainChunckMesh;
                selectedGameObject[i].GetComponent<MeshCollider>().sharedMesh = terrainChunckMesh;
                PrefabUtility.SaveAsPrefabAsset(selectedGameObject[i], terrainChunk);
            }
            else
            {
                Debug.Log("Didn't save terrain asset number " + i);
            }
        }
    }
}

#endif