using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [HideInInspector]
    public int score;
    public EnergyFiller energyFiller;

    // Update is called once per frame
    void Update()
    {
        if (score == 5)
        {
            energyFiller.Increase(0.1f);
        }

        if (score == 10)
        {
            energyFiller.Increase(0.3f);
        }

        if (score == 15)
        {
            energyFiller.Increase(0.5f);

        }

        if (score == 20)
        {
            energyFiller.Increase(0.7f);
        }

        if (score == 25)
        {
            energyFiller.Increase(0.9f);

        }

        if ( score > 30)
        {
            energyFiller.Increase(1);
        }
    }

 
}
