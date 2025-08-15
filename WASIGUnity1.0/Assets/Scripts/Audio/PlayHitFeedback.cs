using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayHitFeedback : MonoBehaviour
{
    [SerializeField] EventReference okHitEventRef;
    [SerializeField] EventReference goodHitEventRef;
    [SerializeField] EventReference perfectHitEventRef;

    public void PlayOKHit()
    {
        RuntimeManager.PlayOneShot(okHitEventRef);
    }

    public void PlayGoodHit()
    {
        RuntimeManager.PlayOneShot(goodHitEventRef);
    }

    public void PlayPerfectHit()
    {
        RuntimeManager.PlayOneShot(perfectHitEventRef);
    }
    


}
