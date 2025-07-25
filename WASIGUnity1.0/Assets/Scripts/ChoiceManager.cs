using Unity.VisualScripting;
using UnityEngine;

//Simple singleton that record your choise of gesture in Shiki's room and passes that information into the stage level. 

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance { get; private set; }
    public bool gesture1Selected { get; set; }
    public bool gesture2Selected { get; set; }
    public bool gesture3Selected { get; set; }
    private void Awake()
    {
        Debug.Log("[ChoiceManager] Awake called.");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[ChoiceManager] Instance was null, now set to this object.");
        }
        else if (Instance != this)
        {
            Debug.LogWarning("[ChoiceManager] Instance already exists. Destroying this object to avoid duplicates.");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("[ChoiceManager] Instance already set to this object.");
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Debug.Log("[LoopManager] OnDestroy called, clearing the Instance since it's this object.");
            Instance = null;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//This function is called when you interact with the choice UI
    public void MarkGestureAsSelected(string tag)
    {
        switch (tag)
        {
            case "Gesture 1":
                gesture1Selected = true;
              
                break;
            case "Gesture 2":
                gesture2Selected = true;
              
                break;
            case "Gesture 3":
                gesture3Selected = true;
            
                break;
            default:
                
                break;
        }
    }
}
