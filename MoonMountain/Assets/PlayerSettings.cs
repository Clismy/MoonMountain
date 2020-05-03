using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class PlayerSettings : MonoBehaviour
{
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

    float height = 0f;
    float offset = 0f;
    float cCHeight = 0f;

    float originalJumpSpeed;
    float newJumpingSpeed;

    [Space]
    [SerializeField] float speed;

    bool crounhing = false;

    Mover mover;
    AdvancedWalkerController aW;
    CapsuleCollider cC;

    void Start()
    {
        mover = GetComponent<Mover>();
        aW = GetComponent<AdvancedWalkerController>();
        cC = GetComponent<CapsuleCollider>();

        crounhing = false;

        originalJumpSpeed = aW.jumpSpeed;
    }

    void Update()
    {
        if (Input.GetButtonDown("Crounching"))
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