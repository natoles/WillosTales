using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public string text;
    public Canvas hintCanvas;
    private TextMeshProUGUI textObject;
    private Image background;
    private Animator anim;
    private bool _isAlreadyActivated = false;

    private void Start()
    {
        textObject = hintCanvas.GetComponentInChildren<TextMeshProUGUI>();
        background = hintCanvas.GetComponentInChildren<Image>();
        anim = hintCanvas.GetComponent<Animator>();
    }

    public void trigger()
    {
        if (!_isAlreadyActivated)
        {
            textObject.text = text;
            hintCanvas.gameObject.SetActive(true);
            _isAlreadyActivated = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(()=>background.color.a>=0.39);
        yield return new WaitForSeconds(5f);
        anim.SetBool("Fade", false);
        yield return new WaitUntil(()=>background.color.a==0);
    }
}
