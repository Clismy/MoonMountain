using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_drawWithMouse : MonoBehaviour
{



    public Camera _camera;
    public Shader _drawShader;
    [Range(1,500)]
    public float  _brushSize;
    [Range(0,1)]
    public float  _brushStrenght;


    private RenderTexture _splatmap;
    private Material      _snowMaterial;
    private Material      _drawMaterial;
    private RaycastHit    _hit;


    void Start()
    {
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);

        _snowMaterial = GetComponent<MeshRenderer>().material;
        _splatmap     = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);

        _snowMaterial.SetTexture("_Splat", _splatmap);
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out _hit))
            {
                _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_Strenght"   , _brushStrenght);
                _drawMaterial.SetFloat("_Size"       , _brushSize);


                RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width,_splatmap.height,0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, temp);
                Graphics.Blit(temp, _splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);

            }
        }
        
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), _splatmap, ScaleMode.ScaleToFit, false, 1);
    }
}
