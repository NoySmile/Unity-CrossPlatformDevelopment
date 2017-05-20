using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class TestSaveStats : MonoBehaviour
{
    private string path;
    
    public StatsValue stats_Value;

    [SerializeField]
    private List<Stat> stats_config;

    private List<Stat> stats_runtime;

    private Stats stats;

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

    private void Start()
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

        foreach (var stat in stats_Value.stats)
            stats.Add(stat);
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
            stats.Add(stat);
    }
    //save the runtime data to file
    public void Save()
    {
        path = Application.persistentDataPath + "/player-stats.json";
        stats_Value.Stats = stats_runtime;
        stats = ScriptableObject.CreateInstance<Stats>();
        foreach(var stat in stats_Value.stats)
            stats.Add(stat);

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
            stats.Add(stat);
    }

    private int modnum = 0;
    private void Modify()
    {
        foreach(var s in stats_runtime)
        {
            var r = Random.Range(0, 10);
            var mod = new RPGStats.Modifier("add", s.Name, r);
            stats.AddModifier(++modnum, mod);
        }
    }

    [CustomEditor(typeof(TestSaveStats))]
    public class InspectorTestSave : Editor
    {
        private List<string> statnames;

        private void OnEnable()
        {
            statnames = new List<string>();
            var stats = Resources.FindObjectsOfTypeAll<Stat>().ToList();
            foreach (var s in stats)
                statnames.Add(s.name);
        }

        private int selected = 0;
        public override void OnInspectorGUI()
        {
            var mt = target as TestSaveStats;
            if(GUILayout.Button("Save"))
                if(mt != null) mt.Save();
            if(GUILayout.Button("Load"))
                if(mt != null) mt.Load();
            if(GUILayout.Button("Clear"))
                if(mt != null) mt.Clear();
            if(GUILayout.Button("Modify"))
                if(mt != null) mt.Modify();
            if (mt != null && (mt.stats_runtime != null && mt.stats_runtime.Count > 0))
            {
                foreach(var s in mt.stats_runtime)
                    EditorGUILayout.LabelField(s.Name, s.Value.ToString());
            }

            selected = EditorGUILayout.Popup("Stats", selected, statnames.ToArray());

            base.OnInspectorGUI();
        }
    }
}