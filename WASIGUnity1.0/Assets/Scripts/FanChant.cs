using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanChant : MonoBehaviour
{
    [HideInInspector]
    public Starstick starstick;
    public StartGame gameStart;
    void Start()
    {
        starstick = FindAnyObjectByType<Starstick>();
        StartCoroutine(FanChantSequencer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FanChantSequencer()
    {
        yield return new WaitUntil(() => gameStart.hasStarted);
        yield return new WaitForSeconds(15);
        starstick.TriggerSync(1);
        Debug.Log("fans chanted!");
    }
}
