using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour {

    public void Toggle()
    {
        gameObject.ToggleActiveSelf();
    }
}
