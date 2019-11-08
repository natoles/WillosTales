using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate2Control : MonoBehaviour
{
    // Start is called before the first frame update
    public PressurePlate pP1;
    public PressurePlate pP2;
    public MovingPlatform gate;
    private bool gateInitialState;
    public bool reversible;         // Can the movingplatform get activated and disactivated ?

    void Start()
    {
        gateInitialState = gate.activated;
    }

    // Update is called once per frame
    void Update()
    {
        if (pP1.active && pP2.active)
        {
            if (reversible)
            {
                gate.activate(!gate.activated);
            }
            else
            {
                gate.activate(!gateInitialState);
            }
        }
    }
}
