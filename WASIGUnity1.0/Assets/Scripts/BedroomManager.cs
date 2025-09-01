using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BedroomManager : MonoBehaviour
{
    public int sceneChanger = 0;
    public WatchInteractor watchInteractor;
    public GameObject comms;
    public TextMeshProUGUI counter;
    public GameObject[] text;  
    public DialogueManager dialogueManager;

    void Start()
    {
        StartCoroutine(ChangeSequence());
        comms.SetActive(false);
    }

    
    void Update()
    {
        counter.text = sceneChanger.ToString();
    }

    IEnumerator ChangeSequence()
    {
        while (true)
        {
            yield return new WaitUntil(() => sceneChanger == 5);
            
                watchInteractor.ReceiveCall();
            
            yield return new WaitUntil(() => watchInteractor.hasLooked);
            yield return new WaitForSeconds(5);

            comms.SetActive(true);
            Debug.Log("Comms set active!");
            yield return new WaitForSeconds(10);
            dialogueManager.StartConversation();
            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);

            foreach (var snippets in dialogueManager.dialogueSnippets)
            {
                yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
                dialogueManager.NextDialogueSnippet();

            }






            yield return null; 

        }

    }
}
