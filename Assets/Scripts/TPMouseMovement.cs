﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPMouseMovement : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject cameraRotator;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;
    public bool canMove;
    void Update()
    {
        if (canMove)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            //rotationY = cameraRotator.transform.localEulerAngles.y;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            cameraRotator.transform.localEulerAngles = new Vector3(-rotationY, cameraRotator.transform.localEulerAngles.y, cameraRotator.transform.localEulerAngles.z);
            transform.localEulerAngles = new Vector3(0, rotationX, 0);
        }
    }

    void Start()
    {
        // Make the Rigidbody not change rotation
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody)
            rigidbody.freezeRotation = true;
    }
}
