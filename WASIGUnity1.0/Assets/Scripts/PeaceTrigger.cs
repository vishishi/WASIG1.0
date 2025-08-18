using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class PeaceTrigger : MonoBehaviour
{
    public Image image;
    public ParticleSystem heartParticles;
    public GameObject peacePrefab;
    public GameObject spawnGO;
    public ParticleSystem rightHand;
    public ParticleSystem leftHand;
    public Transform spawnPoint;

    private bool hasFaded = false;
    private bool peaceRight = false;
    private bool peaceLeft = false;
    private float fadeSpeed = 4f;
    private float fadeProgress = 0f;
    private bool reachedScore = false;
    [HideInInspector]
    public float timeElapsed = 0;
    [HideInInspector]


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


        StartCoroutine(SpawnImage());

    }

    // Update is called once per frame
    void Update()

    {
        Debug.Log("scoretimer:" + timeElapsed);

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

        else
        {

        }

    }

    public void TriggerRight()
    {
        peaceRight = true;
        Debug.Log(" right to true");


    }

    public void TriggerLeft()
    {
        peaceLeft = true;


    }

    public void UntriggerLeft()
    {
        peaceLeft = false;
        timeElapsed = 0;
        Debug.Log("time to 0");
 


    }

    public void UntriggerRight()
    {
        peaceRight = false;
        Debug.Log("right to false");
        timeElapsed = 0;
        Debug.Log("time to 0");
      
    }


    #region      Coroutines
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
        peaceRight = false;
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

    IEnumerator SpawnImage()
    {
        const float fillDuration = 3.5f;   // seconds to fully fill
        const float drainDuration = 0.5f;  // seconds to drain back to 0
        const float destroyThreshold = 0.2f;

        while (true)
        {
            // Wait until either peace is held
            yield return new WaitUntil(() => peaceLeft || peaceRight);

            // Spawn the filler
            GameObject gestureFiller = Instantiate(
                peacePrefab,
                spawnPoint.position,          // spawnPoint should be a Transform
                Quaternion.identity
            );
            Debug.Log("Peace gesture filler instantiated!");
            ParticleSystem particle;
            particle = gestureFiller.GetComponentInChildren<ParticleSystem>();
            Image fillerImage = gestureFiller.GetComponentInChildren<Image>();
            if (fillerImage == null)
            {
                Debug.LogWarning("No Image component found on spawned filler.");
                // Wait until both are released before looping again
                yield return new WaitUntil(() => !(peaceLeft || peaceRight));
                continue;
            }

            // Ensure known start value
            fillerImage.fillAmount = 0f;

            // FILL while either hand is held
            float t = 0f;
            while (peaceLeft || peaceRight)
            {
                t += Time.deltaTime;
                float normalized = Mathf.Clamp01(t / fillDuration);

                fillerImage.fillAmount = normalized;
                yield return null;
            }

            Debug.Log("gesture was filled up to: " + fillerImage.fillAmount);

 
            if (fillerImage.fillAmount >= 0.89f)
            {
                particle.Play();
                scoreCounter.score += 500;
                scoreCounter.perfect += 2;
                yield return new WaitUntil(() => !particle.isPlaying);
                Destroy(gestureFiller);
                continue;
            }

            // Otherwise DRAIN back down while neither is held
            while (!(peaceLeft || peaceRight) && gestureFiller != null)
            {
                // Move from current fill to 0 over drainDuration
                float step = Time.deltaTime / drainDuration;
                fillerImage.fillAmount = Mathf.MoveTowards(fillerImage.fillAmount, 0f, step);

                if (fillerImage.fillAmount <= destroyThreshold)
                {
                    Destroy(gestureFiller);
                    break;
                }

                yield return null;
            }

            // If they start holding again mid-drain, loop will restart and spawn anew
        }


        #endregion

    }
}
