using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniform : NarrativeObjects
{
    // Start is called before the first frame update
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
