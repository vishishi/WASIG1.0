using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BedroomManager : MonoBehaviour
{
    public int sceneChanger = 0;
    public WatchInteractor watchInteractor;
    public GameObject comms;
    public TextMeshProUGUI counter;
    public GameObject[] text;  
    void Start()
    {
        StartCoroutine(ChangeSequence());
        comms.SetActive(false);
    }

    
    void Update()
    {
        counter.text = sceneChanger.ToString();
    }

    IEnumerator ChangeSequence()
    {
        while (true)
        {
            yield return new WaitUntil(() => sceneChanger == 5);
            
                watchInteractor.ReceiveCall();
            
            yield return new WaitUntil(() => watchInteractor.hasLooked);
            yield return new WaitForSeconds(5);

            comms.SetActive(true);
            Debug.Log("Comms set active!");
            yield return new WaitForSeconds(10);
            text[0].SetActive(false);
            yield return new WaitForSeconds(10);
            text[1].SetActive(true);




            yield return null; 

        }

    }
}
