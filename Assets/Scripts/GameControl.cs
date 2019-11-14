using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameControl : MonoBehaviour
{
    public GameObject cam;
    public GameObject camRot;
    public GameObject playerTP;
    public GameObject soulLink;
    public GameObject playerFP;
    public GameObject postProcessVolume;
    public GameObject menu;

    string tpKey = "k";
    string changeKey = "l"; //Key to change mode
    bool isSoulMode = true; //True : soul mode, False : player mode 
    float maxSoulDist = 10f; //max distance between soul and player

    TPMouseMovement TPMouse;
    TPMovement TPMove;
    FirstPersonController FPController;

    

    void Start()
    {
        TPMouse = playerTP.GetComponent<TPMouseMovement>();
        TPMove  = playerTP.GetComponent<TPMovement>();
        FPController = playerFP.GetComponent<FirstPersonController>();

        Physics.IgnoreCollision(playerFP.GetComponent<CharacterController>(), playerTP.GetComponent<Collider>());

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

        if (isSoulMode && Vector3.Distance(playerFP.transform.position, playerTP.transform.position) > maxSoulDist)
        {
            Debug.Log("Soul is too far !");
            ModeChangerHandler();
        }

        #region teleportation
        if (Input.GetKeyDown(tpKey) && isSoulMode)
        {
            playerFP.transform.position = playerTP.transform.position;
            FPController.isTP = true;
            Debug.Log("TP");
            ModeChangerHandler();
        }
        #endregion
        Debug.Log(playerFP.transform.position);
        Debug.Log(playerTP.transform.position);
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
                FPController.canMove = false;
            }
            else
            {
                TPMouse.canMove = TPMove.canMove = false;
                FPController.canMove = true;
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
            FPController.canMove = false;

        }
    }

    void ModeChangerHandler()
    {
        Debug.Log("Changement mode");
        if (isSoulMode)
        {
            //Enters in FP 
            playerTP.SetActive(false);
            soulLink.SetActive(false);
            postProcessVolume.SetActive(false);

            cam.transform.parent = playerFP.transform;
            cam.transform.localPosition = new Vector3(0.1f, 1f, 0.15f); //Local position of the camera

            TPMouse.canMove = false;
            TPMove.canMove = false;
            FPController.canMove = true;
        }
        else
        {
            //Enters in TP 
            playerTP.SetActive(true);
            soulLink.SetActive(true);
            postProcessVolume.SetActive(true);
            playerTP.transform.position = playerFP.transform.position + playerFP.transform.forward * 2 ; //Soul spawn position

            cam.transform.parent = camRot.transform;
            cam.transform.localPosition = new Vector3(0, 1, -8f);
            
            playerTP.transform.localEulerAngles = playerFP.transform.localEulerAngles;
            cam.transform.localEulerAngles = Vector3.zero;

            TPMouse.canMove = true;
            TPMove.canMove = true;
            FPController.canMove = false;
        }
        isSoulMode = !isSoulMode;
    }
}
