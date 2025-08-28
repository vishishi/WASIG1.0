using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    [Header("Raycast")]
    public LayerMask floorMask;
    public float maxDistance = 25f;

    [Header("Reticle")]
    public GameObject reticlePrefab;
    public float fillDuration = 2f;

    [Header("Teleport")]
    public Transform player; // assign your rig/player root

    private Vector3 hitPoint;
    private bool hasLoaded;
    private bool isFilling;

    private GameObject reticleInstance;
    private Animator reticleAnimator;
    private Image reticleImage;

    void Start()
    {
        StartCoroutine(DetectHit());
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, floorMask, QueryTriggerInteraction.Ignore))
        {
            hitPoint = hit.point;
            hasLoaded = true;
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
        }
        else
        {
            hasLoaded = false;
        }
    }

    IEnumerator DetectHit()
    {
        while (true)
        {
            if (hasLoaded && !isFilling)
            {
                isFilling = true;
                yield return StartCoroutine(FillReticle(fillDuration));
                isFilling = false;
            }

           
            yield return null;
        }
    }

    IEnumerator FillReticle(float duration)
    {
        // Create once, then reuse
        if (reticleInstance == null)
        {
            reticleInstance = Instantiate(reticlePrefab, new Vector3(hitPoint.x, hitPoint.y + 0.05f, hitPoint.z), Quaternion.Euler(90, 0, 0));
            reticleImage = reticleInstance.GetComponentInChildren<Image>();
            reticleAnimator = reticleInstance.GetComponent<Animator>();
            reticleAnimator.SetBool("willFill", true);
            if (reticleImage == null)
            {
                Debug.LogError("[Teleporter] Reticle prefab has no Image in children.");
                yield break;
            }
            else if (reticleAnimator == null)
            {
                Debug.LogError("[Teleporter] Reticle prefab has no animator in children.");
            }
        }
        else
        {
            
            reticleInstance.SetActive(true);
            reticleInstance.transform.position = new Vector3 (hitPoint.x, hitPoint.y + 0.05f, hitPoint.z);
            reticleAnimator.SetBool("willFill", true);


        }


        

        yield return new WaitForSeconds(4.5f);
        reticleImage.fillAmount = 0f;
        reticleAnimator.SetBool("willFill", false);
        reticleInstance.SetActive(false);
        if (player != null) player.position = new Vector3 (hitPoint.x, player.position.y,hitPoint.z);
        
       
        hasLoaded = false;
        yield return new WaitForSeconds(3);
        
        
        yield return null;

       
    }

    


}

