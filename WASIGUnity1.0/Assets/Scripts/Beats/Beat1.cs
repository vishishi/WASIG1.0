using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beat1 : Interactable
{
    public override void Interact()
    {

        Disable();

        //myImage.fillAmount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
