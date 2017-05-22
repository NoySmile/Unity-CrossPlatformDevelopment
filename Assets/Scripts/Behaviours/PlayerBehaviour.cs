using System;
using System.Collections.Generic;
using System.IO;
using RPGStats;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour
{
    private string path;

    public StatsValue stats_Value;
    /// <summary>
    /// list of stat to use as this configuration
    /// </summary>
    private List<Stat> stats_config;
    /// <summary>
    /// the runtime list of stats
    /// </summary>
    private List<Stat> stats_runtime;

    /// <summary>
    /// for anyone to view
    /// </summary>
    public List<Stat> Stats
    { get { return stats_runtime; } }
    /// <summary>
    /// the runtime Stats object
    /// </summary>
    private Stats stats;

    private void Start()
    {
        stats_runtime = new List<Stat>();
        stats_config = new List<Stat>();
        stats_config.AddRange(CharacterStats.StatsArray);
        foreach(var stat in stats_config)
        {
            var clone = Instantiate(stat);
            clone.name = clone.name.Replace("(Clone)", string.Empty);
            clone.Name = clone.name;
            stats_runtime.Add(clone);
        }

        stats_Value = new StatsValue { Stats = stats_runtime };

        stats = ScriptableObject.CreateInstance<Stats>();

        foreach(var stat in stats_Value.stats)
        {
            stats.Add(stat);
            GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(stat);
        }

        if(File.Exists(Application.persistentDataPath + "/player-stats.json"))
            Load();

    }

    public void Clear()
    {
        stats_runtime = new List<Stat>();

        foreach(var stat in stats_config)
        {
            var clone = Instantiate(stat);
            clone.name = clone.name.Replace("(Clone)", string.Empty);
            clone.Name = clone.name;
            stats_runtime.Add(clone);
        }

        stats_Value = new StatsValue { Stats = stats_runtime };

        stats = ScriptableObject.CreateInstance<Stats>();

        foreach(var stat in stats_Value.stats)
        {
            GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(stat);
            stats.Add(stat);
        }

    }
    //save the runtime data to file
    public void Save()
    {
        path = Application.persistentDataPath + "/player-stats.json";
        stats_Value.Stats = stats_runtime;
        stats = ScriptableObject.CreateInstance<Stats>();
        foreach(var stat in stats_Value.stats)
        {
            stats.Add(stat);
            GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(stat);
        }


        var json = JsonUtility.ToJson(stats_Value, true);
        File.WriteAllText(path, json);
    }

    /// <summary>
    ///     load the data from file and set the config then populate
    /// </summary>
    public void Load()
    {
        path = Application.persistentDataPath + "/player-stats.json";
        var json = File.ReadAllText(path);

        JsonUtility.FromJsonOverwrite(json, stats_Value);

        stats_runtime = new List<Stat>();

        foreach(var stat in stats_config)
        {
            var clone = Instantiate(stat);
            clone.name = clone.name.Replace("(Clone)", string.Empty);
            clone.Name = clone.name;
            stats_runtime.Add(clone);
        }

        for(var i = 0; i < stats_Value.Count; i++)
        {
            stats_runtime[i].name = stats_Value.names[i];
            stats_runtime[i].Name = stats_Value.names[i];
            stats_runtime[i].Value = stats_Value.values[i];
        }

        stats_Value = new StatsValue { Stats = stats_runtime };
        stats = ScriptableObject.CreateInstance<Stats>();
        foreach(var stat in stats_Value.stats)
        {
            stats.Add(stat);
            GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(stat);
        }

    }

    public void Modify()
    {
        
    }

    public override void ModifyStat(Stat stat, RPGStats.Modifier mod)
    {
        if (!CharacterStats[stat.Name])
        {
            Debug.LogWarningFormat("stat:: {0}, is not a valid stat to modify", stat);
            return;
        }

        CharacterStats.AddModifier(modcount++, mod);

        if (stat.Name == "Health")
            onHealthChange.Invoke(CharacterStats[stat.Name].Value);
        onStatModify.Invoke(stat);
        GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(stat);
    }

    public override void RemoveModifier(int id)
    {
        CharacterStats.RemoveModifier(id);
    }

    public override void ModifyStat(int id, Stat stat, RPGStats.Modifier mod)
    {
        if(!CharacterStats[stat.Name])
        {
            Debug.LogWarningFormat("stat:: {0}, is not a valid stat to modify", stat);
            return;
        }

        CharacterStats.AddModifier(id, mod);
        
        if(stat.Name == "Health")
            onHealthChange.Invoke(CharacterStats[stat.Name].Value);
        onStatModify.Invoke(CharacterStats[stat.Name]);
        GameState.Instance.EVENT_PLAYERSTATCHANGE.Invoke(CharacterStats[stat.Name]);
    }

}