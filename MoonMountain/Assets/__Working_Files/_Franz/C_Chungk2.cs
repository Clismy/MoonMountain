using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Chungk2 : MonoBehaviour
{
    public Material baseMaterial;


    public Shader snow_tack_shader;
    public bool update;

    public float chunkSize = 0.5f;
    // public int chunkSize = 16;
    public GameObject worldGO;
    private C_World world;


    public int chunkX;
    public int chunkY;
    public int chunkZ;





    private List<Vector3> newVertices = new List<Vector3>();
    private List<int> newTriangles = new List<int>();
    private List<Vector2> newUV = new List<Vector2>();

    private float tUnit = 0.25f;
    private Vector2 tStone = new Vector2(1, 0);
    private Vector2 tGrass = new Vector2(0, 1);

    private Mesh mesh;
    private MeshCollider col;

    private int faceCount;

    private Vector2 tGrassTop = new Vector2(1, 1);




    public RenderTexture _splatmap;

    public RenderTexture getSetSplatMap
    {
        get
        {
            if( _splatmap == null)
            {
                GeneretNewMaterial();
            }
            return _splatmap;
        }
        set { _splatmap = value; }
    }


    void Start()
    {


        mesh = GetComponent<MeshFilter>().mesh;
        col = GetComponent<MeshCollider>();

       
    }


    public void GeneretNewMaterial()
    {

        Material newMat = new Material(snow_tack_shader);

        newMat.SetFloat  ("_Tess"        , baseMaterial.GetFloat("_Tess"));
        newMat.SetColor  ("_SnowColor"   , baseMaterial.GetColor("_SnowColor"));
        newMat.SetTexture("_SnowTex"     , baseMaterial.GetTexture("_SnowTex"));
        newMat.SetColor  ("_GroundColor" , baseMaterial.GetColor("_GroundColor"));
        newMat.SetTexture("_GroundTex"   , baseMaterial.GetTexture("_GroundTex"));
        newMat.SetTexture("_Splat"       , _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
        newMat.SetFloat  ("_Displacement", baseMaterial.GetFloat("_Displacement"));
        newMat.SetFloat  ("_Glossiness"  , baseMaterial.GetFloat("_Glossiness"));
        newMat.SetFloat  ("_Metallic"    , baseMaterial.GetFloat("_Metallic"));




        GetComponent<MeshRenderer>().material = newMat;//
    }



    

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        col.sharedMesh = null;
        col.sharedMesh = mesh;

        newVertices.Clear();
        newUV.Clear();
        newTriangles.Clear();

        faceCount = 0; //Fixed: Added this thanks to a bug pointed out by ratnushock!

    }

 
    

    void Cube(Vector2 texturePos)
    {

        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 1); //2
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4 + 3); //4

        newUV.Add(new Vector2(tUnit * texturePos.x + tUnit, tUnit * texturePos.y));
        newUV.Add(new Vector2(tUnit * texturePos.x + tUnit, tUnit * texturePos.y + tUnit));
        newUV.Add(new Vector2(tUnit * texturePos.x, tUnit * texturePos.y + tUnit));
        newUV.Add(new Vector2(tUnit * texturePos.x, tUnit * texturePos.y));

        faceCount++; // Add this line
    }

    void CubeTop(float x, float y, float z)
    {
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x, y, z));

    }
    void CubeNorth(float x, float y, float z)
    {
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));
      
    }
    void CubeEast(float x, float y, float z)
    {
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
       
    }
    void CubeSouth(float x, float y, float z)
    {
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        
    }
    void CubeWest(float x, float y, float z)
    {
        newVertices.Add(new Vector3(x, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x, y - 1, z));
       
    }
    void CubeBot(float x, float y, float z)
    {
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

    }



   
}
