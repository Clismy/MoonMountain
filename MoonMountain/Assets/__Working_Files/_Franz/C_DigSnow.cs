using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_DigSnow : MonoBehaviour
{
    public Transform hand;

    public Animation dig;

    //  public GameObject  _terrain;
    public C_Chungk2 walk_Curent_terrain;
    public Shader _drawShader;
    [Range(1, 500)]
    public float _brushSize;
    [Range(0, 1)]
    public float _brushStrenght;

    private RaycastHit _hit;
    private RaycastHit[] _hit_;
    private int      _layermask;
    private Material _snowMaterial;
    private Material      _drawMaterial;
    public RenderTexture  walk_splatmap;
    private Camera        _camera;

    private Ray digRay;
    public float radius;
    public float dist;


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

            if (!dig.IsPlaying("Gräv"))
                dig.Play();


            if (_camera == null)
                _camera = Camera.main;



        }
    }

    public void DigNow()
    {
      //  _camera.ScreenPointToRay(Input.mousePosition)
        digRay = new Ray(_camera.transform.position , _camera.transform.forward);//new Ray(hand.position- _camera.transform.forward*dist*0.5f, _camera.transform.forward);


        Physics.SphereCast(digRay, radius, dist, _layermask);
        _hit_ = Physics.SphereCastAll(digRay, radius, dist, _layermask);
        // if (Physics.SphereCastAll(digRay, radius, out _hit, dist, _layermask))

        foreach(RaycastHit rh in _hit_)
        {

       
           C_Chunk3 c3 = rh.transform.gameObject.GetComponent("C_Chunk3") as C_Chunk3;

            if (c3 != null)
            {
                C_TerrainModifyer ter = c3.snoPileGo.transform.gameObject.GetComponent("C_TerrainModifyer") as C_TerrainModifyer;
                ter.ReplaceBlockAt(rh, 0);
            }

            C_Chungk2 c2 = rh.transform.gameObject.GetComponent("C_Chungk2") as C_Chungk2;
            if (c2 != null)
            {

                if (walk_Curent_terrain != null && walk_Curent_terrain.gameObject != c2.gameObject)
                    walk_Curent_terrain.getSetSplatMap = walk_splatmap;

                walk_Curent_terrain = c2;
                _snowMaterial       = c2.gameObject.GetComponent<MeshRenderer>().material;
                _snowMaterial.SetTexture("_Splat", walk_Curent_terrain.getSetSplatMap);
                walk_splatmap       = walk_Curent_terrain.getSetSplatMap;

                _drawMaterial.SetVector("_Coordinate", new Vector4(rh.textureCoord.x, rh.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_Strenght", _brushStrenght);
                _drawMaterial.SetFloat("_Size", _brushSize);


                RenderTexture temp = RenderTexture.GetTemporary(walk_splatmap.width, walk_splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(walk_splatmap, temp);
                Graphics.Blit(temp, walk_splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }



               




        }





    }
    private void OnDrawGizmos()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            for (float i = 0; i < dist; i += 0.1f)
            {
                Gizmos.DrawWireSphere(digRay.GetPoint(i), radius);
            }



        }


    }

}
