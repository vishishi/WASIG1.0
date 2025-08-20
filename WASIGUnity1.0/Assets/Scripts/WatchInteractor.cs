using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchInteractor : MonoBehaviour
{
    public ParticleSystem ringer;
    private AudioSource callSound;
    private Animator animator;
    void Start()
    {
        callSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision happened!");


        ringer.Stop(true, ParticleSystemStopBehavior.StopEmitting);


        callSound.Stop();
        animator.StopPlayback();

    }
    public void ReceiveCall()
    {

        {
            ringer.Play();



            callSound.Play();
            animator.SetBool("isCalling", true);
        }
    }
}
