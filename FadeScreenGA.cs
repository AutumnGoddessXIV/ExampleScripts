using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class FadeScreenGA : GameActions
{
    public Image fadescreenImg;
    private bool bFadeIn, bFadeOut; // use these to only call fade in/out once and not multiple times
    public float speed = 1;
    public bool bFadeintst, bFadeouttst;

   
    private void Awake()
    {
        fadescreenImg = GetComponent<Image>();
       
    }
    public override void Action()
    {
        if (bFadeIn) return;

        bFadeIn = true;
        StartCoroutine(nameof(FadeIn));
    }

    public override void DeAction()
    {
        if (bFadeOut) return;

        bFadeOut = true;
        StartCoroutine(nameof(FadeOut));
    }

    IEnumerator FadeIn()
    {
        float rate = 0;
        Color black, clear;
        black = new Vector4(0, 0, 0, 1);
        clear = new Vector4(0, 0, 0, 0);
        while(rate != 1)
        {
            rate = Mathf.Clamp(rate + Time.deltaTime * speed, 0, 1);
            fadescreenImg.color = Color.Lerp(black, clear, rate);
            yield return new WaitForEndOfFrame();
        }
        
        bFadeIn = false;
    }

    IEnumerator FadeOut()
    {
        float rate = 0;
        Color black, clear;
        black = new Vector4(0, 0, 0, 1);
        clear = new Vector4(0, 0, 0, 0);
        while (rate != 1)
        {
            rate = Mathf.Clamp(rate + Time.deltaTime * speed, 0, 1);
            fadescreenImg.color = Color.Lerp(clear, black, rate);
            yield return new WaitForEndOfFrame();
        }
        bFadeOut = false; // prevent the courroutine from being called more than once
    }
}
