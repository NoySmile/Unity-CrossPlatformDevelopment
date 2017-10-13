using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/Stat")]
public class Stat : ScriptableObject
{
    [SerializeField]
    private int _baseValue;
    public string Name;
    public int Value;
    private void OnEnable()
    {
        name = name.Contains("(Clone)") ? name.Replace("(Clone)", string.Empty) : name;
        Name = name;
        Value = _baseValue;
        
    }

    public void Apply(RPGStats.Modifier mod)
    {
        
        if(mod.type == "add")
        {
            Value += mod.value;
        }

        if(mod.type == "mult")
            Value += _baseValue * mod.value / 10;
    }

    public void Remove(RPGStats.Modifier mod)
    {
        if(mod.type == "add")
            Value -= mod.value;
        if(mod.type == "mult")
            Value -= _baseValue * mod.value / 10;
    }
}
