using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequencer : MonoBehaviour
{
    public TutorialDialogueManager dialogueManager;
    public TutorialBGMAudio bgmAudioManager;
    public bool hasStarted = false;
    [HideInInspector] public bool selectionMade = false;
    public GameObject comms;
    public GameObject poseSelector;
    public GameObject gestures;
    [HideInInspector] public float snippetTime;

    [Header("References")]
    public TutorialScoreCounter scoreCounter;
    public ChoiceManager choiceManager;
    public ScreenFader screenFader;
    public WatchInteractor watchInteractor;
    public TutorialSelectGesture gestureSelector;

    void Start()
    {
        StartCoroutine(TutorialSequence());
        StartCoroutine(ReachEnding());
        poseSelector.SetActive(false);
        gestures.SetActive(false);  
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

                //case 2:
                //    {
                //        bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack1);
                //        Debug.Log("case 2 audio is playing!");
                        
                //    }
                //    break;




                case 3:
                    {
                        
                        watchInteractor.ReceiveCall();
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet time as " + snippetTime.ToString());
                        yield return new WaitUntil(() => watchInteractor.hasLooked);
                       

                    }

                    break;
                case 10:
                    {
                        bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack1);
                    }
                    break;
                case 29:
                {

                        bgmAudioManager.StopTutorialBGMTrack();
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet time as " + snippetTime.ToString());
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Right));
                        bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack1);



                    }
                    break;
                case 39:
                    {
                        bgmAudioManager.StopTutorialBGMTrack();
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
                        Debug.Log(" <color=#FFFF00> Sequencer: </color> " + "read the snippet time as " + snippetTime.ToString());
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Left));
                        bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack1);
                    }
                     break;
               
                case 58:
                    {
                        bgmAudioManager.StopTutorialBGMTrack();
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Panda));
                        bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack1);
                    }
                    break;

                case 75:
                    {
                        bgmAudioManager.StopTutorialBGMTrack();
                        gestures.SetActive(true);
                        comms.SetActive(false);
                        poseSelector.SetActive(true);
                        yield return new WaitUntil(() => selectionMade);
                        Destroy(poseSelector);
                        gestures.SetActive(false);
                        comms.SetActive(true);
                        bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack2);
                    }
                    break;

                case 80:
                    {
                        bgmAudioManager.StopTutorialBGMTrack();
                    }
                    break;

                case 81:
                    {
                        bgmAudioManager.PlayTutorialBGMTrack(bgmAudioManager.tutorialBGMtrack3);
                    }
                    break;


  
                    
            }
        }


    }

    IEnumerator ReachEnding()
    {
        while (true)
        {
            if (dialogueManager.currentSnippetIndex == 138)
            {
                Debug.Log("reached the end of the snippets!");
                bgmAudioManager.StopTutorialBGMTrack();
                screenFader.ChangeScene("Main");
                StopAllCoroutines();
            }
            yield return null;
        }
        
    }
}
