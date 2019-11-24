using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    private bool isDead;
    public Animator anim;
    public Image black;

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        if (this.transform.position.y <= 0.0)
        {
            isDead = true;
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        FindObjectOfType<AudioManager>().Play("Death");
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isDead = false;
    }
}
