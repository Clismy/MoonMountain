using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TerrainModifyer : MonoBehaviour
{

    C_SnowPile snoPile;
    GameObject cameraGO;

    void Start()
    {

        snoPile = gameObject.GetComponent("C_SnowPile") as C_SnowPile;
        cameraGO = GameObject.FindGameObjectWithTag("MainCamera");

    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("herj");
         //  ReplaceBlockCursor(0);
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    AddBlockCursor(1);
        //}
        //LoadChunks(GameObject.FindGameObjectWithTag("Player").transform.position, 32, 48);
    }


    public void ReplaceBlockCenter(float range, byte block)
    {
        //Adds the block specified directly in front of the player

        Ray ray = new Ray(cameraGO.transform.position, cameraGO.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.distance < range)
            {
                AddBlockAt(hit, block);
            }
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction * hit.distance), Color.green, 2);
        }
    }

    public void AddBlockCenter(float range, byte block)
    {
        //Replaces the block directly in front of the player

        Ray ray = new Ray(cameraGO.transform.position, cameraGO.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.distance < range)
            {
                ReplaceBlockAt(hit, block);
            }
        }
    }

    public void ReplaceBlockCursor(byte block)
    {
        //Replaces the block specified where the mouse cursor is pointing

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            ReplaceBlockAt(hit, block);
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction * hit.distance),
             Color.green, 2);

        }
    }

    public void AddBlockCursor(byte block)
    {
        //Adds the block specified where the mouse cursor is pointing

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


    
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.GetComponent(typeof(C_Chunk3)) != null)
            {
                AddBlockAt(hit, block);
                Debug.DrawLine(ray.origin, ray.origin + (ray.direction * hit.distance),
                 Color.green, 2);
            }
       
        }
    }

    public void ReplaceBlockAt(RaycastHit hit, byte block)
    {


   

        //removes a block at these impact coordinates, you can raycast against the terrain and call this with the hit.point
        Vector3 position = hit.point;
        position += ( hit.normal * -0.5f);

        SetBlockAt(position, block);
    }

    public void AddBlockAt(RaycastHit hit, byte block)
    {
        //adds the specified block at these impact coordinates, you can raycast against the terrain and call this with the hit.point
        Vector3 position = hit.point;
        position += (hit.normal * 0.5f);

        SetBlockAt(position, block);
    }

    public void SetBlockAt(Vector3 position, byte block)
    {
        //sets the specified block at these coordinates


        int x = Mathf.RoundToInt((position.x - snoPile.transform.position.x) / snoPile.cubeSize);
        int y = Mathf.RoundToInt((position.y - snoPile.transform.position.y) / snoPile.cubeSize);
        int z = Mathf.RoundToInt((position.z - snoPile.transform.position.z) / snoPile.cubeSize);

        SetBlockAt(x, y, z, block);
    }
    public void SetBlockAt(int x, int y, int z, byte block)
    {
        //adds the specified block at these coordinates

        print("Adding: " + x + ", " + y + ", " + z);

        snoPile.data[x, y, z] = block;


        UpdateChunkAt(x, y, z);
    }

    public void UpdateChunkAt(int x, int y, int z)
    {
        //Updates the chunk containing this block

        int updateX = Mathf.FloorToInt(x / snoPile.chunkSize);
        int updateY = Mathf.FloorToInt(y / snoPile.chunkSize);
        int updateZ = Mathf.FloorToInt(z / snoPile.chunkSize);

        print("Updating: " + updateX + ", " + updateY + ", " + updateZ);


        snoPile.chunks[updateX, updateY, updateZ].update = true;
        //if ( updateX != 0)
        //    snoPile.chunks[updateX - 1, updateY, updateZ].update = true;

        //if ( updateX != snoPile.chunks.GetLength(0) - 1)
        //    snoPile.chunks[updateX + 1, updateY, updateZ].update = true;


        //if (updateY != 0)
        //    snoPile.chunks[updateX, updateY - 1, updateZ].update = true;

        //if (updateY != snoPile.chunks.GetLength(1) - 1)
        //    snoPile.chunks[updateX, updateY + 1, updateZ].update = true;


        //if ( updateZ != 0)
        //    snoPile.chunks[updateX, updateY, updateZ - 1].update = true;


        //if (updateZ != snoPile.chunks.GetLength(2) - 1)

        //    snoPile.chunks[updateX, updateY, updateZ + 1].update = true;


        if (x - (snoPile.chunkSize * updateX) == 0 && updateX != 0)
        {
            Debug.Log("1)))))))))))))");
            snoPile.chunks[updateX - 1, updateY, updateZ].update = true;
        }

        if (x - (snoPile.chunkSize * updateX) == 15 && updateX != snoPile.chunks.GetLength(0) - 1)
        {
            Debug.Log("2)))))))))))))");
            snoPile.chunks[updateX + 1, updateY, updateZ].update = true;
        }

        if (y - (snoPile.chunkSize * updateY) == 0 && updateY != 0)
        {
            Debug.Log("3)))))))))))))");
            snoPile.chunks[updateX, updateY - 1, updateZ].update = true;
        }

        if (y - (snoPile.chunkSize * updateY) == 15 && updateY != snoPile.chunks.GetLength(1) - 1)
        {
            Debug.Log("4)))))))))))))");
            snoPile.chunks[updateX, updateY + 1, updateZ].update = true;
        }

        if (z - (snoPile.chunkSize * updateZ) == 0 && updateZ != 0)
        {
            Debug.Log("4)))))))))))))");
            snoPile.chunks[updateX, updateY, updateZ - 1].update = true;
        }

        if (z - (snoPile.chunkSize * updateZ) == 15 && updateZ != snoPile.chunks.GetLength(2) - 1)
        {
            Debug.Log("6)))))))))))))");
            snoPile.chunks[updateX, updateY, updateZ + 1].update = true;
        }

    }

    public void LoadChunks(Vector3 playerPos, float distToLoad, float distToUnload)
    {


        for (int x = 0; x < snoPile.chunks.GetLength(0); x++)
        {
            for (int z = 0; z < snoPile.chunks.GetLength(2); z++)
            {

                float dist = Vector2.Distance(
                                            new Vector2(
                                                        x *  snoPile.chunkSize,
                                                        z * snoPile.chunkSize), 
                                            new Vector2(playerPos.x, playerPos.z));

                if (dist < distToLoad)
                {
                    if (snoPile.chunks[x, 0, z] == null)
                    {
                        snoPile.GenColumn(x, z);
                    }
                }
                else if (dist > distToUnload)
                {
                    if (snoPile.chunks[x, 0, z] != null)
                    {

                        snoPile.UnloadColumn(x, z);
                    }
                }

            }
        }

    }

}

