using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image[] commsImages;
    public Image backgroundImage;
    public AudioSource retryLine;

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

    public void AddPerfect()
    {
        if (hasChosen)
        {
            perfect += 3;
        }
    }
    #endregion

    #region Coroutines
    IEnumerator TutorialIdentifier()
    {
        Debug.Log("Score counter coroutine started!");
        while (dialogueManager.currentSnippetIndex < 60)
        {
            switch(dialogueManager.currentSnippetIndex)
            {
                case 1:
                    {
                        Debug.Log(" <color=#00FFFF> Score Counter: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
                        Debug.Log(" <color=#00FFFF> Score Counter: </color> " + "read the snippet time as " + snippetTime.ToString());
                    }

                    break;
                case 30:
                {
                        Debug.Log(" <color=#00FFFF> Score Counter: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
                        Debug.Log(" <color=#00FFFF> Score Counter: </color> " + "read the snippet time as " + snippetTime.ToString());
                        yield return new WaitForSeconds(snippetTime);
                        yield return StartCoroutine(RunPhase(TutorialPhase.Right, 15, 3));
                }

                    break;
                case 40:
                    {
                        yield return new WaitForSeconds(snippetTime);
                        yield return StartCoroutine(RunPhase(TutorialPhase.Left, 15, 3));
                    }

                    break;

                case 59:
                    {
                        yield return new WaitForSeconds(snippetTime);
                        yield return StartCoroutine(RunPhase(TutorialPhase.Panda, 15, 3));
                        yield return new WaitUntil(() => isCompleted(TutorialPhase.Panda));
                        Debug.Log("tutorial has been completed successfully");
                        StopAllCoroutines();
                       
                    }
                    break;


            }
            yield return null;

        }
    }

    IEnumerator RunPhase(TutorialPhase phase, float duration, int requiredPoints)
    {
        bool success = false;




        
        

        Debug.Log(phase.ToString() + " is the current phase");

        while (!success) // whole block runs until success
        {
            if (isCompleted(phase))
            {
                Debug.Log(phase.ToString() + "was completed before the loop");
                success = true;
                break;
            }

         
                comms.SetActive(false);
            

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
                Debug.Log(phase + " attempt failed. Restarting...");
                beatSpawner.Extermination();
                yield return new WaitForSeconds(2);
                tutorialRetryScreen.SetActive(true);
                retryLine.Play();
                yield return new WaitWhile(() => retryLine.isPlaying);
                tutorialRetryScreen.SetActive(false);
                perfect = 0;
                good = 0;
                Debug.Log("<color=red>Failed the current phase</color>" + "<b>Score resetting<b/>"
                    + "perfect: " + perfect +
                    " good: " + good);
                beatSpawner.RestartFromCheckpoint(); // restart music & beats
                
            }
        }
        Debug.Log("Number of perfects: " + perfect);
        Debug.Log("Number of good: " + good);
        perfect = 0;
        good = 0;
        MarkCompleted(phase);
        Debug.Log(phase.ToString() + " was completed!");
        comms.SetActive(true);

    }



    #endregion
}
