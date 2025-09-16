using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSelectGesture : MonoBehaviour
{
    private Animator gestureFiller;
    public TutorialSequencer sequencer;
    public TutorialScoreCounter scoreCounter;    
    void Start()
    {
        gestureFiller = GetComponent<Animator>();
        if (gestureFiller != null)
        {
            Debug.Log("gesture select animator found!");
        }

        else
        {
            Debug.Log("where the hell is it??");
        }

            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillImage()
    {
        if (gestureFiller != null)
        {
            gestureFiller.SetBool("isGesture", true);
        }
        Debug.Log("Gesture animator is playing in " + gameObject.name);
    }

    public void InformSequencer()
    {
        if (!sequencer.selectionMade)
        {
            sequencer.selectionMade = true;
            Debug.Log("communicated to sequencer!");
        }

        else if (sequencer.selectionMade) 
        {
            scoreCounter.MarkCompleted(TutorialPhase.Gesture);
            Debug.Log("animation was filled and gesture phase was over heeny");
        }
    }
}
