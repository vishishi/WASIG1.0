
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GestureChoice : Interactable
{
    private Image hoverImage;
    private Image loadingImage;

    public float fillDuration = 2f; // Adjust in Inspector if needed

    void Start()
    {
        // Find all child Images and assign by tag
        Image[] childImages = GetComponentsInChildren<Image>(true);

        foreach (Image img in childImages)
        {
            switch (img.tag)
            {
                case "Hover":
                    hoverImage = img;
                    break;
                case "loading":
                    loadingImage = img;
                    break;
            }
        }
    }

    private void Update()
    {
     //  if (IsOnlyThisGestureSelected(gameObject.tag))
       // {
         //   gameObject.SetActive(false);
        //}
    
    }

    public override void Interact()
    {
        Choose();
    }

    public void Choose()
    {
        // Set gesture state
        switch (gameObject.tag)
        {
            case "Gesture 1":
                ChoiceManager.Instance.gesture1Selected = true;
                Debug.Log("Gesture 1 chosen");
                break;
            case "Gesture 2":
                ChoiceManager.Instance.gesture2Selected = true;
                Debug.Log("Gesture 2 chosen");
                break;
            case "Gesture 3":
                ChoiceManager.Instance.gesture3Selected = true;
                Debug.Log("Gesture 3 chosen");
                break;
        }

        string[] allGestureTags = { "Gesture 1", "Gesture 2", "Gesture 3" };

        foreach (string tag in allGestureTags)
        {
            if (tag != gameObject.tag)
            {
                GameObject[] others = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject obj in others)
                {
                    obj.SetActive(false);
                }
            }
        }

        // Fade hover image alpha to 0.5
        if (hoverImage != null)
        {
            Color c = hoverImage.color;
            c.a = 0.5f;
            hoverImage.color = c;
        }

        // Start loading fill
        if (loadingImage != null)
        {
            loadingImage.type = Image.Type.Filled; // Just in case
            StartCoroutine(FillCircle(loadingImage, fillDuration));
        }
    }

    IEnumerator FillCircle(Image fill, float duration)
    {
        float elapsed = 0f;
        fill.fillAmount = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fill.fillAmount = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        yield return new WaitUntil(() => fill.fillAmount > 0.99f);

        if (hoverImage != null)
            hoverImage.enabled = false;

        fill.fillAmount = 0f;

        yield return new WaitUntil(() => fill.fillAmount == 0);
        SceneManager.LoadScene("Main");


    }
    // 🔧 Helper function for clean tag logic
    private bool IsOnlyThisGestureSelected(string tag)
    {
        return tag switch
        {
            "Gesture 2" => ChoiceManager.Instance.gesture1Selected &&
                           !ChoiceManager.Instance.gesture2Selected &&
                           !ChoiceManager.Instance.gesture3Selected,

            "Gesture 3" => ChoiceManager.Instance.gesture2Selected &&
                           !ChoiceManager.Instance.gesture1Selected &&
                           !ChoiceManager.Instance.gesture3Selected,

            "Gesture 1" => ChoiceManager.Instance.gesture3Selected &&
                           !ChoiceManager.Instance.gesture1Selected &&
                           !ChoiceManager.Instance.gesture2Selected,


            _ => false
        };
    }
}


