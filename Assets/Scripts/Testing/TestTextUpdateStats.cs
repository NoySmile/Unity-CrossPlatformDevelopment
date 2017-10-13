using UnityEngine;
using UnityEngine.UI;

public class TestTextUpdateStats : MonoBehaviour
{
    public Stat stat;
    Text text;

    void OnDisable()
    {
        GameState.Instance.EVENT_PLAYERSTATCHANGE.RemoveListener(SetText);
    }

    void OnEnable()
    {
        text = GetComponent<Text>();
        GameState.Instance.EVENT_PLAYERSTATCHANGE.AddListener(SetText);
    }
    void SetText(Stat s)
    {
        if (s.name == stat.name)
            text.text = s.name + "::" + s.Value;
    }
}