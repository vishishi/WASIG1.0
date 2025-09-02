using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialSuperChargeVFX : MonoBehaviour
{
    public TutorialScoreCounter score;
    public Image[] ringImages;
    public ParticleSystem[] particles;
    public GameObject[] bracelets;


    private void Awake()
    {
        foreach (var image in ringImages)
        {
            image.enabled = false;
        }
        foreach (var brace in bracelets)
        {
            brace.SetActive(false);
        }
    }
    void Start()
    {
        StartCoroutine(VFXController());

    }


    IEnumerator VFXController()
    {
        while (true)
        {

            yield return new WaitUntil(() => score.isSuperCharged);


            foreach (var image in ringImages)
            {
                image.enabled = true;
            }

            foreach (var brace in bracelets)
            {
                brace.SetActive(true);
            }

            foreach (var particle in particles)
            {
                particle.Play();
            }



            yield return new WaitUntil(() => !score.isSuperCharged);


            foreach (var brace in bracelets)
            {
                brace.SetActive(false);
            }

            foreach (var image in ringImages)
            {
                image.enabled = false;
            }

            foreach (var particle in particles)
            {
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }


        }
    }
}
