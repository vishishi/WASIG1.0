using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DialogueObjectSnippet
{


    [TextArea(3, 10)]
    public string dialogueSentenceEnglish;
    public AudioClip snippetVA;
    public float snippetTime;
    

}
