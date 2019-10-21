using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public GameObject playerCamera;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;
    void Update()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        playerCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        transform.localEulerAngles = new Vector3(0, rotationX, 0);
    }

    void Start()
    {
        // Make the Rigidbody not change rotation
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody)
            rigidbody.freezeRotation = true;
    }
}
