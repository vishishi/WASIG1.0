using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSequencer : MonoBehaviour
{
    public Animator bedAnimator;
    public Animator narrationAnimator;
    public DialogueObjectsManager dialogueObjectsManager;
    public GameObject canvas;
    void Start()
    {
        StartCoroutine(Sequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(5);
        narrationAnimator.SetBool("hasTouched", true);
        yield return new WaitForSeconds(3);
        dialogueObjectsManager.StartConversation();
        yield return new WaitForSeconds(dialogueObjectsManager.dialogueObjectsSnippets[dialogueObjectsManager.currentSnippetIndex].snippetTime);

        foreach (var snippets in dialogueObjectsManager.dialogueObjectsSnippets)
        {
            dialogueObjectsManager.NextDialogueSnippet();
            yield return new WaitForSeconds(dialogueObjectsManager.dialogueObjectsSnippets[dialogueObjectsManager.currentSnippetIndex].snippetTime);


        }

        narrationAnimator.SetBool("hasRead", true);
        yield return new WaitForSeconds(5);
        bedAnimator.SetTrigger("finishBed");
        yield return new WaitForSeconds(20);
        canvas.SetActive(false);



    }
}
