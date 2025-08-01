using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script handles the heart gesture specifically. It works by checking if both hands are making the heart gestures, there are public functions to change the booleans related to the hand gestures
//They are public so that they can be accesed by the hand gesture scripts for meta
public class GestureTrigger : MonoBehaviour
{
    public Image image;
    public ParticleSystem heartParticles;

    public bool heartRight = false;
    public bool heartLeft = false;
    public bool bothTrue = false;

    private bool hasFaded = false;
    private float fadeSpeed = 4f;
    private float fadeProgress = 0f;

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

    void Update()
    {
        //This is the way to access the emission amount of the heart particle system
        var emission = heartParticles.emission;
        emission.enabled = moduleEnabled;
        if (heartLeft)
        {
            Debug.Log("Left heart detected!");
        }
        if (heartRight)
        {
            Debug.Log("Right heart detected!");
        }

        if (heartLeft && heartRight)
        {
            bothTrue = true;
        }

        else
        {

            bothTrue = false;

        }


        if (bothTrue)
        {
            StartCoroutine(FadeInImage());
        }

    }
    //This are the booleans that are called when performing the gesture
    public void TriggerRight()
    {
        heartRight = true;
    }

    public void TriggerLeft()
    {
        heartLeft = true;
    }

    public void UntriggerLeft()
    {
        heartLeft = false;
        Debug.Log("left is false!");
    }

    public void UntriggerRight()
    {
        heartRight = false;
    }

    private void HeartFade()
    {
        hasFaded = true;
        Debug.Log("Started fading image...");
    }

    //Coroutine to make the heart appear and trigger the heart particles
    IEnumerator FadeInImage()
    {
        float elapsed = 0f;
        float duration = fadeSpeed;
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
                Debug.Log("Particles playing!");
            }

            yield return null;
        }

        // Wait for alpha to finish
        yield return new WaitUntil(() => image.color.a >= 0.98f);
        StartCoroutine(FadeOutImage(4));
        bothTrue = false;
    }

    //Coroutine to make the heart fade out and the particles stop
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
        gameObject.SetActive(false);

    }
}
