using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeColliders : MonoBehaviour
{
    public NarrativeObjects narrativeObject;
    private Collider myCollider;
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            narrativeObject.Signpost();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            narrativeObject.UnSignpost();
            myCollider.enabled = false;
          
        }
    }
}
