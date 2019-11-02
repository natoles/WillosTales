using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform movingPlatform;    // Position of current objet
    public Transform position1;         // First position to reach
    public Transform position2;         // Third position to reach
    public float movingSpeed;           // Speed of the movement
    public float rotationSpeed;         // Speed of the rotation
    public bool activated = true;       // Is activated. If not, gameobject's not moving
    public bool repeated = true;        // Is the platform moving repeatedly ?
    public bool canRotate = false;      // Can the platform rotate ?
    public bool uniqueMove = false;     // Can the platform go back to its first position ?
    private bool moveReached = false;   // Only for uniqueMove-mode. Activated when the movement is complete

    // Update is called once per frame
    void Update()
    {
        if (activated && !moveReached)
        {
            // Translation init
            Vector3 newPosition = movingPlatform.position;
            float distanceTravelled = (newPosition - position1.position).magnitude;
            float totalDistance = (position1.position - position2.position).magnitude;

            // Rotation init
            Vector3 newRotation;
            float rotationTravelled = 0;
            float totalRotation = 0;
            if (canRotate)
            {
                newRotation = movingPlatform.rotation.eulerAngles;
                rotationTravelled = (newRotation - position1.rotation.eulerAngles).magnitude;
                totalRotation = (position1.rotation.eulerAngles - position2.rotation.eulerAngles).magnitude;
            }

            // If distance reached, then swap direction
            if (distanceTravelled >= totalDistance && (!canRotate || rotationTravelled >= totalRotation))
            {
                Transform temp = position2;
                position2 = position1;
                position1 = temp;

                if (uniqueMove)
                {
                    reachMove();
                }

                if (!repeated)
                {
                    activated = !activated;
                }
            }
            // Else let's continue to move
            else
            {
                // Move while the distance isn't reached
                if(distanceTravelled <= totalDistance)
                {
                    Vector3 direction = Vector3.Normalize(position2.position - position1.position);
                    newPosition += (Vector3)(movingSpeed * Time.deltaTime * direction);
                    movingPlatform.position = newPosition;
                }
                // Rotate while rotation isn't reached
                if(canRotate && rotationTravelled <= totalRotation)
                {
                    movingPlatform.rotation = Quaternion.RotateTowards(movingPlatform.rotation, position2.rotation, movingSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void reachMove()
    {
        moveReached = true;
    }

    public void activate(bool predicate)
    {
        activated = predicate;
    }
}
