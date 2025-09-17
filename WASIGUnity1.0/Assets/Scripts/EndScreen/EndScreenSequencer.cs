using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class EndScreenSequencer : MonoBehaviour
{
    public Animator textAnimator;
    public ParticleSystem finalStars;
    public ScreenFader screenFader;
    public AudioSource finalMusic;
    public AudioSource finalSFX;
    public float fadeDuration;

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
        StartCoroutine(FadeMusic(0, 0.5f));
        yield return new WaitForSeconds(3);
        textAnimator.SetBool("hasCue", true);
        yield return new WaitForSeconds(6);
        finalStars.Play();
        finalSFX.Play();
        yield return new WaitForSeconds(13);
        finalStars.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        finalSFX.Stop();
        screenFader.AvengersEndGame();
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeMusic(0.5f, 0f));
    }

    IEnumerator FadeMusic(float start, float target)
    {
        float elapsedTime = 0;


        // Ensure music starts at 0
        finalMusic.volume = start;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            finalMusic.volume = Mathf.Lerp(start, target, t);

            yield return null;
        }

        // Make sure it ends exactly at 1
        finalMusic.volume = target;
    }
}
