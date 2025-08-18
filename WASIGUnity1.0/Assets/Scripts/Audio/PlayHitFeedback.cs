using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayHitFeedback : MonoBehaviour
{
    [SerializeField] EventReference okHitEventRef;
    [SerializeField] EventReference goodHitEventRef;
    [SerializeField] EventReference perfectHitEventRef;

    public void PlayOKHit(Vector3 hitLocation)
    {
        RuntimeManager.PlayOneShot(okHitEventRef, hitLocation);
    }

    public void PlayGoodHit(Vector3 hitLocation)
    {
        RuntimeManager.PlayOneShot(goodHitEventRef, hitLocation);
    }

    public void PlayPerfectHit(Vector3 hitLocation)
    {
        RuntimeManager.PlayOneShot(perfectHitEventRef, hitLocation);
    }
    


}
