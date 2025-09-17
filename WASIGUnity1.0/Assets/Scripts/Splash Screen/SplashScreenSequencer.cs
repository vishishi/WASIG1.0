using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenSequencer : MonoBehaviour
{
    [Header ("VFX")]
    public Animator logo;
    public ParticleSystem[] stars;
   

    [Header("Sounds")]
    public float fadeDuration;
    public AudioSource introMusic;
    public AudioSource [] SFX;
    

    [Header ("References")]
    public GameObject pointableScreen;
    public GameObject rhythmButton;
    public ScreenFader screenFader;
  

    [HideInInspector] public bool hasPointed;
    [HideInInspector] public bool choseRythm = false;
    [HideInInspector] public bool choseNarra = false;


    void Start()
    {
        StartCoroutine(SSSequencer());
        pointableScreen.SetActive(false);
        rhythmButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator SSSequencer()
    {

        yield return new WaitForSeconds(10);
        logo.SetBool("hasWaited", true);
        yield return new WaitForSeconds(0.5f);
        SFX[0].Play();
        yield return new WaitWhile(() => SFX[0].isPlaying);
        SFX[1].Play();
        yield return new WaitWhile(() => SFX[1].isPlaying);
        SFX[2].Play();
        yield return new WaitWhile(() => SFX[2].isPlaying);
        SFX [3].Play();
        introMusic.Play();
        StartCoroutine(FadeMusic(0, 0.1f));
        foreach (var particles in stars)
        {
            particles.Play();
        }
        
       
        yield return new WaitForSeconds(3);
        pointableScreen.SetActive(true);
    
     
        
       
        
        
        yield return new WaitUntil(() => choseNarra || choseRythm);
        StartCoroutine(FadeMusic(0.1f, 0));
        
        if(choseNarra)
        {
            logo.SetBool("hasPointed", true);
            screenFader.ChangeScene("ShikiBedroom");
            yield return new WaitForSeconds(1);
            pointableScreen.SetActive(false); 
            
        }

        else if(choseRythm)
        {
            pointableScreen.SetActive(false);
            logo.SetBool("hasPointed", true);
            screenFader.ChangeScene("Main");
            yield return new WaitForSeconds(1);
            pointableScreen.SetActive(false);
        }

        





    }

    IEnumerator FadeMusic(float start, float target)
    {
        float elapsedTime = 0;
       

        // Ensure music starts at 0
        introMusic.volume = start;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            introMusic.volume = Mathf.Lerp(start, target, t);

            yield return null;
        }

        // Make sure it ends exactly at 1
        introMusic.volume = target;
    }

}
