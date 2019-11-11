﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;
    Rigidbody playerRb;
    public bool canMove;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
        playerRb.useGravity = false;
        FindObjectOfType<AudioManager>().Play("Walking");
    }

    void FixedUpdate()
    {
        
        if (canMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = playerRb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            playerRb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Walking sounds
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                FindObjectOfType<AudioManager>().UnPause("Walking");
            }
            else
            {
                FindObjectOfType<AudioManager>().Pause("Walking");
            }

            if (grounded)
            {
                // Jump
                if (canJump && Input.GetButton("Jump"))
                {
                    playerRb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                }

                grounded = false;
            }
        }

        // We apply gravity manually for more tuning control
        playerRb.AddForce(new Vector3(0, -gravity * playerRb.mass, 0));

    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
