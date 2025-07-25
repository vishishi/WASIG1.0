using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Parent class with tho functions: one abstract and one virtual. The abstract must be implemented on the child scripts and it's meant to be overriden, it cannot contain any parameters
//or logic, since it's meant to be overriden. The virtual can indeed be overriden, in this case, specifically allows to use HandIdentity in the interactable children script.
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

    public virtual void Interact(GameObject rayOrigin)
    {
        Interact();
    }

    public void Disable()
    {
        myImage.fillAmount = 0;
    }



}
