using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchInteractor : Interactable
{
    public ParticleSystem ringer;
    private AudioSource callSound;
    private Animator animator;
    public bool hasLooked = false;
    public GameObject interactor;
   
    void Awake()
    {
        callSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider>();
        myCollider.enabled = false;
    }

    void Update()
    {

    }


    public void ReceiveCall()
    {
       ringer.Play();
       myCollider.enabled = true;
       callSound.Play();
       animator.SetBool("isCalling", true);
        
    }

    public override void Interact()
    {
        Debug.Log("player looked!");
        interactor.SetActive(false);

        ringer.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);


        callSound.Stop();
        animator.SetBool("isCalling", false);
        hasLooked = true;   
    }
}
