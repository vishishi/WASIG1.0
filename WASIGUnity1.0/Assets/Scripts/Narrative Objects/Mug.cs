using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Mug : NarrativeObjects
{

    void Start()
    {
        audio = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
