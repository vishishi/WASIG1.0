using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBGMAudio : MonoBehaviour
{

    public AudioClip tutorialBGMtrack1, tutorialBGMtrack2, tutorialBGMtrack3;
    public AudioSource tutorialBGMAudioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        //tutorialBGMAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTutorialBGMTrack(AudioClip currentTutorialBGMTrack)
    {
        
        tutorialBGMAudioSource.Stop();
        tutorialBGMAudioSource.clip = currentTutorialBGMTrack;

        if (currentTutorialBGMTrack == null)
        {
            Debug.LogError("AudioClip is NULL!");
            return;
        }

        
        tutorialBGMAudioSource.Play();
        StartCoroutine(FadeInBGM());


    }

    public void StopTutorialBGMTrack()
    {
        StartCoroutine(FadeOutBGM());
        

    }

    IEnumerator FadeInBGM()
    {
        float timeToFade = 1f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            tutorialBGMAudioSource.volume = Mathf.Lerp(0, 0.2f, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;

        }

    }

    IEnumerator FadeOutBGM()
    {
        float timeToFade = 2f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            tutorialBGMAudioSource.volume = Mathf.Lerp(0.2f, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;

        }
        tutorialBGMAudioSource.Stop();
    }

    // just for button test

    public void PlayAudioOnButton()
    {
        PlayTutorialBGMTrack(tutorialBGMtrack1);
    }


}
