using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Interactable
{
    Material material;
    public Color color;
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        color = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        material.color = Color.cyan;
    }
}
