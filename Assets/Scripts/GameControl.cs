using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject cam; //camera rotator
    public GameObject playerTP;
    public GameObject playerFP;
    string changeKey = "l"; //Key to change mode
    bool isSoulMode = true; //True : soul mode, False : player mode 
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

        ModeChangerHandler();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(changeKey))
        {
            ModeChangerHandler();  
        }
    }


    void ModeChangerHandler()
    {
        Debug.Log("Changement mode");
        if (isSoulMode)
        {
            cam.transform.parent = playerFP.transform;
            cam.transform.localPosition = new Vector3(0, 0.4f, 0.2f);
            TPMouse.canMove = false;
            TPMove.canMove = false;
            FPMouse.canMove = true;
            FPMove.canMove = true;
        }
        else
        {
            cam.transform.parent = playerTP.transform.GetChild(0).transform;
            cam.transform.localPosition = new Vector3(0, 1, -8f);
            cam.transform.localEulerAngles = Vector3.zero;
            TPMouse.canMove = true;
            TPMove.canMove = true;
            FPMouse.canMove = false;
            FPMove.canMove = false;
        }
        isSoulMode = !isSoulMode;
    }
}
