using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;


public class Computer : NarrativeObjects
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
