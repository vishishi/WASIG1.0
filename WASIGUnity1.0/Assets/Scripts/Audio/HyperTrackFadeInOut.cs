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
            FadeInHypeTrack();
        }
        else
        {
            
        }
    }

    void FadeInHypeTrack()
    {
        float timeToFade = 0.5f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            hypeTrackAudio.volume = Mathf.Lerp(0, 0.75f, timeElapsed / timeToFade);
        }

    }



}
