
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(TestSaveStats))]
public class InspectorTestSave : Editor
{
    private List<string> statnames;

    private void OnEnable()
    {
        statnames = new List<string>();
        var stats = Resources.FindObjectsOfTypeAll<Stat>().ToList();
        foreach(var s in stats)
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
        if(mt != null && (mt.Stats != null && mt.Stats.Count > 0))
        {
            foreach(var s in mt.Stats)
                EditorGUILayout.LabelField(s.Name, s.Value.ToString());
        }

        selected = EditorGUILayout.Popup("Stats", selected, statnames.ToArray());

        base.OnInspectorGUI();
    }
}
#endif