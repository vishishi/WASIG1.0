using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtoManager : MonoBehaviour
{
    public GameObject chooser;
    public GameObject maki;
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
        
        yield return new WaitForSeconds(15);
        maki.SetActive(false);
        chooser.SetActive(true);
        
    }
}


