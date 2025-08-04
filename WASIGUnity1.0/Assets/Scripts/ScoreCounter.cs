using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [HideInInspector]
    public float score;
    [HideInInspector]
    public float fillAmount;
    [HideInInspector]
    public int maxScore = 5500;
    [HideInInspector]
    public int bad;
    [HideInInspector]
    public int good;
    [HideInInspector]
    public int perfect;
    [HideInInspector]
    public int miss;
    [HideInInspector]
    public int superCharge;
    public EnergyFiller energyFiller;

    public PeaceTrigger trigger;


    private void Awake()
    {
        maxScore = 5500;
    }
    private void Start()
    {
        score = 0;


    }
    void Update()
    {

        superCharge = perfect - (bad + miss);
        fillAmount = (float)score / maxScore;

    }

    public void AddGestureScore()
    {
        StartCoroutine(GestureTimer());
    }

    IEnumerator GestureTimer()

    {
        yield return new WaitForSeconds(4);
        Debug.Log("right timer:" + trigger.timeElapsed);
        if (trigger.timeElapsed >= 4)
        {
            score += 500;
            Debug.Log("Timer worked!");
        }
    }
}


