using System.Collections;
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
    float axisXOffset = 0;
    float axisYOffset = 0;
    bool setOffset = true;

    void Update()
    {
        if (canMove)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            

            //rotationY = cameraRotator.transform.localEulerAngles.y;
            rotationY += (Input.GetAxis("Mouse Y")) * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            cameraRotator.transform.localEulerAngles = new Vector3(-rotationY, cameraRotator.transform.localEulerAngles.y, cameraRotator.transform.localEulerAngles.z);
            transform.localEulerAngles = new Vector3(0, rotationX, 0);
            setOffset = false;
        } 
        else
        {
            if(!setOffset)
            {
                transform.localEulerAngles = Vector3.zero;
                rotationY = 0;
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                axisXOffset = Input.GetAxis("Mouse X");
                axisYOffset = Input.GetAxis("Mouse Y");
                Debug.Log(axisXOffset);
                setOffset = true;
            }
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
