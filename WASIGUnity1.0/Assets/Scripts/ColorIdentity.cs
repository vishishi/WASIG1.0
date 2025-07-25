using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Since both layers and tags are being used in different parts of the logic I had to create this enum to identify the different types of beat, in this case by color.
public enum ColorID { Blue, Pink, Yellow}
public class ColorIdentity : MonoBehaviour
{
    public ColorID colorid;
}
