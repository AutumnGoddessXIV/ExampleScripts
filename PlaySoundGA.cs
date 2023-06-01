using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundGA : GameActions
{
    public AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public override void Action()
    {
        aSource.Play();
    }
}
