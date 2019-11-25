using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulWorldToggle : MonoBehaviour
{
    public GameControl GC;

    private bool previousIsSoulMode;

    // Start is called before the first frame update
    void Start()
    {
        previousIsSoulMode = !GC.isSoulMode;

        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        Debug.Log("Nb Child of Env: " + allChildren.Length);

        if (!GC.isSoulMode) //fix de merde parce que le joueur pop en mode âme par défaut
        {
            foreach (Transform child in allChildren)
            {
                if (child.CompareTag("SoulView"))
                {
                    Debug.Log(child.name);
                    child.gameObject.SetActive(true);
                }
                if (child.CompareTag("NormalView"))
                {
                    Debug.Log(child.name);
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Transform child in allChildren)
            {
                if (child.CompareTag("SoulView"))
                {
                    Debug.Log(child.name);
                    child.gameObject.SetActive(false);
                }
                if (child.CompareTag("NormalView"))
                {
                    Debug.Log(child.name);
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GC.isSoulMode != previousIsSoulMode)
        {
            previousIsSoulMode = GC.isSoulMode;

            SetWorldVisibility();
        }
    }

    private void SetWorldVisibility()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        Debug.Log("Nb Child of Env: " + allChildren.Length);

        if (GC.isSoulMode)
        {
            foreach (Transform child in allChildren)
            {
                if (child.CompareTag("SoulView"))
                {
                    Debug.Log(child.name);
                    child.gameObject.SetActive(true);
                }
                if (child.CompareTag("NormalView"))
                {
                    Debug.Log(child.name);
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Transform child in allChildren)
            {
                if (child.CompareTag("SoulView"))
                {
                    Debug.Log(child.name);
                    child.gameObject.SetActive(false);
                }
                if (child.CompareTag("NormalView"))
                {
                    Debug.Log(child.name);
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
