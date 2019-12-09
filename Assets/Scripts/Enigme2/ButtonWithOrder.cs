using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonWithOrder : MonoBehaviour
{
    public int digit;
    float activationMaxDist = 15;
    int nbButtons = 6; 
    public int[] solution = new int[] {4, 5, 3, 2, 6, 1};
    static int[] input = new int[] { -1, -1, -1, -1, -1, -1};
    float animTime = 0.7f;
    float animOffset = 0.2f;
    bool canClick = true;
    public bool hasWon = false;
    public GameControl GC;
    Renderer buttonRenderer;
    public Material playerColor;
    public Material soulColor;
    public GameObject player;
    float clickDistance = 2.5f;
    Animator anim;
    

    void Start()
    {
        buttonRenderer = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(GC.isSoulMode)
        {
            buttonRenderer.material = soulColor;
        } else buttonRenderer.material = playerColor;

        if(anim.GetBool("isPushed"))
        {
            if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("PressButton"))
            {
                anim.SetBool("isPushed", false);
            }
        }
    }

    private void OnMouseDown()
    {
        if(canClick && Vector3.Distance(player.transform.position, transform.position) < clickDistance)
        {
            Debug.Log("Button " + digit);
            StartCoroutine(AnimateButton());
            for (int i = 0; i < nbButtons - 1; i++)
            {
                input[i] = input[i + 1];
            }
            input[nbButtons - 1] = digit;

            bool flag = true;
            for (int i = 0; i < nbButtons; i++)
            {
                if (solution[i] != input[i])
                {
                    flag = false;
                }
            }

            if (flag)
            {
                //Win sequence
                Debug.Log("You solved enigma 2 !");
                hasWon = true;
            }
        }
    }

    IEnumerator AnimateButton()
    {
        FindObjectOfType<AudioManager>().Play("Switch");
        canClick = false;
        anim.SetBool("isPushed", true);
        canClick = true;
        yield return new WaitForEndOfFrame();
    }
}
