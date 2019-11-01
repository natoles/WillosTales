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
            Vector3 newPosition = movingPlatform.position;
            float distanceTravelled = (newPosition - position1.position).magnitude;
            float totalDistance = (position1.position - position2.position).magnitude;

            if (distanceTravelled >= totalDistance)
            {
                Transform temp = position2;
                position2 = position1;
                position1 = temp;

                if(uniqueMove)
                {
                    reachMove();
                }

                if (!repeated)
                {
                    activated = !activated;
                }
            }

            Vector3 direction = Vector3.Normalize(position2.position - position1.position);
            newPosition += (Vector3)(movingSpeed * Time.deltaTime * direction);
            movingPlatform.position = newPosition;

            if (canRotate)
            {
                movingPlatform.rotation = Quaternion.Lerp(movingPlatform.rotation, position2.rotation, rotationSpeed * Time.deltaTime);
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
