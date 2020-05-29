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
    [Range(0.000001f, 10)]
    public float _brushSize;
    [Range(0, 1)]
    public float _brushStrenght;

    private RaycastHit _hit;
    private int _layermask;
    private Material _snowMaterial;
    private Material _drawMaterial;
    public RenderTexture walk_splatmap;
    private Camera _camera;


    void Start()
    {
        _layermask = LayerMask.GetMask("SnowFloor");
        _drawMaterial = new Material(_drawShader);
    }


    // Update is called once per frame
    void Update()
    {
     

     
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
