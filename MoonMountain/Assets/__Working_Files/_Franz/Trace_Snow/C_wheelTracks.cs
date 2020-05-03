using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_wheelTracks : MonoBehaviour
{
  //  public GameObject  _terrain;
    public C_Chunk  Curent_terrain;
    public Transform[] _wheel;
    public Shader      _drawShader;
    [Range(1, 500)]
    public float _brushSize;
    [Range(0, 1)]
    public float _brushStrenght;

    private RaycastHit     _hit;
    private int           _layermask;
    private Material      _snowMaterial;
    private Material      _drawMaterial;
    public RenderTexture _splatmap;
   
    void Start()
    {
        _layermask = LayerMask.GetMask("Floor");
        _drawMaterial = new Material(_drawShader);

       // _snowMaterial = _terrain.GetComponent<MeshRenderer>().material;
       // _snowMaterial.SetTexture("_Splat", _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
    }


    // Update is called once per frame
    void Update()
    {
        //for(int i = 0, lengt = _wheel.Length; i < lengt; i++)
        //{
        //    if (Physics.Raycast(_wheel[i].position, -Vector3.up, out _hit, 1f, _layermask))
        //    {

        //        if(Curent_terrain == null)
        //        {
        //            Curent_terrain = _hit.transform.gameObject.GetComponent("C_Chunk") as C_Chunk;
        //            _snowMaterial = _hit.transform.gameObject.GetComponent<MeshRenderer>().material;
        //            _snowMaterial.SetTexture("_Splat", Curent_terrain.getSetSplatMap);
        //            _splatmap = Curent_terrain.getSetSplatMap;

        //        }
        //        else if(Curent_terrain.gameObject != _hit.transform.gameObject)
        //        {

        //            Curent_terrain.getSetSplatMap = _splatmap;


        //            Curent_terrain = _hit.transform.gameObject.GetComponent("C_Chunk") as C_Chunk;
        //            _snowMaterial  = Curent_terrain.gameObject.GetComponent<MeshRenderer>().material;
        //            _snowMaterial.SetTexture("_Splat", Curent_terrain.getSetSplatMap);
        //            _splatmap = Curent_terrain.getSetSplatMap;
        //        }
             

    


       




        //        _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
        //        _drawMaterial.SetFloat ("_Strenght"  , _brushStrenght);
        //        _drawMaterial.SetFloat ("_Size"      , _brushSize);


        //        RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
        //        Graphics.Blit(_splatmap, temp);
        //        Graphics.Blit(temp, _splatmap, _drawMaterial);
        //        RenderTexture.ReleaseTemporary(temp);

        //    }
        //}
        
    }




    private void OnGUI()
    {
        if(_splatmap != null)
        GUI.DrawTexture(new Rect(0, 0, 256, 256), _splatmap, ScaleMode.ScaleToFit, false, 1);
    }
}
