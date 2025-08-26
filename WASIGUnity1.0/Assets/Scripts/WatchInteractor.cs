using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchInteractor : Interactable
{
    public ParticleSystem ringer;
    private AudioSource callSound;
    private Animator animator;
    public bool hasLooked = false;
    void Start()
    {
        callSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }


    public void ReceiveCall()
    {
       ringer.Play();

       callSound.Play();
       animator.SetBool("isCalling", true);
        
    }

    public override void Interact()
    {
        Debug.Log("player looked!");


        ringer.Stop(true, ParticleSystemStopBehavior.StopEmitting);


        callSound.Stop();
        animator.StopPlayback();
        hasLooked = true;   
    }
}
