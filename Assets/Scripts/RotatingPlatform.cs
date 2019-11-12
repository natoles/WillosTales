using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public Transform rotatingPlatform;      // Rotation of current objet
    public Transform rotation1;             // First rotation to reach
    public Transform rotation2;             // Second rotation to reach
    public float rotationSpeed;             // Speed of the movement
    public bool activated = true;           // Is activated. If not, gameobject's not moving
    public bool uniqueMove = false;         // Can the platform travel backward ?
    private bool moveReached = false;       // Only for uniqueMove-mode. Activated when the movement is complete
    public bool repeatable = true;          // Can the platform be triggered again (to move backward for example)

    // Update is called once per frame
    void Update()
    {
        if (activated && !moveReached)
        {
            // Translation init
            Vector3 newRotation = rotatingPlatform.rotation.eulerAngles;
            float distanceTravelled = (newRotation - rotation1.rotation.eulerAngles).magnitude;
            float totalDistance = (rotation1.rotation.eulerAngles - rotation2.rotation.eulerAngles).magnitude;

            // If distance reached, then swap direction
            if (distanceTravelled >= totalDistance)
            {
                Transform temp = rotation2;
                rotation2 = rotation1;
                rotation1 = temp;

                if (repeatable)
                {
                    activated = false;
                }
                if (uniqueMove)
                {
                    moveReached = true;
                }
            }

            // Move while the distance isn't reached
            Vector3 direction = Vector3.Normalize(rotation2.position - rotation1.position);
            newRotation += (Vector3)(rotationSpeed * Time.deltaTime * direction);
            rotatingPlatform.position = newRotation;
        }
    }
    public void activate(bool predicate)
    {
        activated = predicate;
    }

}
