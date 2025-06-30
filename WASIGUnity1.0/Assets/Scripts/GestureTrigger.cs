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
    public bool bothTrue = true;

    private bool hasFaded = false;
    private float fadeSpeed = 200f;
    private float fadeProgress = 0f;

    void Start()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        Debug.Log("alpha to 0");
        heartParticles.Play();
        heartParticles.Pause();
    }

    void Update()
    {
        if (heartLeft)
        {
            Debug.Log("Left heart detected!");
        }
        if (heartRight)
        {
            Debug.Log("Right heart detected!");
        }

        // Only trigger HeartFade once when both gestures are true and it hasn't faded yet
        if (heartLeft && heartRight && !hasFaded)
        {
            HeartFade();
        }

        // If fading has started, progress the fade
        if (hasFaded && image.color.a < 1f)
        {
            FadeInImage();
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

    private void FadeInImage()
    {
        fadeProgress += Time.deltaTime / fadeSpeed;
        image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 1f), fadeProgress);

        if (image.color.a >= 0.5f && !heartParticles.isPlaying)
        {
            heartParticles.Play();
            Debug.Log("Particles playing!");
        }
    }
}
