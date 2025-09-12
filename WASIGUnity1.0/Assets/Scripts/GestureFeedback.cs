using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureFeedback : MonoBehaviour
{
    public ParticleSystem sparkles;
    public bool isGesturing = false;

    void Awake()
    {
        sparkles = gameObject.GetComponentInChildren <ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respond()
    {
        sparkles.Play();
    }
}
