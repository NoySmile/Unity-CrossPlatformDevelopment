﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject, IEnumerable<Stat>
{
    public Dictionary<string, Stat> Items = new Dictionary<string, Stat>();
    public Dictionary<int, RPGStats.Modifier> Modifiers = new Dictionary<int, RPGStats.Modifier>();
    public Stat[] StatsArray;
    
    public Stat this[string element]
    {
        get
        {
            return Items.ContainsKey(element) ? Items[element] : null;
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

    private void OnEnable()
    {
        if (StatsArray == null) return;

        foreach (var stat in StatsArray)
            Add(Instantiate(stat));
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
        s.Name = s.Name.Replace("(Clone)", string.Empty);
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

    public Stat GetStat(string key)
    {
        return Items[key];
    }

    [Serializable]
    public class IDModifier
    {
        public int identifier;
        public RPGStats.Modifier mod;
    }
}