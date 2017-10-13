using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Stats")]
public class Stats : ScriptableObject, IEnumerable<Stat>
{
    private Dictionary<string, Stat> Items = new Dictionary<string, Stat>();
    private Dictionary<int, RPGStats.Modifier> Modifiers = new Dictionary<int, RPGStats.Modifier>();
    public Stat[] StatsArray;
    
    private void OnEnable()
    {
        if (StatsArray == null) return;

        foreach (var stat in StatsArray)
        {
            var s = Instantiate(stat);
            Add(s);
        }
    }

    public string AddModifier(int id, RPGStats.Modifier m)
    {
        Modifiers.Add(id, m);
        var result = string.Format(
            "Add modifier {0} {1} {2}",
            Modifiers[id].stat,
            Modifiers[id].type,
            Modifiers[id].value);

        Items[m.stat].Apply(m);
        GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(Items[m.stat]);
        return result;
    }

    public string RemoveModifier(int id)
    {
        string statname = Modifiers[id].stat;
        var result = string.Format("Remove modifier {0} {1} {2}", statname, Modifiers[id].type,
            Modifiers[id].value);
        Items[Modifiers[id].stat].Remove(Modifiers[id]);
        Modifiers.Remove(id);
        GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(Items[statname]);
        return result;
    }

    public void Add(Stat s)
    {
        UnityEngine.Assertions.Assert.IsFalse(s.name.Contains("Clone"), "name has clone..this will not work well with the dictionaries");
        Items.Add(s.Name, s);
        GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(Items[s.Name]);
    }

    public void ClearModifiers()
    {
        var keys = Modifiers.Keys.ToArray();
        foreach (var key in keys)
            RemoveModifier(key);

        Modifiers.Clear();
    }

    public Stat this[string element]
    {
        get
        {
            var item = Items.ContainsKey(element) ? Items[element] : null;
            if (item == null)
                Debug.Log("tried to fetch " + element);
            return item;
        }

        set { Items[element] = value; }
    }

    public int Count
    {
        get { return StatsArray.Length; }
    }

    public IEnumerator<Stat> GetEnumerator()
    {
        return Items.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Items.Values.GetEnumerator();
    }

}