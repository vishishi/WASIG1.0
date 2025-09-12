using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSingleton : MonoBehaviour
{

    public ChoiceManager choiceManagerPrefab;
    private ChoiceManager choiceManager;
    void Awake()
    {
       // if (ChoiceManager.Instance == null)
        //{
           // Instantiate(choiceManagerPrefab);
            //choiceManagerPrefab.gesture3Selected = true;

            choiceManager = GetComponent<ChoiceManager>();
            choiceManager.MarkGestureAsSelected("Gesture 2");
        //}
    }

    
    void Update()
    {
        
    }
}
