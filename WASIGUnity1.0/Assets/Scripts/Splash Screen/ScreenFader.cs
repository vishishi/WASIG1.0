using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    private Image black;
    public float fadeDuration;
    public string sceneName;
    public bool isTesting;
    void Start()
    {
        black = GetComponentInChildren<Image>();
        Debug.Log("Image found!");
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeScreen(float duration, string sceneName)
    {
        float elapsedTime = 0;
        Color startColor = black.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            black.color = Color.Lerp(startColor, targetColor, t);

            Debug.Log("Image alpha is now: " + black.color.a);
            yield return null;
        }

        if (!isTesting)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ChangeScene(string nameScene)
    {
        StartCoroutine(FadeScreen(fadeDuration, nameScene));
    }
}
