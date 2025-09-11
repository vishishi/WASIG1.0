using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DialogueSnippet
{


    [TextArea(3, 10)]
    public string dialogueSentenceEnglish;
    public AudioClip snippetVA;
    public float snippetTime;
    public string characterName;
    public float snippetPoint;



    //when we put in FMOD add a string for the audio key

}
