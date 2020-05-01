using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    [SerializeField] bool isGrounded = false;
    [SerializeField] float raycastOffsetY;
    [SerializeField, Range(0, 5)] float maxDistance;
    [SerializeField] LayerMask groundLayerMask;

    [SerializeField] Vector3 collisionOffset;
    [SerializeField] float circleSize;

    void Update()
    {
        isGrounded = CheckGrounded();
    }

    bool CheckGrounded()
    {
        Vector3 newOffset = transform.position + collisionOffset;

        var hit = Physics.OverlapSphere(newOffset, circleSize, groundLayerMask);

        for (int i = 0; i < hit.Length; i++)
        {
            return true;
        }
        return false;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    void OnDrawGizmos()
    {
        Vector3 newOffset = transform.position + collisionOffset;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(newOffset, circleSize);
    }
}