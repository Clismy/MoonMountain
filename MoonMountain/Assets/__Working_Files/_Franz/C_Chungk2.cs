using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Chungk2 : MonoBehaviour
{
    public Material baseMaterial;

    public Shader snow_tack_shader;
    
    private MeshCollider col;


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



    

 

   
}
