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
    public RotatingPlatform doorEnigma1;

    string tpKey = "k";
    string changeKey = "l"; //Key to change mode
    public bool isSoulMode = true; //True : soul mode, False : player mode 
    float maxSoulDist = 10f; //max distance between soul and player
    bool canChange = true;
    bool canTP = false;
    public float cooldownTPduration = 5; //seconds
    bool activatedTP = false;

    TPMouseMovement TPMouse;
    TPMovement TPMove;
    FirstPersonController FPController;
    Animator animFP;

    

    void Start()
    {
        TPMouse = playerTP.GetComponent<TPMouseMovement>();
        TPMove  = playerTP.GetComponent<TPMovement>();
        FPController = playerFP.GetComponent<FirstPersonController>();
        animFP = playerFP.GetComponent<Animator>();

        Physics.IgnoreCollision(playerFP.GetComponent<CharacterController>(), playerTP.GetComponent<Collider>());

        ModeChangerHandler();
    }

    // Update is called once per frame
    void Update()
    {
        if (doorEnigma1.activated && !activatedTP)
        {
            activatedTP = true;
            canTP = true;
        }
            

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
        if (Input.GetKeyDown(tpKey) && isSoulMode && canTP)
        {
            playerFP.transform.position = playerTP.transform.position;
            FPController.isTP = true;
            Debug.Log("TP");
            ModeChangerHandler();
            StartCoroutine(cooldownTPRoutine());
        }
        #endregion
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
            UnlockInputs();
        }
        else 
        {
            
            // Open the menu
            Debug.Log("Opening the menu.");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            LockInputs();
            animFP.SetBool("isWalking", false);

        }
    }

    void ModeChangerHandler()
    {
        if(canChange)
        {
            Debug.Log("Changement mode");
            if (isSoulMode)
            {
                //Enters in FP
                StartCoroutine(animationTransitionTPtoFP());
            }
            else
            {
                //Enters in TP 
                playerTP.SetActive(true);
                soulLink.SetActive(true);
                postProcessVolume.SetActive(true);
                CancelAnimations();
                
                playerTP.transform.localEulerAngles = playerFP.transform.localEulerAngles;
                cam.transform.localEulerAngles = Vector3.zero;

                TPMouse.canMove = true;
                FPController.canMove = false;
                StartCoroutine(animationTransitionFPtoTP());
            }
            isSoulMode = !isSoulMode;
        }
    }

    public void LockInputs()
    {
        TPMouse.canMove = false;
        TPMove.canMove = false;
        FPController.canMove = false;
        canChange = false;
    }

    public void UnlockInputs()
    {
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
        canChange = true;
    }

    void CancelAnimations()
    {
        animFP.SetBool("isWalking", false);
        animFP.SetBool("isJumping", false);
        animFP.SetBool("grounded", false);
    }

    IEnumerator cooldownTPRoutine()
    {
        canTP = false;
        yield return new WaitForSeconds(cooldownTPduration);
        canTP = true;
    }
    
    IEnumerator animationTransitionFPtoTP()
    {
        StartCoroutine(camAnimationFPtoTP());
        TPMove.canMove = false;
        canChange = false;
        float maxDuration = 3f;
        float normalizedTime = 0;
        Vector3 startPos = playerFP.transform.position;
        Vector3 endPos = playerFP.transform.position + playerFP.transform.forward * 2;
        startPos += Vector3.up * 0.7f;
        endPos += Vector3.up * 0.7f;
        playerTP.transform.position = startPos;
        camRot.transform.rotation = playerFP.transform.rotation;
        while (Vector3.Distance(playerTP.transform.position, endPos) > .5f && normalizedTime <= 1f)
        {
            playerTP.transform.position = Vector3.Lerp(playerTP.transform.position,
                endPos, 0.7f * Time.deltaTime / Vector3.Distance(playerTP.transform.position, endPos));
            normalizedTime += Time.deltaTime/maxDuration;
            yield return null;
        }
        TPMove.canMove = true;
        canChange = true;
    }
    
    IEnumerator camAnimationFPtoTP()
    {
        
        cam.transform.parent = camRot.transform;
        cam.transform.localPosition = Vector3.zero;
        Vector3 startPos = cam.transform.localPosition;
        
        Vector3 endPos = new Vector3(0, 1, -8f);
        while (Vector3.Distance(cam.transform.localPosition, endPos) > .05f)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition,
                endPos, 2f * Time.deltaTime / Vector3.Distance(cam.transform.localPosition, endPos));
            yield return null;
        }
        
        cam.transform.localPosition = new Vector3(0, 1, -8f);
    }

    IEnumerator animationTransitionTPtoFP()
    {
        TPMouse.canMove = false;
        TPMove.canMove = false;
        canChange = false;
        float maxDuration = 3f;
        float normalizedTime = 0;
        Vector3 endPos = playerFP.transform.position;
        endPos += Vector3.up * 0.7f;
        while (Vector3.Distance(playerTP.transform.position, endPos) > .5f && normalizedTime <= 1f)
        {
            playerTP.transform.position = Vector3.Lerp(playerTP.transform.position,
                endPos, 10f * Time.deltaTime / Vector3.Distance(playerTP.transform.position, endPos));
            normalizedTime += Time.deltaTime / maxDuration;
            yield return null;
        }

        playerTP.transform.position = Vector3.zero;
        //returning 0 will make it wait 1 frame
        yield return 0;
        playerTP.SetActive(false);

        soulLink.SetActive(false);
        postProcessVolume.SetActive(false);

        cam.transform.parent = playerFP.transform;
        cam.transform.localPosition = new Vector3(0.1f, 1f, 0.15f); 

        FPController.canMove = true;
        canChange = true;
    }

}
