using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Assertions;

public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            if(!_instance)
                _instance = CreateInstance<T>();
            return _instance;
        }
    }
}

/// <summary>
/// class to track data and save to a location
/// </summary>
[CreateAssetMenu]
public class DataController : ScriptableSingleton<DataController>
{
    private static int numsaves = 0;
    public static void Save<T>(T data, string profilename = "") where T : ScriptableObject
    {
        string path = Application.dataPath + "/StreamingAssets/";
        var files = Directory.GetFiles(path, "*.json").ToList();
        numsaves = files.Count;
        //object to json string
        var json = JsonUtility.ToJson(data, true);
        //filename to save
        var savename = (profilename != "") ? data.GetType().ToString() : profilename;
        var filename = string.Format("{0}-{1}.json", savename, numsaves);
        //write all text to the file at the path
        File.WriteAllText(path + filename, json);
        Debug.LogFormat(@"<color=cyan>save {0} with filename {1}</color>", data, filename);
    }
    
    public static T Load<T>(string filename) where T : ScriptableObject
    {
        string path = Application.dataPath + "/StreamingAssets/";
        var json = File.ReadAllText(path + filename+".json");
        var data = CreateInstance<T>();
        JsonUtility.FromJsonOverwrite(json, data);
        Debug.LogFormat(@"<color=green>load {0} with filename {1}</color>", data, filename);
        return data;
    }

}