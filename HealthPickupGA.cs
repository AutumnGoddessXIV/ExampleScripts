using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPiickupGA : GameActions
{
    public static Action<int> IncreaseHealth = delegate { };
    [SerializeField] private int healAmount; 

    public override void Action()
    {
        IncreaseHealth(healAmount);
    }
}
