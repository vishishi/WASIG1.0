using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactor : MonoBehaviour
{
   LayerMask interactableLayer;
   Interactable interactable;
   Beat beat;
   public float rayLength = 100f;
   private LineRenderer lineRenderer;
   public ScoreCounter scoreCounter;
   private Scene scene;
   
    void Start()
    {
        interactableLayer = LayerMask.GetMask("Interactable");
        scene = SceneManager.GetActiveScene();

        
        
        
        
        
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity, interactableLayer))
        {
            // Only runs if the raycast hit something in the correct layer
            interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }

            // Check tag of the hit object
            if (scene.name == "Main" && scoreCounter != null & hit.collider.CompareTag("Beat"))
            {
                scoreCounter.score++;
                Debug.Log("Current score:" + scoreCounter.score);

            }



        }
 
    }

}
