using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Collider myCollider;
    [HideInInspector]
    public Image myImage;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Interact();

    public void Disable()
    {
        myImage.fillAmount = 0;
    }



}
