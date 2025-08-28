using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerAid : MonoBehaviour
{
    public BedroomManager BedroomManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Hand"))
        {
            BedroomManager.sceneChanger++;
        }
    }
}
