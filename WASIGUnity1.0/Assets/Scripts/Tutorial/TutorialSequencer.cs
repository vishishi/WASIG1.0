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
                case 7:
                {
                yield return new WaitForSeconds(snippetTime);
                yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Right));
                

                }
                    break;
                case 10:
                    {
                        yield return new WaitForSeconds(snippetTime);
                            yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Left));
                    }
                     break;
            }
            yield return null;

        }


    }
}
