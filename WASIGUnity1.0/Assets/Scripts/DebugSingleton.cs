using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSingleton : MonoBehaviour
{

    public ChoiceManager choiceManagerPrefab;
    void Start()
    {
        if (ChoiceManager.Instance == null)
        {
            Instantiate(choiceManagerPrefab);
            choiceManagerPrefab.gesture2Selected = true;
        }
    }

    
    void Update()
    {
        
    }
}
