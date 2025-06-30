using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : Interactable
{
    public AudioSource LOT;
    [HideInInspector]
    public bool hasStarted = false;
    

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        hasStarted = false;
        myImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Interact called: starting game");
        LOT.Play();
        myCollider.enabled = false;
        hasStarted = true;
        myImage.enabled = false;
    }
}
