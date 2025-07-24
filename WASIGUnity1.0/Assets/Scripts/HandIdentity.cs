using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandType { Left, Right }

public class HandIdentity : MonoBehaviour
{

    public bool hasToBeYellow = false;
    public HandType handType;

  [Header ("Colours")]
  [ColorUsage(true, true)]
  public Color Yellow;
  [ColorUsage(true, true)]
  public Color pink;
    [ColorUsage(true, true)]
    public Color blue;
        

  public Renderer [] HandRenderer;
  private Material myMaterial1;
    private Material myMaterial2;



    private void Start()
    {
        
      myMaterial1 = HandRenderer[0].material;
        myMaterial2 = HandRenderer[1].material;
    }
    private void Update()
    {
        switch (hasToBeYellow, handType)
        {
            case (false, HandType.Left):
                {
                    myMaterial1.SetColor("_EmissionColor", pink);

                    break;
                }

            case (false, HandType.Right):
                {
                    myMaterial2.SetColor("_EmissionColor", blue);

                    break;
                }

            case (true, _):

                {
                    myMaterial1.SetColor("_EmissionColor", Yellow);
                    myMaterial2.SetColor("_EmissionColor", Yellow);
                    break;
                }
        }

    }




}
