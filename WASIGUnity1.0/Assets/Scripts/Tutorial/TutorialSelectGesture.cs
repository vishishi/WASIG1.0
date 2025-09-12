using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSelectGesture : MonoBehaviour
{
    private Animator gestureFiller;
    void Start()
    {
        gestureFiller = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillImage()
    {
        gestureFiller.SetBool("isGesture", true);
        Debug.Log("Gesture animator is playing in " + gameObject.name);
    }
}
