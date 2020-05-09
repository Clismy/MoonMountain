using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SnowPile : MonoBehaviour
{
    public byte[,,] data;
    public int worldX = 16;
    public int worldY = 16;
    public int worldZ = 16;

    public GameObject chunk;
    public C_Chunk3[,,] chunks;  //Changed from public GameObject[,,] chunks;
    public int chunkSize = 16;
    public float cubeSize = 0.5f;
    public int chungL;
    public int datraL;
    public void Start()
    {

       data = new byte[worldX, worldY, worldZ];

        for (int x = 1; x < worldX-2; x++)
        {
            for (int z = 1; z < worldZ-1; z++)
            {
                //int stone = PerlinNoise(x, 0, z, 10, 3, 1.2f);
                //stone += PerlinNoise(x, 300, z, 20, 4, 0) + 10;
                //int dirt = PerlinNoise(x, 100, z, 50, 2, 0) + 1; //Added +1 to make sure minimum grass height is 1
                //stone = 0;
                for (int y = 1; y < worldY-1; y++)
                {
                    //if (y <= stone)
                    //{
                    //    data[x, y, z] = 1;
                    //}
                    data[x, y, z] = 1;

                }
            }
        }


        chunks = new C_Chunk3[Mathf.FloorToInt(worldX / chunkSize),
        Mathf.FloorToInt(worldY / chunkSize), Mathf.FloorToInt(worldZ / chunkSize)];

        Debug.Log(" hej " +chunks.GetLength(0) + "  " + chunks.GetLength(1) + "  " + chunks.GetLength(2));


        for (int x = 0; x < chunks.GetLength(0); x++)
        {
            for (int y = 0; y < chunks.GetLength(1); y++)
            {
                for (int z = 0; z < chunks.GetLength(2); z++)
                {
                    
                    //Create a temporary Gameobject for the new chunk instead of using chunks[x,y,z]
                    GameObject newChunk = Instantiate(chunk, new Vector3(x * cubeSize  - 0.5f + transform.position.x,
                                                                         y * cubeSize  + 0.5f+ transform.position.y , 
                                                                         z * cubeSize  - 0.5f+ transform.position.z ), new Quaternion(0, 0, 0, 0),transform) as GameObject;

                    //Now instead of using a temporary variable for the script assign it
                    //to chunks[x,y,z] and use it instead of the old \"newChunkScript\" 
                    chunks[x, y, z] = newChunk.GetComponent("C_Chunk3") as C_Chunk3;
                    chunks[x, y, z].snoPileGo = gameObject;
                    chunks[x, y, z].cubeSize = cubeSize;
                    chunks[x, y, z].chunkSize = chunkSize;
                    chunks[x, y, z].chunkX = x * chunkSize;
                    chunks[x, y, z].chunkY = y * chunkSize;
                    chunks[x, y, z].chunkZ = z * chunkSize;

                }
            }
        }
        datraL = data.Length;
        chungL = chunks.Length;
    }


    int PerlinNoise(int x, int y, int z, float scale, float height, float power)
    {
        float rValue;
        rValue = (float)SimplexNoise.Noise01(((double)x) / scale, ((double)y) / scale, ((double)z) / scale);
        rValue *= height;

        if (power != 0)
        {
            rValue = Mathf.Pow(rValue, power);
        }

        return (int)rValue;
    }

    public byte Block(int x, int y, int z)
    {

        if (x >= worldX || x < 0 || y >= worldY || y < 0 || z >= worldZ || z < 0)
        {
            return (byte)1;
        }

        return data[x, y, z];
    }






    public void GenColumn(int x, int z)
    {

        for (int y = 0; y < chunks.GetLength(1); y++)
        {


            //Create a temporary Gameobject for the new chunk instead of using chunks[x,y,z]
            GameObject newChunk = Instantiate(chunk, new Vector3(x *cubeSize   - 0.5f + transform.position.x,
                                                                 y *cubeSize   + 0.5f+ transform.position.y , 
                                                                 z * cubeSize  - 0.5f + transform.position.z), new Quaternion(0, 0, 0, 0), transform) as GameObject;

            chunks[x, y, z] = newChunk.GetComponent("C_Chunk3") as C_Chunk3;

            chunks[x, y, z].snoPileGo = gameObject;
            chunks[x, y, z].chunkSize = chunkSize;
            chunks[x, y, z].cubeSize = cubeSize;
            chunks[x, y, z].chunkX = x * chunkSize;
            chunks[x, y, z].chunkY = y * chunkSize;
            chunks[x, y, z].chunkZ = z * chunkSize;


        }

    }

    public void UnloadColumn(int x, int z)
    {
        for (int y = 0; y < chunks.GetLength(1); y++)
        {
            Object.Destroy(chunks[x, y, z].gameObject);

        }
    }



    private void OnDrawGizmos()
    {
        float centerX = transform.position.x - chunkSize * 0.38f;
        float centerY = transform.position.y - chunkSize * 0.5f;
        float centerZ = transform.position.z - chunkSize * 0.38f;



        Gizmos.DrawWireCube(new Vector3(centerX, centerY, centerZ), new Vector3(worldX, worldY, worldZ) * chunkSize* 0.25f);
    }




}
