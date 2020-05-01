using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;

    bool desiredJump = false;

    Rigidbody rb;
    PlayerGround pG;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pG = GetComponent<PlayerGround>();
    }

    void Update()
    {
        desiredJump |= Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        if (desiredJump)
        {
            Jump();
            desiredJump = false;
        }
    }

    void Jump()
    {
        if (pG.IsGrounded())
        {
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            rb.velocity += Vector3.up * jumpSpeed;
        }
    }

    public void AddJump()
    {
        float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        rb.velocity += Vector3.up * jumpSpeed * 2f;
    }
}