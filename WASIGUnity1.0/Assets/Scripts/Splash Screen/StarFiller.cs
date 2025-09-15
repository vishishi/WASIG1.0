using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StarFiller : Interactable
{
    private Image starFiller;
    public Image emptyStar;
    public TextMeshProUGUI text;
    public AudioSource fillerSound;
    public SplashScreenSequencer sequencer;
    public ScreenFader screenFader;
    public float animationDuration;
    void Start()
    {
        starFiller = GetComponent<Image>();
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        myCollider.enabled = false;
        fillerSound.Play();
        StartCoroutine(FillStar(animationDuration));
        Debug.Log("couroutine has been called");
    }

    IEnumerator FillStar(float duration)
    {
        yield return new WaitForSeconds(1);
        float elapsedTime =0 ;


        
        while (elapsedTime < duration)

        {
           
            elapsedTime += Time.deltaTime;
            starFiller.fillAmount = Mathf.Clamp01(elapsedTime/duration);
            
            yield return null;

        }

        starFiller.enabled = false;
        emptyStar.enabled = false;

        screenFader.ChangeScene("ShikiBedroom");
        //Debug.Log("star image is disabled!");
        //text.text = "Choose your level";
        //sequencer.hasPointed = true;

        
       



    }
}
