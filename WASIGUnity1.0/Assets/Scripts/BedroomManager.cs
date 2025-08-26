using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedroomManager : MonoBehaviour
{
    public int sceneChanger = 0;
    public WatchInteractor watchInteractor;
    public GameObject comms;
    void Start()
    {
        StartCoroutine(ChangeSequence());
    }

    
    void Update()
    {
        
    }

    IEnumerator ChangeSequence()
    {
        while (true)
        {
            if(sceneChanger == 5)
            watchInteractor.ReceiveCall();
            yield return null;
            break;

        }

        comms.SetActive(true);
 


        
    }
}
