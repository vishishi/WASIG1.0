using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public GameObject gesture1;
    public GameObject gesture2;
    public GameObject gesture3;

    public GestureSpawnner spawnner;
    public ChoiceManager choiceManager;


    private void Awake()
    {
        gesture1.SetActive(false);
        gesture2.SetActive(false);
        gesture3.SetActive(false);
    }
    void Start()
    {
        if (ChoiceManager.Instance != null)
        {
            if (ChoiceManager.Instance.gesture1Selected)
            {
                gesture1.SetActive(true);
                spawnner.Gesture1();
                Debug.Log("Gesture 1 was selected!");
            }

            if (ChoiceManager.Instance.gesture2Selected)
            {
                gesture2.SetActive(true);
                spawnner.Gesture2();
                Debug.Log("Gesture 2 was selected!");

            }

            if (ChoiceManager.Instance.gesture3Selected)
            {
                gesture3.SetActive(true);
                spawnner.Gesture3();
                Debug.Log("Gesture 3 was selected!");
            }
        }
        else
        {
            if (choiceManager.gesture1Selected)
            {
                gesture1.SetActive(true);
                spawnner.Gesture1();
                Debug.Log("Gesture 1 was selected!");
            }

            if (choiceManager.gesture2Selected)
            {
                gesture2.SetActive(true);
                spawnner.Gesture2();

            }

            if(choiceManager.gesture3Selected)
            {
                gesture3.SetActive(true);
                spawnner.Gesture3();
            }
        }
    }

    void Update()
    {
  
    }


}
