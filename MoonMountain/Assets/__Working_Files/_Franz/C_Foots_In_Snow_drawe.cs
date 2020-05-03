using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Foots_In_Snow_drawe : MonoBehaviour
{
    //  public GameObject  _terrain;
    public C_Chungk2 walk_Curent_terrain;
    public C_Chungk2 draw_Curent_terrain;
    public Transform[] _wheel;
    public Shader _drawShader;
    [Range(1, 500)]
    public float _brushSize;
    [Range(0, 1)]
    public float _brushStrenght;

    private RaycastHit _hit;
    private int _layermask;
    private Material _snowMaterial;
    private Material _drawMaterial;
    public RenderTexture walk_splatmap;
    public RenderTexture draw_splatmap;
    private Camera _camera;


    public Transform hand;

    void Start()
    {
        _layermask = LayerMask.GetMask("SnowFloor");
        _drawMaterial = new Material(_drawShader);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_camera == null)
                _camera = Camera.main;


            Ray test = new Ray(hand.position, _camera.transform.forward);
          
            if (Physics.Raycast(test,  out _hit, 0.5f))
            {
                C_Chunk3 c3 = _hit.transform.gameObject.GetComponent("C_Chunk3") as C_Chunk3;

                if (c3 != null)
                {
                   C_TerrainModifyer ter = c3.snoPileGo.transform.gameObject.GetComponent("C_TerrainModifyer") as C_TerrainModifyer;
                    ter.ReplaceBlockAt(_hit, 0);
                }

            }

        }


        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    if (_camera == null)
        //        _camera = Camera.main;


        //    if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit))
        //    {


        //        if (draw_Curent_terrain == null)
        //        {
        //            draw_Curent_terrain = _hit.transform.gameObject.GetComponent("C_Chungk2") as C_Chungk2;
        //            _snowMaterial = _hit.transform.gameObject.GetComponent<MeshRenderer>().material;
        //            _snowMaterial.SetTexture("_Splat", draw_Curent_terrain.getSetSplatMap);
        //            draw_splatmap = draw_Curent_terrain.getSetSplatMap;

        //        }
        //        else if (draw_Curent_terrain.gameObject != _hit.transform.gameObject)
        //        {

        //            draw_Curent_terrain.getSetSplatMap = draw_splatmap;


        //            draw_Curent_terrain = _hit.transform.gameObject.GetComponent("C_Chungk2") as C_Chungk2;
        //            _snowMaterial = draw_Curent_terrain.gameObject.GetComponent<MeshRenderer>().material;
        //            _snowMaterial.SetTexture("_Splat", draw_Curent_terrain.getSetSplatMap);
        //            draw_splatmap = draw_Curent_terrain.getSetSplatMap;
        //        }

        //        _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
        //        _drawMaterial.SetFloat("_Strenght", _brushStrenght);
        //        _drawMaterial.SetFloat("_Size", _brushSize);


        //        RenderTexture temp = RenderTexture.GetTemporary(draw_splatmap.width, draw_splatmap.height, 0, RenderTextureFormat.ARGBFloat);
        //        Graphics.Blit(draw_splatmap, temp);
        //        Graphics.Blit(temp, draw_splatmap, _drawMaterial);
        //        RenderTexture.ReleaseTemporary(temp);
        //    }
        //}







        for (int i = 0, lengt = _wheel.Length; i < lengt; i++)
        {
            if (Physics.Raycast(_wheel[i].position, -Vector3.up, out _hit, 1f, _layermask))
            {

                if (walk_Curent_terrain == null)
                {
                    walk_Curent_terrain = _hit.transform.gameObject.GetComponent("C_Chungk2") as C_Chungk2;
                    _snowMaterial = _hit.transform.gameObject.GetComponent<MeshRenderer>().material;
                    _snowMaterial.SetTexture("_Splat", walk_Curent_terrain.getSetSplatMap);
                    walk_splatmap = walk_Curent_terrain.getSetSplatMap;

                }
                else if (walk_Curent_terrain.gameObject != _hit.transform.gameObject)
                {

                    walk_Curent_terrain.getSetSplatMap = walk_splatmap;


                    walk_Curent_terrain = _hit.transform.gameObject.GetComponent("C_Chungk2") as C_Chungk2;
                    _snowMaterial = walk_Curent_terrain.gameObject.GetComponent<MeshRenderer>().material;
                    _snowMaterial.SetTexture("_Splat", walk_Curent_terrain.getSetSplatMap);
                    walk_splatmap = walk_Curent_terrain.getSetSplatMap;
                }

                _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_Strenght", _brushStrenght);
                _drawMaterial.SetFloat("_Size", _brushSize);


                RenderTexture temp = RenderTexture.GetTemporary(walk_splatmap.width, walk_splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(walk_splatmap, temp);
                Graphics.Blit(temp, walk_splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);

            }
        }

    }




    //private void OnGUI()
    //{
    //    if (walk_splatmap != null)
    //        GUI.DrawTexture(new Rect(0, 0, 256, 256), walk_splatmap, ScaleMode.ScaleToFit, false, 1);

    //    if (draw_splatmap != null)
    //        GUI.DrawTexture(new Rect(512, 0, 256, 256), draw_splatmap, ScaleMode.ScaleToFit, false, 1);
    //}
}
