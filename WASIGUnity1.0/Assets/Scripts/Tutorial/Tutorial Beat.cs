using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeat : Interactable
{ 

    [System.Serializable]

    public struct AccuracyColorPair
    {
        public Accuracy accuracy;
        [ColorUsage(true, true)] public Color color;
    }

    public HandIdentity handIdentity;
    private ParticleSystem[] particles;
    private ParticleSystem shape;
    private ParticleSystem burst;

    private Rigidbody myRigidBody;

    private ColorIdentity colorID;

    private HandIdentity leftHand;
    private HandIdentity rightHand;
    private TutorialScoreCounter scoreCounter;

    [HideInInspector]
    public int hits;
    [HideInInspector]
    public float hitAcc;

    private Color color;
    [HideInInspector]
    public float targetIntensity = -2f;
    [HideInInspector]
    public float fadeDuration = 1.5f;

    private float spawnTime;
    private float hitTime;
    [HideInInspector]
    public Vector3 beatLocation;


    public AccuracyColorPair[] accuracyColors;
    private Dictionary<Accuracy, Color> accuracyColorMap;

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
        spawnTime = Time.time;
        scoreCounter = FindAnyObjectByType<TutorialScoreCounter>();





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
    IEnumerator EnableCollider()
    //The collider is disable for 1.5 seconds to make it hitable in a shorter timeframe
    {
        yield return new WaitForSeconds(1.1f);
        myCollider.enabled = true;
    }
    public override void Interact(GameObject rayOrigin)
    {

        beatLocation = gameObject.transform.position;

        HandIdentity identity = rayOrigin.GetComponent<HandIdentity>();
        //Vairables for burst particles to be used in score
        var shapeColor = shape.colorOverLifetime;
        var burstCount = burst.emission.burstCount;

        hits++;



        //Variables for hand color change   
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

                StartCoroutine(ToggleBooleans(hand => hand.hasToGlowBlue = true, hand => hand.hasToGlowBlue = false));


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
        //Method for adding score and spawming particle amounts accordingly

        float actualHitTime = Time.time;
        Accuracy result = CalculateAccuracy(actualHitTime);

        if (scoreCounter.isSuperCharged)
        {
            switch (result)
            {
                case Accuracy.Perfect:
           
                    burstCount = 100;
                    scoreCounter.perfect++;
                    Debug.Log("Perfect!");
                    break;
                case Accuracy.Good:
                    scoreCounter.score += 100;
                    burstCount = 75;
                    scoreCounter.good++;
                    Debug.Log("Good!");
                    break;
                case Accuracy.Bad:
                    scoreCounter.score += 50;
                    burstCount = 75;
                    scoreCounter.good++;
                    Debug.Log("Bad!");
                    break;
                case Accuracy.Miss:
                    scoreCounter.miss++;
                    Debug.Log("Miss!");
                    break;

            }
        }

        else
        {
            switch (result)
            {
                case Accuracy.Perfect:
                  //  scoreCounter.score += 100;
                    burstCount = 100;
                    //scoreCounter.perfect++;
                    Debug.Log("Perfect!");
                    break;
                case Accuracy.Good:
                    //scoreCounter.score += 75;
                    burstCount = 75;
                    //scoreCounter.good++;
                    Debug.Log("Good!");
                    break;
                case Accuracy.Bad:
                    //scoreCounter.score += 25;
                    burstCount = 75;
                    //scoreCounter.good++;
                    Debug.Log("Bad!");
                    break;
                case Accuracy.Miss:
                    //scoreCounter.miss++;
                    Debug.Log("Miss!");
                    break;

            }


        }
    }



    public void OnTriggerEnter(Collider other)
    {
        float timeToHit = Time.time - spawnTime;  // how long this beat has traveled
        Debug.Log("Time to hit wall: " + timeToHit.ToString("F2") + " seconds");

        scoreCounter.miss++;
        Destroy(gameObject);




        Debug.Log("collided!" + other.gameObject.name);

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
    public Accuracy CalculateAccuracy(float actualHitTime)
    {
        float idealHitTime = spawnTime + 2.7f;
        float offset = Mathf.Abs(actualHitTime - idealHitTime);

        if (offset <= 0.05f)
            return Accuracy.Perfect;
        else if (offset <= 0.10f)
            return Accuracy.Good;
        else if (offset <= 0.25f)
            return Accuracy.Bad;
        else
            return Accuracy.Miss;
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

        if (leftHand != null) setTrueAction(leftHand);
        if (rightHand != null) setTrueAction(rightHand);

        yield return new WaitForSeconds(0.2f);

        if (leftHand != null) setFalseAction(leftHand);
        if (rightHand != null) setFalseAction(rightHand);

    }
}
