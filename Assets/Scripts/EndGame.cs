using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    
    public GameObject menu;
    public Animator anim;
    public Image black;

    private void OnTriggerEnter(Collider col)
    {
        // Open the menu
            Debug.Log("End of the game.");
            StartCoroutine(Fading());
    }
    
    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(()=>black.color.a==1);
        anim.SetBool("Fade", false);
        menu.SetActive(!menu.activeSelf);
        anim.SetBool("FadeBack", true);
        yield return new WaitUntil(()=>black.color.a==0);
        yield return new WaitForSeconds(3f);
        anim.SetBool("Fade", true);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
}
