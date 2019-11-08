﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject cam; //camera rotator
    public GameObject playerTP;
    public GameObject playerFP;
    public GameObject postProcessVolume;
    public GameObject menu;
    LineRenderer soulLink;
    string changeKey = "l"; //Key to change mode
    bool isSoulMode = true; //True : soul mode, False : player mode 
    float maxSoulDist = 10f; //max distance between soul and player

    TPMouseMovement TPMouse;
    TPMovement TPMove;
    FPMouseMovement FPMouse;
    FPMovement FPMove;

    

    void Start()
    {
        TPMouse = playerTP.GetComponent<TPMouseMovement>();
        TPMove  = playerTP.GetComponent<TPMovement>();
        FPMouse = playerFP.GetComponent<FPMouseMovement>();
        FPMove  = playerFP.GetComponent<FPMovement>();
        soulLink = playerTP.GetComponent<LineRenderer>();

        Physics.IgnoreCollision(playerFP.GetComponent<Collider>(), playerTP.GetComponent<Collider>());

        ModeChangerHandler();
    }

    // Update is called once per frame
    void Update()
    {
        // Open or close the menu if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenCloseMenu();
        
        // If the menu is open, don't execute the rest
        if (menu.activeSelf) return;
        
        // Switch between FP and TP view if the L key is pressed
        if (Input.GetKeyDown(changeKey))
            ModeChangerHandler();

        soulLink.SetPosition(0, playerFP.transform.position);
        soulLink.SetPosition(1, playerTP.transform.position);

        if (isSoulMode && Vector3.Distance(playerFP.transform.position, playerTP.transform.position) > maxSoulDist)
        {
            Debug.Log("Soul is too far !");
            ModeChangerHandler();
        }
    }
    
    public void OpenCloseMenu()
    {
        menu.SetActive(!menu.activeSelf);
        if (!menu.activeSelf) 
        {
            // Exit the menu
            Debug.Log("Closing the menu.");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (isSoulMode)
            {
                TPMouse.canMove = TPMove.canMove = true;
                FPMouse.canMove = FPMove.canMove = false;
            }
            else
            {
                TPMouse.canMove = TPMove.canMove = false;
                FPMouse.canMove = FPMove.canMove = true;
            }
        }
        else 
        {
            // Open the menu
            Debug.Log("Opening the menu.");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            TPMouse.canMove = false;
            TPMove.canMove = false;
            FPMouse.canMove = false;
            FPMove.canMove = false;
        }
    }

    void ModeChangerHandler()
    {
        Debug.Log("Changement mode");
        if (isSoulMode)
        {
            //Enters in FP 
            playerTP.SetActive(false);
            postProcessVolume.SetActive(false);

            cam.transform.parent = playerFP.transform;
            cam.transform.localPosition = new Vector3(0, 0.4f, 0.2f);

            TPMouse.canMove = false;
            TPMove.canMove = false;
            FPMouse.canMove = true;
            FPMove.canMove = true;
        }
        else
        {
            //Enters in TP 
            playerTP.SetActive(true);
            postProcessVolume.SetActive(true);
            playerTP.transform.position = playerFP.transform.position + playerFP.transform.forward * 2 ; //Soul spawn position

            cam.transform.parent = playerTP.transform.GetChild(0).transform;
            cam.transform.localPosition = new Vector3(0, 1, -8f);
            
            playerTP.transform.localEulerAngles = playerFP.transform.localEulerAngles;
            cam.transform.localEulerAngles = Vector3.zero;

            TPMouse.canMove = true;
            TPMove.canMove = true;
            FPMouse.canMove = false;
            FPMove.canMove = false;
        }
        isSoulMode = !isSoulMode;
    }
}
