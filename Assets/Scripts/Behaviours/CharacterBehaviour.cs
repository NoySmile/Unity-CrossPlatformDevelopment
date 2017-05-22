using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterBehaviour : MonoBehaviour
{

    [Serializable]
    public class StatsValue
    {
        public int Count;
        public List<string> names;
        public List<Stat> stats;
        public List<int> values;

        public List<Stat> Stats
        {
            get { return stats; }
            set
            {
                stats = value;
                values = new List<int>();
                names = new List<string>();
                foreach(var t in stats)
                {
                    values.Add(t.Value);
                    names.Add(t.Name);
                }

                Count = stats.Count;
            }
        }
    }

    [Serializable]
    public class OnStatModify : UnityEvent<Stat> { }


    [Serializable]
    public class OnHealthChange : UnityEvent<int> { }

    protected int modcount;
    [SerializeField]
    protected Stats CharacterStats;
    public OnHealthChange onHealthChange = new OnHealthChange();
    public OnStatModify onStatModify = new OnStatModify();
    
    public virtual void ModifyStat(Stat stat, RPGStats.Modifier mod) { }

    public virtual void ModifyStat(int id, Stat stat, RPGStats.Modifier mod) { }

    public virtual void RemoveModifier(int id) { }

    public virtual void ClearAll()
    {
        CharacterStats.ClearModifiers();
    }
}
