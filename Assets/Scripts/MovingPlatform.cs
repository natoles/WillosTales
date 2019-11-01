using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform movingPlatform;    // Position of current objet
    public Transform position1;         // First position to reach
    public Transform position2;         // Third position to reach
    public float Speed;                 // Speed
    public bool activated = true;       // Is activated. If not, gameobject's not moving
    public bool repeated = true;        // Is the platform moving repeatedly ?
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
            newPosition += (Vector3)(Speed * Time.deltaTime * direction);
            movingPlatform.position = newPosition;
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
