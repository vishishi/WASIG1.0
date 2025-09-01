using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem.Utilities;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] AudioSource dialogueAudioSource;
    [SerializeField] Image characterPortrait;
    [SerializeField] Sprite shikiSprite;
    [SerializeField] Sprite makiSprite;

    //[SerializeField] Button nextButton;
    //[SerializeField] GameObject dialogueObjectButton;
    //[SerializeField] GameObject dialogueScriptObject;
    int numberOfDialogueObjects = 8;

    //make an array of the snippet class
    public DialogueSnippet[] dialogueSnippets;
    [HideInInspector]
    public int currentSnippetIndex = 0;
    public bool isTesting;



    private void Start()
    {
        //nextButton.onClick.RemoveAllListeners();
        //nextButton.onClick.AddListener(NextDialogueSnippet);
    }

    private void Update()
    {


        /*if (numberOfDialogueObjects > 0)
        {
            Debug.Log("there are no more dialogue objects");
        }*/
    }

    public void StartConversation()
    {
      
        dialogueText.text = dialogueSnippets[currentSnippetIndex].dialogueSentenceEnglish;
        if (characterNameText != null)
        {
            characterNameText.text = dialogueSnippets[currentSnippetIndex].characterName;
        }
        dialogueAudioSource.Stop();
        dialogueAudioSource.clip = dialogueSnippets[currentSnippetIndex].snippetVA;
        dialogueAudioSource.Play();
        if (dialogueSnippets[currentSnippetIndex].characterName == "Shiki")
        {
            characterPortrait.sprite = shikiSprite;
        }

        else if ((dialogueSnippets[currentSnippetIndex].characterName == "Maki"))
        {
            characterPortrait.sprite = makiSprite;
        }

        else
        {
            return;
        }
        //dialogueObjectButton.SetActive(false);

    }
    

    
    //trigger this on a button or an interactable
    public void NextDialogueSnippet()
    {
        if (currentSnippetIndex >= dialogueSnippets.Length-1)
        {
            return;
        }
        

        else
        {
          
            //increase index number of snippets
            currentSnippetIndex++;

   

            //update text every time button is pressed
            dialogueText.text = dialogueSnippets[currentSnippetIndex].dialogueSentenceEnglish;
            characterNameText.text = dialogueSnippets[currentSnippetIndex].characterName;

            //play audio clip
            dialogueAudioSource.Stop();
            dialogueAudioSource.clip = dialogueSnippets[currentSnippetIndex].snippetVA;
            dialogueAudioSource.Play();
            
            if (dialogueSnippets[currentSnippetIndex].characterName == "Shiki")
            {
                characterPortrait.sprite = shikiSprite;
            }

            else if ((dialogueSnippets[currentSnippetIndex].characterName == "Maki"))
            {
                characterPortrait.sprite = makiSprite;
            }



        }


    }

    void EndThisDialogue()
    {
        Debug.Log("End of Dialogue");
        //numberOfDialogueObjects--;
        //dialogueScriptObject.SetActive(false);

    }



}
