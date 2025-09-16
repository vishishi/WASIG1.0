using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequencer : MonoBehaviour
{
    public TutorialDialogueManager dialogueManager;
    public TutorialBGMAudio bgmAudioManager;
    public bool hasStarted = false;
    public GameObject comms;
    public GameObject poseSelector;
    [HideInInspector] public float snippetTime;

    [Header("References")]
    public TutorialScoreCounter scoreCounter;
    public ChoiceManager choiceManager;
    public ScreenFader screenFader;
    public WatchInteractor watchInteractor;

    void Start()
    {
        StartCoroutine(TutorialSequence());
        StartCoroutine(ReachEnding());
        poseSelector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        snippetTime = dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime;
    }

    IEnumerator TutorialSequence()
    {
       // yield return new WaitForSeconds(3);
        dialogueManager.StartConversation();
       yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
        for (int i = 0; i < dialogueManager.dialogueSnippets.Length; i++)
        {
  
            dialogueManager.NextDialogueSnippet();
            Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
            Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet time as " + snippetTime.ToString());
            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);

            switch (i)
            {

                /*case 2:
                    {
                        bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack1);
                        break;
                    } */
                    



                case 4:
                    {
                        
                        watchInteractor.ReceiveCall();
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet time as " + snippetTime.ToString());
                        yield return new WaitUntil(() => watchInteractor.hasLooked);
                        //bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack1);

                    }

                    break;
                case 29:
                {

                        //yield return new WaitForSeconds(snippetTime);
                        //bgmAudioManager.StopTutorialBGMTrack();
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet time as " + snippetTime.ToString());
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Right));



                    }
                    break;
                case 39:
                    {
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet time as " + snippetTime.ToString());
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Left));
                    }
                     break;
               
                case 58:
                    {
                        //yield return new WaitForSeconds(snippetTime);
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Panda));
                    }
                    break;

                case 75:
                    {
                        //yield return new WaitForSeconds(snippetTime);
                        comms.SetActive(false);
                        poseSelector.SetActive(true);
                        yield return new WaitUntil(() => (choiceManager.gesture1Selected || choiceManager.gesture2Selected || choiceManager.gesture3Selected));
                        poseSelector.SetActive(false);
                        comms.SetActive(true);
                    }
                    break;

                case 83:
                {

                  
                    yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Gesture));
                }

                break;
                               
                    
            }

            if (dialogueManager.currentSnippetIndex >= dialogueManager.dialogueSnippets.Length)
            {
                Debug.Log("reached the end of the snippets!");
                screenFader.ChangeScene("Main");
                StopAllCoroutines();
            }
            yield return null;

        }


    }

    IEnumerator ReachEnding()
    {
        while (true)
        {
            if (dialogueManager.currentSnippetIndex == 152)
            {
                Debug.Log("reached the end of the snippets!");
                screenFader.ChangeScene("Main");
                StopAllCoroutines();
            }
            yield return null;
        }
    }
}
