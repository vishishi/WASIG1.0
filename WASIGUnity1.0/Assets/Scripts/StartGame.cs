using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : Interactable
{
    public AudioSource LOT;
    public Image hoverImage;
    public Image circleFill;
    private Color hoverColor;
    [HideInInspector]
    public bool hasStarted = false;
    

    // Start is called before the first frame update
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

        yield return new WaitUntil(() => fill.fillAmount > 0.99f);
        hoverImage.enabled = false;
        fill.fillAmount = 0f;
        hasStarted = true;
        LOT.Play();
        myCollider.enabled = false;
        myImage.enabled = false;

    }
}
