using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDebugger : MonoBehaviour
{
    public TutorialScoreCounter scoreCounter;
    private int phases;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(phases)
        {
            case 1:
                scoreCounter.MarkCompleted(TutorialPhase.Right);
               

                break;

            case 2:
                scoreCounter.MarkCompleted(TutorialPhase.Left);
            
                break;

            case 3:
                scoreCounter.MarkCompleted(TutorialPhase.Panda);
              
                break;
            case 4:
                scoreCounter.MarkCompleted(TutorialPhase.Gesture);
            
                break;

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
