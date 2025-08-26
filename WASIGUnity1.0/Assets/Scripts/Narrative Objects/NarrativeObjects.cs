using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 abstract public class NarrativeObjects : MonoBehaviour
{
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public ParticleSystem particles;
    [HideInInspector]
    public Canvas canvas;
    [HideInInspector]
    public Image[] images;
    [HideInInspector]
    public Animator animator;
    public GameObject touchVFX;
    public BedroomManager bedroomManager;
    [HideInInspector]
    public Collider myCol;
   void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Signpost()
    {
        audioSource.Play();
        particles.Play();
        Debug.Log(gameObject.name + " has started signposting!");
    }

    public virtual void UnSignpost()
    {
        audioSource.Stop(); particles.Stop();
        Debug.Log (gameObject.name + " has stopped signposting!");
    }

    public virtual void PlayAnimation(Vector3 touchLocation)
    {
        
        StartCoroutine(StartAnimation(touchLocation));

    }


    IEnumerator StartAnimation(Vector3 touchLocation)
    {
        //bedroomManager.sceneChanger++;
        myCol.enabled = false;
        Instantiate(touchVFX, touchLocation, Quaternion.Euler(0f, 0f, 90f));
        yield return new WaitUntil(() => !particles.isPlaying);
        animator.SetBool("hasTouched", true);
        yield return new WaitForSeconds (5);
        if (gameObject.name == "Window")
        {
            animator.SetBool("hasRead", false);
        }
        else
        {
            animator.SetBool("hasRead", true);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hand")) return;

        Debug.Log("Successful hands!");

        UnSignpost();

        // Approximate contact point for TRIGGERS:
        Vector3 pointOnMe = myCol ? myCol.ClosestPoint(other.bounds.center) : transform.position;
        Vector3 pointOnThem = other.ClosestPoint(pointOnMe);
        Vector3 spawnPos = (pointOnMe + pointOnThem) * 0.5f;

        PlayAnimation(spawnPos);
    }
}
