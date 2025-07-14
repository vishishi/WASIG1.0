using System.Collections;
using System.Collections.Generic;
using Meta.XR.ImmersiveDebugger;
using UnityEngine;

public class Interactor : MonoBehaviour
{
   LayerMask interactableLayer;
   Interactable interactable;
   public float rayLength = 100f;
   private LineRenderer lineRenderer;
   
    void Start()
    {
        interactableLayer = LayerMask.GetMask("Interactable");
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity, interactableLayer))
        {
         
            
           // lineRenderer.SetPosition(0, origin);
          //  lineRenderer.SetPosition(1, hit.point);
            interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
        else
        {
            
           // lineRenderer.SetPosition(0, origin);
          //  lineRenderer.SetPosition(1, origin + direction * rayLength);
        }
    }
}
