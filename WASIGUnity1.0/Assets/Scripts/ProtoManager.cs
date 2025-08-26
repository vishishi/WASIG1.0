using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoManager : MonoBehaviour
{
    public GameObject chooser;
    public GameObject maki;
    public WatchInteractor interactor;
    public GameObject comms;
    public GameObject choice;
    public SceneLoader sceneLoader;
    void Start()
    {
        StartCoroutine(Sequencer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Sequencer()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Load the next scene!");
        sceneLoader.LoadNextScene();
        
        
    }
}


