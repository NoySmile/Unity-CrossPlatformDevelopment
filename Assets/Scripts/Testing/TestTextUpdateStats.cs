using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTextUpdateStats : MonoBehaviour
{
    public Stat stat;
    private UnityEngine.UI.Text text;
    private void OnDisable()
    {
        GameState.Instance.EVENT_PLAYERSTATCHANGE.RemoveListener(SetText);
    }
    private void Awake()
    {  text = GetComponent<UnityEngine.UI.Text>();
        GameState.Instance.EVENT_PLAYERSTATCHANGE.AddListener(SetText);
    }
    private void Start()
    {
      
        
    }

    void SetText(Stat s)
    {
        if (s.name == stat.name)
            text.text = s.name + "::" + s.Value;
    }

}
