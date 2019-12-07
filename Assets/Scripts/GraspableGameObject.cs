using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GraspableGameObject : MonoBehaviour
{
    private bool isMoving;
    private bool isThrowing;
    private Vector3 location;
    public float speed = 10;

    public GameObject playerTP;
    public GameObject playerFP;
    public GameObject soulLink;
    public GameControl gameControl;

    void Update()
    {
        // Annulation / dÃ©crochage du grappin
        if (Input.GetKeyDown(KeyCode.Space) && isMoving)
        {
            isMoving = false;
            gameControl.UnlockInputs();
            playerFP.gameObject.GetComponent<Rigidbody>().useGravity = true;
            playerFP.gameObject.GetComponent<FirstPersonController>().m_GravityMultiplier = 2;
            playerTP.SetActive(false);
            soulLink.SetActive(false);
        }
    }

    public void Trigger(Transform selection)
    {
        if (!isThrowing && !isMoving)
        {
            Debug.Log("Triggering grappling hook...");

            isThrowing = true;
            location = selection.position;
            gameControl.LockInputs();
            playerFP.gameObject.GetComponent<Rigidbody>().useGravity = false;
            playerFP.gameObject.GetComponent<FirstPersonController>().m_GravityMultiplier = 1;

            playerTP.transform.position = playerFP.gameObject.GetComponent<FirstPersonController>().transform.position;
            playerTP.SetActive(true);
            soulLink.SetActive(true);

            StartCoroutine(SoulThrow());
        }
    }

    IEnumerator SoulThrow()
    {
        Debug.Log("Throwing the soul...");

        while (isThrowing)
        {
            playerTP.transform.position = Vector3.Lerp(playerTP.transform.position, location,
                speed * 10f * Time.deltaTime / Vector3.Distance(playerFP.transform.position, location));

            if (Vector3.Distance(playerTP.transform.position, location) < .5f)
            {
                isMoving = true;
                isThrowing = false;
            }

            yield return null;
        }

        StartCoroutine(MoveToSpot());
    }

    // DÃ©placement du joueur vers le point touchÃ© par le grappin
    IEnumerator MoveToSpot()
    {
        Debug.Log("Moving to the soul...");

        while (isMoving)
        {
            playerFP.transform.position = Vector3.Lerp(playerFP.transform.position, location,
                speed * Time.deltaTime / Vector3.Distance(playerFP.transform.position, location));

            // Si on est Ã  - de 1 unitÃ©(s) de la cible final on dÃ©croche le grappin automatiquement
            if (Vector3.Distance(playerFP.transform.position, location) < 1f)
            {
                isMoving = false;
                isThrowing = false;
                gameControl.UnlockInputs();
                playerFP.gameObject.GetComponent<Rigidbody>().useGravity = true;
                playerFP.gameObject.GetComponent<FirstPersonController>().m_GravityMultiplier = 2;

                playerTP.SetActive(false);
                soulLink.SetActive(false);
            }

            yield return null;
        }
    }
}