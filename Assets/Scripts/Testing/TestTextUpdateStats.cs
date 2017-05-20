using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTextUpdateStats : MonoBehaviour
{
    public Stat stat;
    private UnityEngine.UI.Text text;

    private void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
    }

    void SetText(Stat s)
    {
        if (s.name == stat.name)
            text.text = s.name + "::" + s.Value;
    }

}
