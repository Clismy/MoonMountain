using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class PlayerSettings : MonoBehaviour
{
    [Header("CrouchSettings")]
    public KeyCode crouchKey = KeyCode.LeftControl;
    public GameObject cameraHeightPos;
    public Camera cameraForwardPos;
    public float crouchHeight;
    public float crouchForward;
    public bool doOnce = true;

    [SerializeField] float standingHeight;
    [SerializeField] float crounchingHeight;

    [Space]
    [SerializeField] float realStandingHeight;
    [SerializeField] float realCrounchingHeight;

    [Space]
    [SerializeField] float standingOffset;
    [SerializeField] float crounchingOffset;

    [Space]
    [SerializeField] LayerMask ignorePlayer;
    [SerializeField] float maxDistance;

    [Space]
    public float ceilingAngleLimit = 10f;

    Vector3 heightVec;
    Vector3 camVec;
    Vector3 camVecOrg;
    float height;
    float offset = 0f;
    float cCHeight = 0f;

    float cruncheCamerOffset = 0f; 

    float originalJumpSpeed;
    float newJumpingSpeed;

    [Space]
    [SerializeField] float speed;

    public bool crounhing = false;

    Mover mover;
    AdvancedWalkerController aW;
    CapsuleCollider cC;

    private void Awake()
    {
        camVecOrg = new Vector3(0, 0, cameraForwardPos.transform.position.z);
    }

    void Start()
    {
        mover = GetComponent<Mover>();
        aW = GetComponent<AdvancedWalkerController>();
        cC = GetComponent<CapsuleCollider>();

        crounhing = false;

        originalJumpSpeed = aW.jumpSpeed;
        heightVec = gameObject.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            crounhing = !crounhing;
        }

        if (CheckForCeleing())
        {
            crounhing = true;
        }

        float h = crounhing ? crounchingHeight : standingHeight;
        height = Mathf.MoveTowards(height, h, Time.deltaTime * speed);
        mover.colliderHeight = height;

        //if (crounhing)
        //{
        //    if (doOnce)
        //    {
        //        camVecOrg = new Vector3(0, 0, Camera.main.transform.position.z);
        //        camVec = Camera.main.transform.position;
        //        doOnce = false;
        //    }
        //
        //
        //    cameraHeightPos.transform.position = new Vector3(cameraHeightPos.transform.position.x, cameraHeightPos.transform.position.y - crouchHeight, cameraHeightPos.transform.position.z);
        //    //cameraForwardPos.transform.position = camVec;
        //}
        //else
        //{
        //    cameraHeightPos.transform.position = new Vector3(cameraHeightPos.transform.position.x, cameraHeightPos.transform.position.y, cameraHeightPos.transform.position.z);
        //    //cameraForwardPos.transform.position = camVecOrg;
        //}


//========================================================================================================================
 //Franz gjorde denna ändraing för att övergången mellan crunch och stand skulle bli mjukare        if (crounhing)
        if (doOnce)
            {
                camVecOrg = new Vector3(0, 0, Camera.main.transform.position.z);
                camVec = Camera.main.transform.position;
                doOnce = false;
            }
     

        float ccy = (crounhing ? crouchHeight : 0);
        cruncheCamerOffset = Mathf.MoveTowards(cruncheCamerOffset, ccy, Time.deltaTime * speed);
        cameraHeightPos.transform.position = new Vector3(cameraHeightPos.transform.position.x, cameraHeightPos.transform.position.y - cruncheCamerOffset, cameraHeightPos.transform.position.z);
//======================================================================================================================== 
       
        //Vector3 h1 = crounhing ? new Vector3(cameraHeightPos.transform.position.x, gameObject.transform.position.y + crounchingHeight, cameraHeightPos.transform.position.z) :
        //    new Vector3(cameraHeightPos.transform.position.x, gameObject.transform.position.y + standingHeight, cameraHeightPos.transform.position.z);
        //heightVec = Vector3.MoveTowards(heightVec, h1, Time.deltaTime * speed);
        //cameraHeightPos.transform.position = heightVec;

        float o = crounhing ? crounchingOffset : standingOffset;
        offset = Mathf.MoveTowards(offset, o, Time.deltaTime * speed);
        mover.colliderOffset.y = offset;

        float ccH = crounhing ? realCrounchingHeight : realStandingHeight;
        cCHeight = Mathf.MoveTowards(cCHeight, ccH, Time.deltaTime * speed);
        cC.height = cCHeight;

        aW.jumpSpeed = crounhing ? 0f : originalJumpSpeed;
    }

    bool CheckForCeleing()
    {
        RaycastHit[] hit = Physics.RaycastAll(Camera.main.transform.position, Vector3.up, maxDistance, ignorePlayer);

        Debug.DrawRay(Camera.main.transform.position, Vector3.up * maxDistance);
        float _angle = 0f;

        for (int i = 0; i < hit.Length; i++)
        {
            //Calculate angle between hit normal and character;
            _angle = Vector3.Angle(-transform.up, hit[i].normal);

            //If angle is smaller than ceiling angle limit, register ceiling hit;
            if (_angle < ceilingAngleLimit)
                return true;
        }

        return false;
    }
}