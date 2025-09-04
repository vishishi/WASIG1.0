using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSingleton : MonoBehaviour
{

    public ChoiceManager choiceManagerPrefab;
    void Awake()
    {
        if (ChoiceManager.Instance == null)
        {
            Instantiate(choiceManagerPrefab);
            choiceManagerPrefab.MarkGestureAsSelected("Gesture 2");
        }
    }

    
    void Update()
    {
        
    }
}
