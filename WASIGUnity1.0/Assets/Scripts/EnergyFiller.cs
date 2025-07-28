
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class EnergyFiller : MonoBehaviour
{
    public TextMeshProUGUI score;
    private Image block;
    [HideInInspector]
    public float duration;
    public ScoreCounter scoreCounter;
    void Start()
    {
        block = GetComponent<Image>();
    }

  
    void Update()
    {
        score.text = scoreCounter.score.ToString();
        block.fillAmount = Mathf.Lerp(block.fillAmount, scoreCounter.fillAmount, Time.deltaTime * 2);
    }

    IEnumerator Fill(float increase)
    {
        float elapsed = 0f;
        float start = block.fillAmount;
        float duration = 2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            block.fillAmount = Mathf.Lerp(start, increase, t);
            yield return null;
        }

        block.fillAmount = increase;
    }

    public void Increase (float increase)
    {
        StartCoroutine(Fill(increase));
    }
}
