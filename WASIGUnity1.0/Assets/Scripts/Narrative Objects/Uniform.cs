using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uniform : NarrativeObjects
{
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
        canvas = GetComponentInChildren<Canvas>();
        images = canvas.GetComponentsInChildren<Image>();
        animator = GetComponentInChildren<Animator>();
        myCol = GetComponent<Collider>() ?? GetComponentInChildren<Collider>();
    }

    void Update()
    {
        
    }
}
