using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


//This is a script attached to the beat prefabs, it handles the logic for the beats dissapearing and the particles bursting, it also tell the hand to change colors
//if they are hit with the correct hand (Left --> pink, right --> blue, both --> yellow)
public class Beat : Interactable
{
    public HandIdentity handIdentity;
    private ParticleSystem[] particles;
    private ParticleSystem shape;
    private ParticleSystem burst;

    private Rigidbody myRigidBody;

    private ColorIdentity colorID;

    private HandIdentity leftHand;
    private HandIdentity rightHand;

    [HideInInspector]
    public int hits;
    [HideInInspector]
  

    private Color color;
    [HideInInspector]
    public float targetIntensity = -2f;
    [HideInInspector]
    public float fadeDuration = 1.5f;




    void Start()
    {
        //Initialise variables
        myRigidBody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        colorID = GetComponent<ColorIdentity>();

        //Disable collider
        myCollider.enabled = false;

        //Coroutine for enable collider and find the two sets of particles
        StartCoroutine(EnableCollider());
        FindParticles();

 
    }

    public override void Interact(GameObject rayOrigin)
    {
        HandIdentity identity = rayOrigin.GetComponent<HandIdentity>();
        var shapeColor = shape.colorOverLifetime;

        hits++;

   
        HandIdentity leftHand = null;
        HandIdentity rightHand = null;

        var hands = FindObjectsByType<HandIdentity>(FindObjectsSortMode.None);
        foreach (var hand in hands)
        {
            if (hand.handType == HandType.Left) leftHand = hand;
            if (hand.handType == HandType.Right) rightHand = hand;
        }

// Method for changing color
        switch (identity.handType, colorID.colorid, hits)
        {
// FB Glowing pink
            case (HandType.Left, ColorID.Pink, _):
                Dissapear();

                StartCoroutine(ToggleBooleans(hand => hand.hasToGlowPink = true, hand => hand.hasToGlowPink = false));


                if (leftHand != null)
                    leftHand.hasToBeYellow = false;
                if (rightHand != null)
                    rightHand.hasToBeYellow = false;
                break;
// FB Glowing Blue
            case (HandType.Right, ColorID.Blue, _):
                Dissapear();

                StartCoroutine(ToggleBooleans(hand => hand.hasToGlowBlue = true, hand =>  hand.hasToGlowBlue = false));


                if (leftHand != null)
                    leftHand.hasToBeYellow = false;
                if (rightHand != null)
                    rightHand.hasToBeYellow = false;
                break;
//FB Glowing to yellow
            case (_, ColorID.Yellow, 2):
                Dissapear();

                StartCoroutine(ToggleBooleans(hand => hand.hasToBeYellow = true, hand => hand.hasToBeYellow = false));
                break;

            default:
                
                if (leftHand != null)
                    leftHand.hasToBeYellow = false;
                if (rightHand != null)
                    rightHand.hasToBeYellow = false;
                break;
        }
    }



    public void FindParticles()
    {
        //shape is the actual form of the prefab, burst is the hit feedback
        particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {

            if (particle.name == "Shape")
            {

                shape = particle;

            }

            if (particle.name == "Burst")
            {
                burst = particle;
            }


        }
    }

    public void OnTriggerEnter(Collider other)
    {
//destroy the beat prefabs once they hit the barrier
        Destroy(gameObject);


        Debug.Log("collided!" + other.gameObject.name);

    }

    IEnumerator EnableCollider()
//The collider is disable for 1.5 seconds to make it hitable in a shorter timeframe
    {
        yield return new WaitForSeconds(1.5f);
        myCollider.enabled = true;
    }

    public void Dissapear()
    {
        var shapeColor = shape.colorOverLifetime;
        shape.Stop();
        burst.Play();
        myCollider.enabled = false;
        shapeColor.enabled = true;
        shapeColor.color = new Color(0, 0, 0, 0);
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

//Coroutines that takes parameter for changing the booleans in the Hand Identity script (manages colors)  
    IEnumerator ToggleBooleans(Action<HandIdentity> setTrueAction, Action<HandIdentity> setFalseAction) 
    {
        HandIdentity leftHand = null;
        HandIdentity rightHand = null;



        var hands = FindObjectsByType<HandIdentity>(FindObjectsSortMode.None);
        foreach (var hand in hands)
        {
            if (hand.handType == HandType.Left) leftHand = hand;
            if (hand.handType == HandType.Right) rightHand = hand;
        }

        if (leftHand != null)setTrueAction(leftHand);
        if (rightHand != null)setTrueAction(rightHand);
           
        yield return new WaitForSeconds(0.2f);

        if (leftHand != null) setFalseAction(leftHand);
        if (rightHand != null) setFalseAction(rightHand);
            
    }
}

  

