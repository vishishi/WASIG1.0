using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeaceTrigger : MonoBehaviour
{
    public Image image;
    public ParticleSystem heartParticles;
    public ParticleSystem rightHand;
    public ParticleSystem leftHand;
    private bool hasFaded = false;
    private bool peaceRight = false;
    private bool peaceLeft = false;  
    private float fadeSpeed = 4f;
    private float fadeProgress = 0f;
    private bool reachedScore = false;
    [HideInInspector]
    public float timeElapsed = 0; 

    public ScoreCounter scoreCounter;

    [HideInInspector]
    public bool moduleEnabled;
    void Start()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        Debug.Log("alpha to 0");
        heartParticles.Play();
        heartParticles.Pause();
        moduleEnabled = true;
    }

    // Update is called once per frame
    void Update()                                                                                                                                               

    {
        Debug.Log ("scoretimer:" + timeElapsed);
        
        if (peaceLeft)
        {
            timeElapsed += Time.deltaTime;
            StartCoroutine(FadeInImage());
            
      
        }

        if (peaceRight)
        {
            timeElapsed += Time.deltaTime;
            StartCoroutine(FadeInImage());

        }

    }

    public void TriggerRight()
    {
        peaceRight = true;
        Debug.Log(" right to true");
        rightHand.Play();

    }

    public void TriggerLeft()
    {
        peaceLeft = true;
        leftHand.Play();
      
    }

    public void UntriggerLeft()
    {
        peaceLeft = false;
        timeElapsed = 0;
        Debug.Log("time to 0");
        leftHand.Stop();


    }

    public void UntriggerRight()
    {
        peaceRight = false;
        Debug.Log("right to false");
        timeElapsed = 0;
        Debug.Log("time to 0");
        rightHand.Stop();
    }

    IEnumerator FadeInImage()
    {
        float elapsed = 0f;
        float duration = fadeSpeed; // You can rename fadeSpeed to something more descriptive if needed
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float fadeProgress = Mathf.Clamp01(elapsed / duration);
            image.color = Color.Lerp(startColor, targetColor, fadeProgress);
            
            // Trigger particles halfway through fade
            if (image.color.a >= 0.5f && !heartParticles.isPlaying)
            {
                heartParticles.Play();
            }

            yield return null;
        }

        // Wait for alpha to finish
        yield return new WaitUntil(() => image.color.a >= 0.98f);
        StartCoroutine(FadeOutImage(4));
        peaceLeft = false;
        peaceRight=false;   
    }

    IEnumerator FadeOutImage(float duration)
    {
        float elapsed = 0f;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Fade to transparent

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float fadeProgress = Mathf.Clamp01(elapsed / duration);
            image.color = Color.Lerp(startColor, targetColor, fadeProgress);
            yield return null;
        }

        if (image.color.a <= 0.5f)
        {
            moduleEnabled = false;



        }
        yield return new WaitUntil(() => image.color.a <= 0.05f);
        //gameObject.SetActive(false);

    }

   

}
