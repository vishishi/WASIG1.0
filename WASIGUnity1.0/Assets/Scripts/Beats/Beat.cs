using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Beat : Interactable
{
    
    private ParticleSystem [] particles;
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
        var shapeMain = shape.main;
        var shapeEmission = shape.emission;
        var shapeShape = shape.shape;
        var shapeNoise = shape.noise;
        var shapeCOL = shape.colorOverLifetime;
        shapeMaterial = shape.GetComponent<ParticleSystemRenderer>().material;
        Color baseColor = shapeMaterial.GetColor("_Color");
        float startIntensity = GetCurrentHDRIntensity(baseColor);
        Color normalized = baseColor.linear / Mathf.Pow(2f, startIntensity);
        StartCoroutine(FadeBaseColor(shapeMaterial, normalized, startIntensity, targetIntensity, fadeDuration));

        myRigidBody.useGravity = true;
        //shapeCOL.color = new Color(0, 0, 0, 0);
       //shapeNoise.enabled = true;
        //shapeNoise.frequency = -0.1f;

        shapeMain.loop = false;
    
        

        Debug.Log("collided!" + other.gameObject.name);

    }


    IEnumerator FadeBaseColor(Material mat, Color baseColorNormalized, float fromIntensity, float toIntensity, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            float currentIntensity = Mathf.Lerp(fromIntensity, toIntensity, t);
            Color currentColor = baseColorNormalized * Mathf.Pow(2f, currentIntensity);

            mat.SetColor("_Color", currentColor);

            yield return null;
        }

        // Final precision set
        Color finalColor = baseColorNormalized * Mathf.Pow(2f, toIntensity);
        mat.SetColor("_Color", finalColor);
    }

    float GetCurrentHDRIntensity(Color hdrColor)
    {
        // Estimate HDR intensity assuming linear scaling
        float maxChannel = Mathf.Max(hdrColor.r, hdrColor.g, hdrColor.b);
        return Mathf.Log(maxChannel, 2f);
    }
}


