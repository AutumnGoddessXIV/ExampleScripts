using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InvincibilityGA : GameActions
{
    public float invincibilityTime = 1;
    public static Action InvincibilityOn = delegate { };
    public static Action InvincibilityOff = delegate { };
    public override void Action()
    {
        StartCoroutine(nameof(InvincibilityTimer));
    }

    IEnumerator InvincibilityTimer()
    {
        InvincibilityOn();
        yield return new WaitForSeconds(invincibilityTime);
        InvincibilityOff();


    }
}
