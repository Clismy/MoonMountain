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
        GameObject terrainChunckObject;
        Mesh terrainChunckMesh;

        for (int i = 0; i < selectedGameObject.Length; i++)
        {
            MeshFilter mf = selectedGameObject[i].transform.GetComponent<MeshFilter>();
            if (mf != null)
            {
                string terrainChunk = "Assets/Resources/SavedMeshes/GameObjects/TerrainChunk (" + i + ").prefab";
                PrefabUtility.SaveAsPrefabAsset(selectedGameObject[i], terrainChunk);
                string savePath = "Assets/Resources/SavedMeshes/Meshes/TerrainMesh (" + i + ").asset";
                Debug.Log("Saved Mesh to:" + savePath);
                AssetDatabase.CreateAsset(mf.mesh, savePath);
            }
            else
            {
                Debug.Log("Didn't save terrain asset number " + i);
            }

            terrainChunckObject = Resources.Load<GameObject>("SavedMeshes/GameObjects/TerrainChunk (" + i + ")");
            terrainChunckMesh = Resources.Load<Mesh>("SavedMeshes/Meshes/TerrainMesh (" + i + ")");
            if(terrainChunckObject == null)
            {
                Debug.Log("no gameobject");
            }
            if (terrainChunckMesh == null)
            {
                Debug.Log("no mesh");
            }
            terrainChunckObject.GetComponent<MeshFilter>().mesh = terrainChunckMesh;
        }
    }
}

#endif