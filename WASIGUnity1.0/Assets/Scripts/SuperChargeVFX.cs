using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperChargeVFX : MonoBehaviour
{
    public ScoreCounter score;
    public ParticleSystem[] particleSystems;
    public Animator [] animatorSmall;
    public Animator [] animatorLarge;

    private void Awake()
    {

    }
    void Start()
    {
        StartCoroutine(VFXController());
        foreach (var particleSystem in particleSystems)
        {
            var mainParticles = particleSystem.main; // use the loop variable
            mainParticles.playOnAwake = false;
        }
    }


    void Update()
    {

    }

    IEnumerator VFXController()
    {
        while (true)
        {

            yield return new WaitUntil(() => score.isSuperCharged);

            animatorSmall[0].SetBool("supercharged", true);
            animatorLarge[0].SetBool("supercharged", true);
            animatorSmall[1].SetBool("supercharged", true);
            animatorLarge[1].SetBool("supercharged", true);
            foreach (var ps in particleSystems)
                ps.Play();


            yield return new WaitUntil(() => !score.isSuperCharged);
            animatorSmall[0].SetBool("unsupercharged", true);
            animatorLarge[0].SetBool("unsupercharged", true);
            animatorSmall[1].SetBool("unsupercharged", true);
            animatorLarge[1].SetBool("unsupercharged", true);

            foreach (var ps in particleSystems)
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}


