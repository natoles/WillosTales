using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public static string POS1 = "Moving to Position 1";
    public static string POS2 = "Moving to Position 2";



    public Transform movingPlatform;    // Position of current objet
    public Transform position1;         // First position to reach
    public Transform position2;         // Third position to reach
    public float Speed;                 // Speed
    private string currentState;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = movingPlatform.position;
        float distanceTravelled = (newPosition - position1.position).magnitude;
        float totalDistance = (position1.position - position2.position).magnitude;

        if(distanceTravelled >= totalDistance)
        {
            Transform temp = position2;
            position2 = position1;
            position1 = temp;
        }

        Vector3 direction = Vector3.Normalize(position2.position - position1.position);
        newPosition += (Vector3)(Speed * Time.deltaTime * direction);
        movingPlatform.position = newPosition;
    }

    void ChangeTarget()
    {

    }
}
