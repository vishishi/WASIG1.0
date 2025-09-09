using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhosMakingThatNoise : MonoBehaviour
{
    public AudioSource[] noises; 
    void Start()
    {
        noises = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var noise in noises)
        {
            if (noise.isPlaying && noise.name != "BGM")
            {
                Debug.Log(noise.name + " is being a twat");
            }
        }
    }
}
