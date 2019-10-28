using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    Rigidbody playerRb;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
        playerRb.useGravity = false;
    }

    void FixedUpdate()
    {


        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Depth"), Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = playerRb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        playerRb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
