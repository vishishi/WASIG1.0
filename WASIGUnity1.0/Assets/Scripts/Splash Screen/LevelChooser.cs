using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChooser : Interactable
{
    public SplashScreenSequencer sequencer;
    public Image horizontalSlice;
    void Start()
    {
        myCollider = GetComponent<Collider>();
        
        Debug.Log ("slice name is" +  horizontalSlice.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        myCollider.enabled = false;

        if (gameObject.name == "Rhythm")
        {
            StartCoroutine(Fill(1));
            sequencer.choseRythm = true;
            Debug.Log("chose rhythm scene!");
        }

        else if (gameObject.name == "Narrative")
        { 

            StartCoroutine(Fill(1));
            sequencer.choseNarra = true;
            Debug.Log("chose narrative scene!");
        }

    }

    IEnumerator Fill(float duration)
    {
        
        float elapsedTime = 0;



        while (elapsedTime < duration)

        {

            elapsedTime += Time.deltaTime;
            horizontalSlice.fillAmount = Mathf.Clamp01(elapsedTime / duration);

            yield return null;

        }
    }
}
