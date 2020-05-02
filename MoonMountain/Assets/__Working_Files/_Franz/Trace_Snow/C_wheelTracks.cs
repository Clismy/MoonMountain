using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_wheelTracks : MonoBehaviour
{
    public GameObject  _terrain;
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
    private RenderTexture _splatmap;

    void Start()
    {
        _layermask = LayerMask.GetMask("Floor");
        _drawMaterial = new Material(_drawShader);

        _snowMaterial = _terrain.GetComponent<MeshRenderer>().material;
        _snowMaterial.SetTexture("_Splat", _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
    }


    // Update is called once per frame
    void Update()
    {
        for(int i = 0, lengt = _wheel.Length; i < lengt; i++)
        {
            if (Physics.Raycast(_wheel[i].position, -Vector3.up, out _hit, 1f, _layermask))
            {
                _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat ("_Strenght"  , _brushStrenght);
                _drawMaterial.SetFloat ("_Size"      , _brushSize);


                RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, temp);
                Graphics.Blit(temp, _splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);

            }
        }
        
    }
}
