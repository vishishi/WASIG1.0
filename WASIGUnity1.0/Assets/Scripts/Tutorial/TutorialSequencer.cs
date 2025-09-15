using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequencer : MonoBehaviour
{
    public TutorialDialogueManager dialogueManager;
    public bool hasStarted = false;
    public GameObject comms;
    public TutorialScoreCounter scoreCounter;
    [HideInInspector] public float snippetTime;
    void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    // Update is called once per frame
    void Update()
    {
        snippetTime = dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime;
    }

    IEnumerator TutorialSequence()
    {
        yield return new WaitForSeconds(3);
        dialogueManager.StartConversation();
        yield return new WaitForSeconds(snippetTime);
        for (int i = 0; i < dialogueManager.dialogueSnippets.Length; i++)
        {
            yield return new WaitForSeconds(snippetTime);
            dialogueManager.NextDialogueSnippet();

            switch(i)
            {
                case 30:
                {
                hasStarted = true;
                yield return new WaitForSeconds(snippetTime);
                yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Right));
                

                }
                    break;
                case 40:
                    {
                        yield return new WaitForSeconds(snippetTime);
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Left));
                    }
                     break;
               
                case 59:
                    {
                        yield return new WaitForSeconds(snippetTime);
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Panda));
                    }
                    break;

                case 75:
                    {
                        yield return new WaitForSeconds(snippetTime);
                        yield return new WaitUntil(() => ChoiceManager.Instance.gesture1Selected || ChoiceManager.Instance.gesture2Selected || ChoiceManager.Instance.gesture3Selected);
                    }
                    break;

                case 84:
                    {
                        yield return new WaitForSeconds(snippetTime);
                        yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Panda));
                    }
                    break;
            }
            yield return null;

        }


    }
}
