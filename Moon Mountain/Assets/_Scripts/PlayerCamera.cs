using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float mouseSensitivity;

    float verticalRotation;
    float horizontalRotation;
    [SerializeField] float upDownRange;

    void Update()
    {
        MouseLook();
    }

    void MouseLook()
    {
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, rotLeftRight, 0f);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, Camera.main.transform.localRotation.z);
    }

    public void SetRot(float verti)
    {
        verticalRotation = 0;
    }
}