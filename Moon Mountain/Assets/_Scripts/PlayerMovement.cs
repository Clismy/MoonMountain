using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float acceleration, deAcceleration;

    bool stopMoving = false;
    bool spriting = false;

    [Space]
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Vector3 bodyOffset;
    [SerializeField] float distanceToWall;
    [SerializeField] float startY, endY;

    Vector3 input;
    Rigidbody rb;
    PlayerGround pG;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pG = GetComponent<PlayerGround>();
    }

    void Update()
    {
        spriting = Input.GetKey(KeyCode.LeftShift) && pG.IsGrounded() || spriting && !pG.IsGrounded() ? true : false;

        if (!pG.IsGrounded())
        {
            spriting = false;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 beforeInput = new Vector3(horizontal, vertical);

        if (stopMoving)
        {
            beforeInput = Vector2.zero;
        }

        beforeInput = new Vector2(Check(beforeInput.x, new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z)), Check(beforeInput.y, new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z)));

        input = new Vector3(GetAcceleratedInput(beforeInput.x, input.x), 0f, GetAcceleratedInput(beforeInput.y, input.z));

        float speed = spriting ? runSpeed : movementSpeed;
        Vector3 moveDirection = input.normalized * speed;

        moveDirection = Camera.main.transform.rotation * moveDirection;
        moveDirection.y = rb.velocity.y;

        rb.velocity = moveDirection;
    }

    float GetAcceleratedInput(float newInput, float oldInput)
    {
        if (Mathf.Abs(newInput) > 0)
        {
            oldInput = Mathf.MoveTowards(oldInput, newInput, acceleration * Time.deltaTime);
        }
        else
        {
            oldInput = Mathf.MoveTowards(oldInput, 0f, deAcceleration * Time.deltaTime);
        }

        return oldInput;
    }

    int SetDirection(float value, float input)
    {
        int direction = 0; //Make A Variable That Will Store The Direction Of The Player, Depending On Where They Press
        if (value > 0) //If The Player Pressed "Right"
        {
            direction = 1;
        }
        else if (value < 0) //If The Player Pressed "Left"
        {
            direction = -1;
        }

        if (stopMoving == true)
        {
            direction = 0;
            input = 0;
        }

        return direction;
    }

    float Check(float inp, Vector3 camDir)
    {
        RaycastHit hit;
        Vector3 d = inp > 0 ? camDir : -camDir;

        if (inp == 0)
            return 0;

        for (float i = startY; i < endY; i += 0.1f)
        {
            Debug.DrawRay(transform.position + Vector3.up * i, d * distanceToWall, Color.yellow);

            if (Physics.Raycast(transform.position + Vector3.up * i, d, out hit, distanceToWall, wallLayer))
            {
                if (hit.normal != Vector3.right || hit.normal != Vector3.forward)
                {
                    return inp;
                }
                return 0;
            }
        }
        return inp;
    }
}