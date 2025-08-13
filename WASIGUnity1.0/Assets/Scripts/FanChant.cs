using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static BeatMapSpawner;

public class FanChant : MonoBehaviour
{
    [HideInInspector]
    public Starstick starstick;
    public ScoreCounter scoreCounter;
    public StartGame gameStart;
    //public AudioSource cheer;

    [System.Serializable]
    public class ChantPoints
    {
        public float time;
        public float duration;
        public float beat;
        //public AudioSource chant; 
    }

    private bool lastSuperChargeState = false;



    [Header("Chant Settings")]
    public List<ChantPoints> chantPoints;
    void Start()
    {
        starstick = FindAnyObjectByType<Starstick>();
        StartCoroutine(FanChantSequencer());
    }

    private void Update()
    {
        if (!lastSuperChargeState && scoreCounter.isSuperCharged)
        { 
      //      cheer.Play();
        }

        lastSuperChargeState = scoreCounter.isSuperCharged;
    }

    IEnumerator FanChantSequencer()
    {
        yield return new WaitUntil(() => gameStart.hasStarted);

        chantPoints.Sort((a, b) => a.time.CompareTo(b.time));
        float lastTime = 0;

        for (int i = 0; i < chantPoints.Count; i++)
        {
        
            float wait = Mathf.Max(0f, chantPoints[i].time -lastTime);
            if (wait < 0f)
            {
                yield return new WaitForSeconds(wait);
                //chantPoints[i].chant.Play();
                starstick.TriggerSync(chantPoints[i].duration, chantPoints[i].beat);

                lastTime = chantPoints[i].time;
            }
        }
    }
}

