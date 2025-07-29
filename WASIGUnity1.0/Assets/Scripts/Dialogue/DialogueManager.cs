using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshPro dialogueText;
    [SerializeField] Button nextButton;
    [SerializeField] Button dialogueObject;

    //make an array of the snippet class
    [SerializeField] DialogueSnippet[] dialogueSnippets;
    int currentSnippetIndex = 0;

    //trigger this on a button or an interactable
    public void NextDialogueSnippet()
    {
        dialogueText.text = dialogueSnippets[currentSnippetIndex].dialogueSentenceEnglish;

        //when FMOD play monologue sound by key here

        //if we have reached the end of the array
        if (currentSnippetIndex >= dialogueSnippets.Length)
        {
            Debug.Log("End of Dialogue");
        }

    }



}
