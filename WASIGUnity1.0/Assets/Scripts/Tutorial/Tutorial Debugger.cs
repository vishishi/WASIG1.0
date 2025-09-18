using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDebugger : MonoBehaviour
{
    public TutorialScoreCounter scoreCounter;
    public ChoiceManager choiceManager;
    public TutorialDialogueManager dialogueManager;
    private int phases;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Debugger")
        {
            switch (phases)
            {
                case 1:
                    {
                        scoreCounter.MarkCompleted(TutorialPhase.Right);
                    }


                    break;

                case 2:
                    {
                        scoreCounter.MarkCompleted(TutorialPhase.Left);
                    }

                    break;

                case 3:
                    {
                        scoreCounter.MarkCompleted(TutorialPhase.Panda);
                    }

                    break;
                case 4:
                    {
                        choiceManager.gesture2Selected = true;
                    }

                    break;

                case 5:
                    {
                        scoreCounter.MarkCompleted(TutorialPhase.Gesture);
                    }
                    break;


            }

   
        }

        else if (gameObject.name == "Debugger 2")
        {
            switch (phases)
            {
                case 1:
                    {
                        dialogueManager.currentSnippetIndex = 138;
                        Debug.Log("FFS index is" + dialogueManager.currentSnippetIndex);
                       
                    }


                    break;

                case 2:
                    {
                        dialogueManager.currentSnippetIndex = 40;
                    
                    }

                    break;

                case 3:
                    {
                        dialogueManager.currentSnippetIndex = 30;
                        
                    }

                    break;
                case 4:
                    {
                        dialogueManager.currentSnippetIndex = 59;
                        
                    }

                    break;

                case 5:
                    {
                        dialogueManager.currentSnippetIndex = 76;

                    }
                    break;


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        phases++;
        scoreCounter.perfect = 5;
        Debug.Log("Current phase count: " + phases.ToString());
    }

    private void OnTriggerExit(Collider other)
    {
        scoreCounter.perfect = 0;
    }
}
