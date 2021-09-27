using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject follwingTarget;
    [SerializeField]
    private Vector3 offset  = new Vector3(0,5,-5);
    public float distance;

    private Vector3 relativePos;
    private float horizontalAngle;
    [SerializeField]
    public float verticalAngle;

    [SerializeField]
    private float rerex;
    [SerializeField]
    private float rerey;
    [SerializeField]
    private Vector3 lookOffSet;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        offset = new Vector3(0.5f, 1.5f, 0);
        verticalAngle = 45;
        distance = 5;
        relativePos = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * new Vector3(0, 0, -distance);
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Rotate(mouseX * rerex, mouseY * rerey);

        transform.position = follwingTarget.transform.position + offset + relativePos;
        transform.LookAt(follwingTarget.transform.position + transform.TransformDirection(lookOffSet));
    }

    private void Rotate(float anglex, float angley)
    {
        horizontalAngle += anglex;
        verticalAngle = Mathf.Clamp(verticalAngle - angley,10,80);
        relativePos = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * new Vector3(0, 0, -distance);
    }
}
