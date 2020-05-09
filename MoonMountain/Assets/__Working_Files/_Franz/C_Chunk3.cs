﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Chunk3 : MonoBehaviour
{

       
    public bool update;

    public float cubeSize = 0.5f;
     public int chunkSize = 16;
    public GameObject snoPileGo;
    private C_SnowPile snowPile;


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
   
    
    void Start()
    {


        mesh = GetComponent<MeshFilter>().mesh;
        col = GetComponent<MeshCollider>();
        snowPile = snoPileGo.GetComponent("C_SnowPile") as C_SnowPile;


        GenerateMesh();

    }


    void LateUpdate()
    {


        if (update)
        {
            GenerateMesh();
            update = false;
        }
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

    void CubeTop(int x, int y, int z, byte block)
    {





 
        newVertices.Add(new Vector3(x       , y, z + cubeSize));
        newVertices.Add(new Vector3(x + cubeSize, y, z + cubeSize));
        newVertices.Add(new Vector3(x + cubeSize, y, z));
        newVertices.Add(new Vector3(x        , y, z));


        Vector2 texturePos = new Vector2(0, 0);

        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }


        Cube(texturePos);

    }
    void CubeNorth(int x, int y, int z, byte block)
    {
   
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z + cubeSize));
        newVertices.Add(new Vector3(x + cubeSize, y        , z + cubeSize));
        newVertices.Add(new Vector3(x        , y        , z + cubeSize));
        newVertices.Add(new Vector3(x        , y - cubeSize, z + cubeSize));

        Vector2 texturePos;

        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }

        Cube(texturePos);

    }
    void CubeEast(int x, int y, int z, byte block)
    {
      
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z));
        newVertices.Add(new Vector3(x + cubeSize, y        , z));
        newVertices.Add(new Vector3(x + cubeSize, y        , z + cubeSize));
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z + cubeSize));

        Vector2 texturePos;

        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }


        Cube(texturePos);

    }
    void CubeSouth(int x, int y, int z, byte block)
    {
 
        newVertices.Add(new Vector3(x        , y - cubeSize, z));
        newVertices.Add(new Vector3(x        , y        , z));
        newVertices.Add(new Vector3(x + cubeSize, y        , z));
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z));

        Vector2 texturePos;

        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }


        Cube(texturePos);

    }
    void CubeWest(int x, int y, int z, byte block)
    {
    
        newVertices.Add(new Vector3(x, y - cubeSize, z + cubeSize));
        newVertices.Add(new Vector3(x, y        , z + cubeSize));
        newVertices.Add(new Vector3(x, y        , z));
        newVertices.Add(new Vector3(x, y - cubeSize, z));

        Vector2 texturePos;


        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }


        Cube(texturePos);

    }
    void CubeBot(int x, int y, int z, byte block)
    {

        newVertices.Add(new Vector3(x        , y - cubeSize, z));
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z));
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z + cubeSize));
        newVertices.Add(new Vector3(x        , y - cubeSize, z + cubeSize));

        Vector2 texturePos;

        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }


        Cube(texturePos);

    }







    void CubeTop(  float x, float y, float z, byte block)
    {






        newVertices.Add(new Vector3(x, y, z + cubeSize));
        newVertices.Add(new Vector3(x + cubeSize, y, z + cubeSize));
        newVertices.Add(new Vector3(x + cubeSize, y, z));
        newVertices.Add(new Vector3(x, y, z));


        Vector2 texturePos = new Vector2(0, 0);



        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }



        Cube(texturePos);

    }
    void CubeNorth(float x, float y, float z, byte block)
    {

        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z + cubeSize));
        newVertices.Add(new Vector3(x + cubeSize, y, z + cubeSize));
        newVertices.Add(new Vector3(x, y, z + cubeSize));
        newVertices.Add(new Vector3(x, y - cubeSize, z + cubeSize));

        Vector2 texturePos;


        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }

        Cube(texturePos);

    }
    void CubeEast( float x, float y, float z, byte block)
    {

        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z));
        newVertices.Add(new Vector3(x + cubeSize, y, z));
        newVertices.Add(new Vector3(x + cubeSize, y, z + cubeSize));
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z + cubeSize));

        Vector2 texturePos;


        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }



        Cube(texturePos);

    }
    void CubeSouth(float x, float y, float z, byte block)
    {

        newVertices.Add(new Vector3(x, y - cubeSize, z));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + cubeSize, y, z));
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z));

        Vector2 texturePos;


        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }



        Cube(texturePos);

    }
    void CubeWest( float x, float y, float z, byte block)
    {

        newVertices.Add(new Vector3(x, y - cubeSize, z + cubeSize));
        newVertices.Add(new Vector3(x, y, z + cubeSize));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x, y - cubeSize, z));

        Vector2 texturePos;


        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }



        Cube(texturePos);

    }
    void CubeBot(  float x, float y, float z, byte block)
    {

        newVertices.Add(new Vector3(x, y - cubeSize, z));
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z));
        newVertices.Add(new Vector3(x + cubeSize, y - cubeSize, z + cubeSize));
        newVertices.Add(new Vector3(x, y - cubeSize, z + cubeSize));

        Vector2 texturePos;


        texturePos = tStone;
        if (block == 2)
        {
            texturePos = tGrass;
        }


        Cube(texturePos);

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




    byte Block(int x, int y, int z)
    {
        return snowPile.Block(x + chunkX, y + chunkY, z + chunkZ); // Don't replace the Block in this line!
    }

    public void GenerateMesh()
    {


      
       Vector3 transfo =  snoPileGo.transform.position;
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
              
                    //This code will run for every block in the chunk

                    if (Block(x, y, z) != 0)
                    {
                        //If the block is solid

                        float xs = x * cubeSize;
                        float ys = y * cubeSize;
                        float zs = z * cubeSize;


                        if (Block(x, y + 1, z) == 0)
                            CubeTop(   xs, ys, zs, Block(x, y, z));

                        if (Block(x, y - 1, z) == 0)
                            CubeBot(   xs, ys, zs, Block(x, y, z));

                        if (Block(x + 1, y, z) == 0)
                            CubeEast(  xs, ys, zs, Block(x, y, z));
                        if (Block(x - 1, y, z) == 0)
                            CubeWest(  xs, ys, zs, Block(x, y, z));
                        if (Block(x, y, z + 1) == 0)
                            CubeNorth( xs, ys, zs, Block(x, y, z));
                        if (Block(x, y, z - 1) == 0)
                            CubeSouth( xs, ys, zs, Block(x, y, z));

                        

                    }

                }
            }
        }

        UpdateMesh();
    }

}
