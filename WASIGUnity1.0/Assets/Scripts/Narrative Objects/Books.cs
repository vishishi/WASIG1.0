using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Books : NarrativeObjects
{
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
        canvas = GetComponentInChildren<Canvas>();
        images = canvas.GetComponentsInChildren<Image>();
        animator = GetComponentInChildren<Animator>();
        myCol = GetComponent<Collider>() ?? GetComponentInChildren<Collider>();
        dialogueManager = GetComponentInChildren<DialogueManager>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
