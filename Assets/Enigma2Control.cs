using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigma2Control : MonoBehaviour
{
    public ButtonWithOrder button1;
    public GameObject bridge;
    public Transform repairedBridgePos;
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button1.hasWon)
        {
            bridge.transform.position = repairedBridgePos.position;
            wall.SetActive(false);
        }
    }
}
