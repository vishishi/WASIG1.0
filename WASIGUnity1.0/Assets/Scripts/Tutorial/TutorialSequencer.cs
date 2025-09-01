using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequencer : MonoBehaviour
{
    public TutorialDialogueManager dialogueManager;
    public bool hasStarted = false;
    public GameObject comms;
    void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TutorialSequence()
    {
        dialogueManager.StartConversation();
        yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
        for (int i = 0; i < dialogueManager.dialogueSnippets.Length; i++)
        {
            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
            dialogueManager.NextDialogueSnippet();

            switch(i)
            {
                case 3:
                {
                yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
                hasStarted = true;

                }
            break;
            }

        }


    }
}
