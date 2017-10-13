using System.Collections;
using UnityEngine.EventSystems;

public class ActionChoiceBehaviour : UIBehaviour
{


    public float step = 5f;

    public bool up;

    public void RadialLerp()
    {
        up = !up;
        StopAllCoroutines();    
    }
}