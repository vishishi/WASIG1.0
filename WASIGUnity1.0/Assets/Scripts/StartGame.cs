using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : Interactable
 //Another interactable that starts the game currently by enabling the Beat Spam Spawner script. Currently a placeholder as there shouldn't be an actual start button in the game
{
   // public AudioSource LOT;
    public Image hoverImage;
    public Image circleFill;
    private Color hoverColor;
    [HideInInspector]
    public bool hasStarted = false;
    public BeatMapSpawner spawner;


    private void Awake()
    {
        spawner.enabled = false;
    }
    void Start()
    {
        myCollider = GetComponent<Collider>();
        hasStarted = false;
        myImage = GetComponent<Image>();
        hoverColor = new Color(hoverImage.color.r, hoverImage.color.g, hoverImage.color.b, 0.2f);
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Interact called: starting game");
        StartCoroutine(StartGemu(circleFill, 1f));
        hoverImage.color = hoverColor;
    }

    IEnumerator StartGemu(Image fill, float duration)
    {
        float elapsed = 0f;
        fill.fillAmount = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fill.fillAmount = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        yield return new WaitUntil(() => fill.fillAmount > 0.98f);
        myCollider.enabled = false;
        hoverImage.enabled = false;
        fill.fillAmount = 0f;
        spawner.enabled = true;
        //LOT.Play();
       
        myImage.enabled = false;

    }
}
