using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 abstract public class NarrativeObjects : MonoBehaviour
{
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public AudioSource touchSound;
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
    public DialogueManager dialogueManager;
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

    public virtual void FindComponents()
    {
        audioSource = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
        canvas = GetComponentInChildren<Canvas>();
        images = canvas.GetComponentsInChildren<Image>();
        animator = GetComponentInChildren<Animator>();
        myCol = GetComponent<Collider>() ?? GetComponentInChildren<Collider>();
        dialogueManager = GetComponentInChildren<DialogueManager>();
        touchSound = GetComponentInChildren<AudioSource>();
    }


    IEnumerator StartAnimation(Vector3 touchLocation)
    {

        myCol.enabled = false;
        if (gameObject.name == "Books" || gameObject.name == "Computer")
        {
            Instantiate(touchVFX, touchLocation, Quaternion.Euler(0, -90, 90));
        }

        else
        {
            Instantiate(touchVFX, touchLocation, Quaternion.Euler(0f, 0f, 90f));
        }

        yield return new WaitUntil(() => !particles.isPlaying);
        animator.SetBool("hasTouched", true);
        Debug.Log(gameObject.name + " is playing now");
        yield return new WaitForSeconds(3);
        dialogueManager.StartConversation();
        yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);

        foreach (var snippets in dialogueManager.dialogueSnippets)
        {
            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
            dialogueManager.NextDialogueSnippet();

        }

        animator.SetBool("hasRead", true);

        yield return new WaitForSeconds(5);
        bedroomManager.sceneChanger++;
    }
            

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hand")) return;

        Debug.Log("Successful hands!");

        UnSignpost();
        touchSound.Play();

        // Approximate contact point for TRIGGERS:

        Vector3 pointOnMe = myCol ? myCol.ClosestPoint(other.bounds.center) : transform.position;
        Vector3 pointOnThem = other.ClosestPoint(pointOnMe);
        Vector3 spawnPos = (pointOnMe + pointOnThem) * 0.5f;

        PlayAnimation(spawnPos);
    }
}
