using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonWithOrder : MonoBehaviour
{
    public int digit;
    float activationMaxDist = 15;
    int nbButtons = 6; 
    int[] solution = new int[] {1, 2, 3, 4, 5, 6};
    static int[] input = new int[] { -1, -1, -1, -1, -1, -1};
    float animTime = 0.7f;
    float animOffset = 0.2f;
    bool canClick = true;
    public GameControl GC;
    Renderer buttonRenderer;
    public Material playerColor;
    public Material soulColor;

    void Start()
    {
        buttonRenderer = GetComponent<Renderer>();
    }
    void Update()
    {
        if(GC.isSoulMode)
        {
            buttonRenderer.material = soulColor;
        } else buttonRenderer.material = playerColor;
    }

    private void OnMouseDown()
    {
        if(canClick)
        {
            Debug.Log(digit);
            StartCoroutine(AnimateButton());
            for (int i = 0; i < nbButtons - 1; i++)
            {
                input[i] = input[i + 1];
            }
            input[nbButtons - 1] = digit;

            bool flag = true;
            for (int i = 0; i < nbButtons; i++)
            {
                if (solution[i] != input[i]) flag = false;
            }

            if (flag)
            {
                //Win sequence
                Debug.Log("You Won !");
            }
        }
    }

    IEnumerator AnimateButton()
    {
        canClick = false;
        float elapsedTime = 0;
        float startPosZ = transform.position.z;
        float endPosZ = transform.position.z - animOffset;
        float tmp;
        while (elapsedTime < animTime)
        {
            tmp = Mathf.Lerp(startPosZ, endPosZ, (elapsedTime / animTime));
            transform.position = new Vector3(transform.position.x, transform.position.y, tmp);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        elapsedTime = 0;
        while (elapsedTime < animTime)
        {
            tmp = Mathf.Lerp(endPosZ, startPosZ, (elapsedTime / animTime));
            transform.position = new Vector3(transform.position.x, transform.position.y, tmp);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canClick = true;
    }
}
