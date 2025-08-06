using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Button nextButton;
    [SerializeField] GameObject dialogueObjectButton;
    [SerializeField] GameObject dialogueScriptObject;
    int numberOfDialogueObjects = 8;

    //make an array of the snippet class
    [SerializeField] DialogueSnippet[] dialogueSnippets;
    int currentSnippetIndex = 0;



    private void Start()
    {
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(NextDialogueSnippet);
    }

    private void Update()
    {


        if (numberOfDialogueObjects > 0)
        {
            Debug.Log("there are no more dialogue objects");
        }
    }

    public void StartConversation()
    {
        dialogueText.text = dialogueSnippets[currentSnippetIndex].dialogueSentenceEnglish;
        dialogueObjectButton.SetActive(false);

    }
    

    
    //trigger this on a button or an interactable
    void NextDialogueSnippet()
    {
        //increase index number of snippets
        currentSnippetIndex++;

        //update text every time button is pressed
        dialogueText.text = dialogueSnippets[currentSnippetIndex].dialogueSentenceEnglish;

        //when FMOD play monologue sound by key here

        //if we have reached the end of the array
        if (currentSnippetIndex >= dialogueSnippets.Length)
        {
            EndThisDialogue();
        }

    }

    void EndThisDialogue()
    {
        Debug.Log("End of Dialogue");
        numberOfDialogueObjects--;
        dialogueScriptObject.SetActive(false);

    }



}
