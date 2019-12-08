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
    
    public void hide()
    {
        hintCanvas.gameObject.SetActive(false);
    }

    IEnumerator Fade()
    {
        anim.SetBool("FadeIN", true);
        yield return new WaitUntil(()=>background.color.a>=0.39);
        anim.SetBool("FadeIN", false);
        yield return new WaitForSeconds(5f);
        anim.SetBool("FadeOUT", true);
        yield return new WaitUntil(()=>background.color.a==0);
        anim.SetBool("FadeOUT", false);
        hide();
        
    }
}
