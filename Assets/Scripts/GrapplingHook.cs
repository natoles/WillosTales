using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GrapplingHook : MonoBehaviour
{

    public Camera cam;
    public RaycastHit hit;

    public LayerMask surfaces;
    public int maxDistance;

    public bool isMoving;
    bool isThrowing;
    public Vector3 location;

    public float speed = 10;
    public Transform hook;

    public FirstPersonController FPC;

    public GameObject playerTP;
    public GameObject soulLink;
    public GameControl gameControl;

    void Update()
    {

        // Envoi du grappin
        if (Input.GetKeyDown(KeyCode.J) && !isThrowing && !isMoving)
        {
            Grapple();
        }

        if(isThrowing)
        {
            SoulThrow();
        }

        // Si le personnage vole, on l'envoie vers le point d'arrivÃ©e 
        if (isMoving)
        {
            MoveToSpot();
        }

        // Annulation / dÃ©crochage du grappin
        if (Input.GetKeyDown(KeyCode.Space) && isMoving)
        {
            isMoving = false;
            gameControl.UnlockInputs();
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            FPC.m_GravityMultiplier = 2;
            playerTP.SetActive(false);
            soulLink.SetActive(false);
        }


    }

    // Lors de l'envois du grappin
    public void Grapple()
    {
        // On crÃ©Ã© un raycast de "maxDistance" unitÃ©s depuis la camÃ©ra vers l'avant.
        // Si ce raycast touche quelque chose c'est que la grappin est utilisable
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance, surfaces))
        {
            isThrowing = true;
            location = hit.point;
            gameControl.LockInputs();
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            FPC.m_GravityMultiplier = 1;

            playerTP.SetActive(true);
            playerTP.transform.position = FPC.transform.position;
            soulLink.SetActive(true);
        }

    }

    // DÃ©placement du joueur vers le point touchÃ© par le grappin
    public void MoveToSpot()
    {
        isThrowing = false;
        transform.position = Vector3.Lerp(transform.position, location, speed * Time.deltaTime / Vector3.Distance(transform.position, location));

        // Si on est Ã  - de 1 unitÃ©(s) de la cible final on dÃ©croche le grappin automatiquement
        if (Vector3.Distance(transform.position, location) < 1f)
        {
            isMoving = false;
            isThrowing = false;
            gameControl.UnlockInputs();
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            FPC.m_GravityMultiplier = 2;

            playerTP.SetActive(false);
            soulLink.SetActive(false);
        }
    }

    public void SoulThrow()
    {
        playerTP.transform.position = Vector3.Lerp(playerTP.transform.position, location, speed * 10f * Time.deltaTime / Vector3.Distance(transform.position, location));

        if(Vector3.Distance(playerTP.transform.position, location) < .5f)
        {
            isMoving = true;
        }

    }
}