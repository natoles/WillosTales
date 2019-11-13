using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate2Control : MonoBehaviour
{
    // Start is called before the first frame update
    public PressurePlate pP1;
    public PressurePlate pP2;
    public MovingPlatform gate;
    public RotatingPlatform rotatingGate;
    private bool gateInitialState;
    private bool rotatingGateInitialState;
    public bool reversible;         // Can the movingplatform get activated and disactivated ?

    void Start()
    {
        if(gate != null)
            gateInitialState = gate.activated;
        if (rotatingGate != null)
            rotatingGateInitialState = rotatingGate.activated;
    }

    // Update is called once per frame
    void Update()
    {
        if (pP1.active && pP2.active)
        {
            if (reversible)
            {
                if(gate!=null)
                    gate.activate(!gate.activated);
                if (rotatingGate != null)
                    rotatingGate.activate(!gate.activated);
            }
            else
            {
                if (gate != null)
                    gate.activate(!gateInitialState);
                if (rotatingGate != null)
                    rotatingGate.activate(!rotatingGateInitialState);
            }
        }
    }
}
