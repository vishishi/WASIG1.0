using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
                Debug.Log("Particles playing!");
            }

            yield return null;
        }

        // Wait for alpha to finish
        yield return new WaitUntil(() => image.color.a >= 0.98f);
        StartCoroutine(FadeOutImage(4));
        bothTrue = false;
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
        gameObject.SetActive(false);

    }
}
