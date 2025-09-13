using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HyperTrackFadeInOut : MonoBehaviour
{
    [SerializeField] ScoreCounter score;
    AudioSource hypeTrackAudio;


    private void Start()
    {
        hypeTrackAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (score.isSuperCharged == true)
        {
            StartCoroutine(FadeInHypeTrack());
        }
        else
        {
            StartCoroutine(FadeOutHypeTrack());
        }
    }

    IEnumerator FadeInHypeTrack()
    {
        float timeToFade = 1f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            hypeTrackAudio.volume = Mathf.Lerp(0, 0.75f, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;

        }

    }

    IEnumerator FadeOutHypeTrack()
    {
        float timeToFade = 1f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            hypeTrackAudio.volume = Mathf.Lerp(0.75f, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;

        }
    }

    



}
