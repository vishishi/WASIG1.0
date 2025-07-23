using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Beat : Interactable
{

    private ParticleSystem[] particles;
    private ParticleSystem shape;

    private ParticleSystem burst;

    private Rigidbody myRigidBody;

    private Material shapeMaterial;

    public float targetIntensity = -2f;
    public float fadeDuration = 1.5f;

    public override void Interact()
    {
        var shapeColor = shape.colorOverLifetime;
        shape.Stop();
        burst.Play();
        myCollider.enabled = false;
        shapeColor.enabled = true;
        shapeColor.color = new Color(0, 0, 0, 0);

    }

    void Start()
    {
        FindParticles();
        myRigidBody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        myCollider.enabled = false;
        StartCoroutine(EnableCollider());


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FindParticles()
    {
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

        myRigidBody.useGravity = true;


        //shapeMain.loop = false;



        Debug.Log("collided!" + other.gameObject.name);

    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);
        myCollider.enabled = true;
    }

}

  

