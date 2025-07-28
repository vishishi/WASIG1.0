using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [HideInInspector]
    public int score;
    [HideInInspector]
    public float fillAmount;
    [HideInInspector]
    public int maxScore = 5500;
    [HideInInspector]
    public int bad;
    [HideInInspector]
    public int good;
    [HideInInspector]
    public int perfect;
    [HideInInspector]
    public int miss;
    [HideInInspector]
    public int superCharge;
    public EnergyFiller energyFiller;


    private void Awake()
    {
        maxScore = 5500;
    }
    private void Start()
    {
        score = 0;  
      
       //s score =Mathf.Clamp(score, 0, maxScore);
    }
    void Update()
    {

        superCharge = perfect - (bad + miss);
        fillAmount = (float)score/maxScore;

    }
}
