using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialPhase
{
    None,
    Left,
    Right,
    Panda,
    Gesture
}
public class TutorialScoreCounter : MonoBehaviour
{
    [HideInInspector] public float score;
    [HideInInspector] public float fillAmount;
    [HideInInspector] public int maxScore = 5500;

    [HideInInspector] public int bad;
    [HideInInspector] public int good;
    [HideInInspector] public int perfect;
    [HideInInspector] public int miss;
    [HideInInspector] public float snippetTime;


    public bool completedGesture = false;
    public bool hasFailed = false;

    public TutorialEnergyFiller energyFiller;
    public TutorialDialogueManager dialogueManager;
    public TutorialBeatSpawner beatSpawner;

    public GameObject tutorialRetryScreen;
    public GameObject comms;
    public PeaceTrigger trigger;
    public bool isSuperCharged;
    public bool isTesting;


    [SerializeField] private int superChargeThreshold = 3;
    [HideInInspector] public int gestureChoice; [HideInInspector] public bool hasChosen;

    private int lastPerfect = 0;
    private int lastMiss = 0;
    private int perfectStreak = 0;

    private Dictionary<TutorialPhase, bool> phaseCompletion = new Dictionary<TutorialPhase, bool>();

    private void Awake()
    {
        maxScore = 5500;
        foreach (TutorialPhase phase in System.Enum.GetValues(typeof(TutorialPhase)))
        {
            phaseCompletion[phase] = false;
        }
    
    }

    private void Start()
    {
        score = 0f;
        isSuperCharged = false;
        lastPerfect = perfect;
        lastMiss = miss;
        perfectStreak = 0;
        StartCoroutine(TutorialIdentifier());
        
    }

    void Update()
    {
        snippetTime = dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime;    
        fillAmount = (float)score / maxScore;

        int dPerfect = perfect - lastPerfect;
        int dMiss = miss - lastMiss;

        if (!isSuperCharged)
        {
            // Build streak with any new perfects
            if (dPerfect > 0)
                perfectStreak += dPerfect;

            // Any miss breaks the streak
            if (dMiss > 0)
                perfectStreak = 0;

            // Enter supercharge when we hit the threshold
            if (perfectStreak >= superChargeThreshold)
            {
                isSuperCharged = true;
                perfectStreak = 0;
                Debug.Log("[SuperCharge] ON");
            }

            if (isTesting)
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
    #region Dictionary Functions
    public void MarkCompleted(TutorialPhase phase)
    {
        phaseCompletion[phase] = true;
    }
    public bool isCompleted (TutorialPhase phase)
    {
        return phaseCompletion[phase];
    }
    #endregion

    #region Coroutines
    IEnumerator TutorialIdentifier()
    {
        while(true)
        {
            switch(dialogueManager.currentSnippetIndex)
            {
                case 39:
                {
                        yield return new WaitForSeconds(snippetTime);
                        yield return StartCoroutine(RunPhase(TutorialPhase.Right, 20, 3));
                }

                    break;
                case 49:
                    {
                        yield return new WaitForSeconds(snippetTime);
                        yield return StartCoroutine(RunPhase(TutorialPhase.Left, 20, 3));
                    }

                    break;

                case 68:
                    {
                        yield return new WaitForSeconds(snippetTime);
                        yield return StartCoroutine(RunPhase(TutorialPhase.Panda, 20, 3));
                    }

                    break;
            }
            yield return null;
        }
    }

    IEnumerator RunPhase(TutorialPhase phase, float duration, int requiredPoints)
    {
        bool success = false;
        comms.SetActive(false);
        Debug.Log(phase.ToString() + " is the current phase");

        while (!success) // whole block runs until success
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                if (perfect + good >= requiredPoints) // use >= in case of overshoot
                {
                    success = true;
                    break;
                   
                }

                elapsedTime += Time.deltaTime;
                yield return null; // wait until next frame
            }

            if (!success)
            {
                yield return new WaitForSeconds(2);
                tutorialRetryScreen.SetActive(true);
                yield return new WaitForSeconds(7);
                tutorialRetryScreen.SetActive(false);
                beatSpawner.RestartFromCheckpoint(); // restart music & beats
                
            }
        }

        perfect = 0;
        good = 0;
        MarkCompleted(phase);
        Debug.Log(phase.ToString() + " was completed!");
        yield return new WaitForSeconds(3);
        comms.SetActive(true);
    }



    #endregion
}
