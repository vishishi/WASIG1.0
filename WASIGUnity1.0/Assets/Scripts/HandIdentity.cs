using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandType { Left, Right }

public class HandIdentity : MonoBehaviour
{

    public bool hasToBeYellow = false;
    public bool hasToGlowPink = false;
    public bool hasToGlowBlue = false;
    public HandType handType;

  [Header ("Colours")]
  [ColorUsage(true, true)]
  public Color yellow;
  [ColorUsage(true, true)]
  public Color pink;
  [ColorUsage(true, true)]
  public Color blue;
  [ColorUsage(true, true)]
  public Color pinker;
  [ColorUsage(true, true)]
  public Color bluer;


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
        switch (hasToBeYellow,hasToGlowPink,hasToGlowBlue, handType)
        {
//Base pink color
            case (false, false, _, HandType.Left):
                {
                    myMaterial1.SetColor("_EmissionColor", pink);

                    break;
                }
//FB Glow to pink
            case (false, true, _, HandType.Left):
                {
                    myMaterial1.SetColor("_EmissionColor", pinker);

                    break;
                }
//Base blue color
            case (false, _, false, HandType.Right):
                {
                    myMaterial2.SetColor("_EmissionColor", blue);

                    break;
                }
//FB Glow to blue
            case (false, _, true, HandType.Right):
                {
                    myMaterial2.SetColor("_EmissionColor", bluer);

                    break;
                }
//FB Change to yellow
            case (true, _, _,_):

                {
                    myMaterial1.SetColor("_EmissionColor", yellow);
                    myMaterial2.SetColor("_EmissionColor", yellow);
                    break;
                }
        }

    }




}
