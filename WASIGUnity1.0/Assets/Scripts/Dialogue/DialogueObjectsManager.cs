using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueObjectsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] AudioSource dialogueAudioSource;

    //make an array of the snippet class
    public DialogueObjectSnippet[] dialogueObjectsSnippets;
    [HideInInspector]
    public int currentSnippetIndex = 0;
    public bool isTesting;

    public void StartConversation()
    {

        dialogueText.text = dialogueObjectsSnippets[currentSnippetIndex].dialogueSentenceEnglish;

    }

    public void NextDialogueSnippet()
    {
        if (currentSnippetIndex >= dialogueObjectsSnippets.Length - 1)
        {
            return;
        }


        else
        {

            //increase index number of snippets
            currentSnippetIndex++;



            //update text every time button is pressed
            dialogueText.text = dialogueObjectsSnippets[currentSnippetIndex].dialogueSentenceEnglish;
            

            //play audio clip
            dialogueAudioSource.Stop();
            dialogueAudioSource.clip = dialogueObjectsSnippets[currentSnippetIndex].snippetVA;
            dialogueAudioSource.Play();

           


        }


    }

}
