using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [HideInInspector] public float score;
    [HideInInspector] public float fillAmount;
    [HideInInspector] public int maxScore = 5500;

    [HideInInspector] public int bad;
    [HideInInspector] public int good;
    [HideInInspector] public int perfect;
    [HideInInspector] public int miss;

   
    public EnergyFiller energyFiller;

    public PeaceTrigger trigger;
    public bool isSuperCharged;
    public bool isTesting;


    [SerializeField] private int superChargeThreshold = 0;
    [SerializeField] private int maxMiss = 0;


    private int lastPerfect = 0;
    private int lastMiss = 0;
    private int perfectStreak = 0;

    private void Awake()
    {
        maxScore = 5500;
    }

    private void Start()
    {
        score = 0f;
        isSuperCharged = false;
        lastPerfect = perfect;
        lastMiss = miss;
        perfectStreak = 0;
    }

    void Update()
    {

        fillAmount = (float)score / maxScore;

        int dPerfect = perfect - lastPerfect;
        int dMiss = miss - lastMiss;    

        if (!isSuperCharged)
        {
            // Build streak with any new perfects
            if (dPerfect > 0)
                perfectStreak += dPerfect;

            // Any miss breaks the streak
            if (dMiss > maxMiss)
                perfectStreak = 0;

            // Enter supercharge when we hit the threshold
            if (perfectStreak >= superChargeThreshold)
            {
                isSuperCharged = true;
                perfectStreak = 0;
                Debug.Log("[SuperCharge] ON");
            }

            if(isTesting)
            {
                perfectStreak = 3;
            }
        }
        else 
        {
            
            if (dMiss > 0)
            {
                isSuperCharged = false;
                perfectStreak = 0;
                Debug.Log("[SuperCharge] OFF (miss)");
            }

        }

        // Store current values for next frame’s diff
        lastPerfect = perfect;
        lastMiss = miss;
    }

    public void AddGestureScore()
    {
        StartCoroutine(GestureTimer());
    }

    IEnumerator GestureTimer()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("right timer:" + trigger.timeElapsed);
        if (trigger.timeElapsed >= 3.8f)
        {
      
            Debug.Log("Timer worked!");
        }
    }
}


