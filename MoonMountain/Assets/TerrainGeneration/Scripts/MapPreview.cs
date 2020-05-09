using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map
{
	public string name;
	public MeshSettings meshSettings;
	public HeightMapSettings heightMapSettings;
	public TextureData textureData;
	public Material skyBox;
}

public class MapPreview : MonoBehaviour 
{

	public bool autoUpdate;
	[SerializeField] private enum DrawMode { Mesh, NoiseMap, FalloffMap };
	[Header("General")]
	[SerializeField] private DrawMode drawMode;
	public Material terrainMaterial;
	[Header("Preview")]
	public MeshFilter previewMeshFilter;
	[SerializeField] private Renderer previewTextureRenderer;
	[SerializeField] private MeshRenderer previewMeshRenderer;
	[Header("Map")]
	public int mapIndexSelector;
	[Range(0, MeshSettings.numSupportedLODs - 1)] [SerializeField] private int editorPreviewLOD; // LOD: 1, 2, 4, 8 . . .
	public List<Map> maps = new List<Map>();




	public void DrawMapInEditor() {
		maps[mapIndexSelector].textureData.ApplyToMaterial(terrainMaterial);
		maps[mapIndexSelector].textureData.UpdateMeshHeights(terrainMaterial, maps[mapIndexSelector].heightMapSettings.minHeight, maps[mapIndexSelector].heightMapSettings.maxHeight);
		
		HeightMap heightMap = HeightMapGenerator.GenerateHeightMap(maps[mapIndexSelector].meshSettings.numVertsPerLine, maps[mapIndexSelector].meshSettings.numVertsPerLine, maps[mapIndexSelector].heightMapSettings, Vector2.zero);
		RenderSettings.skybox = maps[mapIndexSelector].skyBox;

		Texture2D noiseMap = null;
		Texture2D falloffMap = null;

		if (drawMode == DrawMode.NoiseMap) 
		{
			noiseMap = TextureGenerator.TextureFromHeightMap(heightMap);
			DrawTexture(noiseMap);
		} 
		else if (drawMode == DrawMode.Mesh) 
		{
			noiseMap = TextureGenerator.TextureFromHeightMap(heightMap);
			DrawMesh(MeshGenerator.GenerateTerrainMesh(heightMap.values, maps[mapIndexSelector].meshSettings, editorPreviewLOD));
		} 
		else if (drawMode == DrawMode.FalloffMap) {
			DrawTexture(falloffMap);
		}
	}





	public void DrawTexture(Texture2D texture) {
		previewTextureRenderer.sharedMaterial.mainTexture = texture;
		previewTextureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height) / 10f;

		previewTextureRenderer.gameObject.SetActive(true);
		previewMeshFilter.gameObject.SetActive(false);
	}

	public void DrawMesh(MeshData meshData) {
		if (previewMeshFilter != null)
		{
			previewMeshFilter.sharedMesh = meshData.CreateMesh();
		}

		previewTextureRenderer.gameObject.SetActive(false);
		previewMeshFilter.gameObject.SetActive(true);
	}



	void OnValuesUpdated() {
		if (!Application.isPlaying) 
		{
			DrawMapInEditor ();
		}
	}

	void OnTextureValuesUpdated() {
		maps[mapIndexSelector].textureData.ApplyToMaterial(terrainMaterial);
	}

	void OnValidate() 
	{
		if (maps[mapIndexSelector].meshSettings != null)
		{
			maps[mapIndexSelector].meshSettings.OnValuesUpdated -= OnValuesUpdated;
			maps[mapIndexSelector].meshSettings.OnValuesUpdated += OnValuesUpdated;
		}
		if (maps[mapIndexSelector].heightMapSettings != null)
		{
			maps[mapIndexSelector].heightMapSettings.OnValuesUpdated -= OnValuesUpdated;
			maps[mapIndexSelector].heightMapSettings.OnValuesUpdated += OnValuesUpdated;
		}
		if (maps[mapIndexSelector].textureData != null)
		{
			maps[mapIndexSelector].textureData.OnValuesUpdated -= OnTextureValuesUpdated;
			maps[mapIndexSelector].textureData.OnValuesUpdated += OnTextureValuesUpdated;
		}
		//Clamp map-index
		if (mapIndexSelector < 0)
		{
			mapIndexSelector = 0;
		}
		if (mapIndexSelector > maps.Count - 1)
		{
			mapIndexSelector = maps.Count - 1;
		}

	}

}
