using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlowManager : MonoBehaviour
{
    public Image[] beats;
    public Image bigHeart;
    public Collider[] colliders;
    public StartGame game;
    public GameObject heartHands;

    private bool rhythmStarted = false;

    void Update()
    {
        if (game.hasStarted && !rhythmStarted)
        {
            Debug.Log("Starting rhythm!");
            rhythmStarted = true;
            StartCoroutine(ProtoRhythm());
        }
    }

    IEnumerator ProtoRhythm()
    {
     
        yield return new WaitForSeconds(2);
        StartCoroutine(RadialFill(beats[0], 2f));
        colliders[0].enabled = true;
        yield return new WaitUntil(()=> beats[0].fillAmount == 0f);
        //yield return new WaitForSeconds(2);
        StartCoroutine(RadialFill(beats[1], 1f));
        yield return new WaitUntil(() => beats[1].fillAmount == 0f);
        yield return new WaitForSeconds(2);
        StartCoroutine(RadialFill(beats[2], 2f));
        yield return new WaitUntil(() => beats[2].fillAmount == 0f);
        yield return new WaitForSeconds(2);
        StartCoroutine(RadialFill(beats[3], 2f));
        yield return new WaitUntil(() => beats[3].fillAmount == 0f);
        StartCoroutine(RadialFill(beats[4], 1f));
        yield return new WaitUntil(() => beats[4].fillAmount == 0f);
        StartCoroutine(RadialFill(beats[5], 1f));
        yield return new WaitUntil(() => beats[5].fillAmount == 0f);
        heartHands.SetActive(true);
        yield return new WaitUntil(() => bigHeart.fillAmount == 1f);
        heartHands.SetActive(false);


         
    }

    public IEnumerator RadialFill(Image beat, float duration)
    {
        float elapsed = 0f;
        beat.fillAmount = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            beat.fillAmount = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        beat.fillAmount = 1f;
    }
}

