using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenSequencer : MonoBehaviour
{
    public Animator textAnimator;
    public ParticleSystem finalStars;
    public ScreenFader screenFader;

    void Start()
    {
        StartCoroutine(FinalSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FinalSequence()
    {
        yield return new WaitForSeconds(3);
        textAnimator.SetBool("hasCue", true);
        yield return new WaitForSeconds(6);
        finalStars.Play();
        yield return new WaitForSeconds(13);
        finalStars.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        screenFader.AvengersEndGame();
    }
}
