using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeaceTrigger : MonoBehaviour
{
    public Image image;
    public ParticleSystem heartParticles;
    private bool hasFaded = false;
    private bool peaceRight = false;
    private bool peaceLeft = false;  
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

    // Update is called once per frame
    void Update()
    {
        if (peaceLeft)
        {
            StartCoroutine(FadeInImage());

        }

        else if (peaceRight)
        {
            StartCoroutine(FadeInImage());
        }

    }

          public void TriggerRight()
            {
                peaceRight = true;
           }

    public void TriggerLeft()
    {
        peaceLeft = true;
    }

    public void UntriggerLeft()
    {
        peaceLeft = false;
        Debug.Log("left is false!");
    }

    public void UntriggerRight()
    {
        peaceRight = false;
    }

    IEnumerator FadeInImage()
    {
        float elapsed = 0f;
        float duration = fadeSpeed; // You can rename fadeSpeed to something more descriptive if needed
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        if (ChoiceManager.Instance.gesture2Selected)
        {
            Debug.Log("Singleton works!");
        }

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
        gameObject.SetActive(false);

    }

}
