using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGestureManager : MonoBehaviour
{
    public GameObject gesture1;
    public GameObject gesture2;
    public GameObject gesture3;

    public TutorialBeatSpawner beatSpawner;
    public ChoiceManager choiceManager;
    [HideInInspector] public bool hasChosen = false;


    private void Awake()
    {
        //gesture1.SetActive(false);
        //gesture2.SetActive(false);
        //gesture3.SetActive(false);
    }
    void Start()
    {
      StartCoroutine (DetectChoice ());
        hasChosen = false;
    }

    IEnumerator DetectChoice()
    {
        while (true)
        {
            while(!hasChosen)
            {
                if (choiceManager.gesture1Selected)
                {
                    gesture1.SetActive(true);
                    beatSpawner.hasChosen = true;
                    beatSpawner.gestureChoice = 1;
                    Debug.Log("Gesture 1 was selected!");
                    hasChosen = true;
                    break;
                }

                if (choiceManager.gesture2Selected)
                {
                    gesture2.SetActive(true);
                    beatSpawner.hasChosen = true;
                    beatSpawner.gestureChoice = 2;
                    Debug.Log("Gesture 2 was selected!");
                    hasChosen = true;
                    break;

                }

                if (choiceManager.gesture3Selected)
                {
                    gesture3.SetActive(true);
                    beatSpawner.hasChosen = true;
                    beatSpawner.gestureChoice = 3;
                    Debug.Log("Gesture 2 was selected!");
                    hasChosen = true;
                    break;
                }
                yield return null;
               
            }
            yield return null;

            if (hasChosen)
            {
                Debug.Log("Choosing loop broken!");
                break;
            }

            

        }
    }
}
