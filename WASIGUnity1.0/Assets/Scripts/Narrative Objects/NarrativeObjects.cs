using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 abstract public class NarrativeObjects : MonoBehaviour
{
    [HideInInspector]
    public AudioSource audio;
    [HideInInspector]
    public ParticleSystem particles;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Signpost()
    {
        audio.Play();
        particles.Play();
        Debug.Log(gameObject.name + " has started signposting!");
    }

    public virtual void UnSignpost()
    {
        audio.Stop(); particles.Stop();
        Debug.Log (gameObject.name + " has stopped signposting!");
    }
}
