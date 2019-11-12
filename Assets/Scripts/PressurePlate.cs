using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    Collider pressureZone;
    public bool active = false;
    public LayerMask activeLayers;

    // Start is called before the first frame update
    void Start()
    {
        pressureZone = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (activeLayers == (activeLayers | (1 << col.gameObject.layer)))
        {
            active = true;
        }
        FindObjectOfType<AudioManager>().Play("Switch");
    }

    private void OnTriggerExit(Collider col)
    {
        if (activeLayers == (activeLayers | (1 << col.gameObject.layer)))
        {
            active = false;
        }
        FindObjectOfType<AudioManager>().Play("Switch");
    }
}
