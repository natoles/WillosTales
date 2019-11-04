using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionnableGameObject : MonoBehaviour
{
    public MovingPlatform target;   // MovingPlatform to move
    public Transform playerPos;
    [SerializeField, Range(0f, 5f)]
    private float activationMaxDist = 2f;
    public bool reversible;         // Can the movingplatform get activated and disactivated ?
    private bool targetInitialState;// Initial state of the target (bool), only used for not-reversable object

    private void Start()
    {
        targetInitialState = target.activated;
    }


    void OnMouseDown()
    {
        if (Vector3.Distance(playerPos.position, transform.position) < activationMaxDist) //If player too far, can't activate
        {
            activateObject();
        }
        
    }

    public void activateObject()
    {
        if(reversible)
        {
            target.activate(!target.activated);
        }
        else
        {
            target.activate(!targetInitialState);
        }
    }
}
