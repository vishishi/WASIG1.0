using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static UnityEngine.ParticleSystem;

public class Mug : NarrativeObjects
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
