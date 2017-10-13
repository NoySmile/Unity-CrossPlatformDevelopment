using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
///     Controller class for UI
///     Interacts with the UIView and Rest of Game
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField] BackPackBehaviour backPackBehaviour;

    bool InventoryUp;

    [SerializeField] Dropdown itemDropdownList;

    Vector3 oldInventoryPos;

    [HideInInspector] public OnCancel onCancel;

    [HideInInspector] public OnStart onStart;

    public OnStartButton onStartButton;

    [HideInInspector] public OnSubmit onSubmit;

    Button save_button, load_button, clear_button;

    [SerializeField] Dropdown savesDropdownList;

    [SerializeField] UIView View;

    void Awake()
    {
        View = FindObjectOfType<UIView>();
    }

    void Start()
    {
        onStart.Invoke();
        var ui_gridbehaviour = View.InventoryGrid.GetComponent<UIGridBehaviour>();
        backPackBehaviour.onBackPackChange.AddListener(ui_gridbehaviour.SetItems);
    }

    void Update()
    {
        if (GetCancel()) onCancel.Invoke();
        if (GetSubmit()) onSubmit.Invoke();
        if (GetStart()) onStartButton.Invoke();
    }


    public void UpdateSavesDropdown()
    {
        var path = Application.dataPath + "/StreamingAssets/";
        var files = Directory.GetFiles(path, "*.json").ToList();

        var names = new List<string>();
        foreach (var f in files)
        {
            var fname = f.Substring(path.Length);
            var nfname = fname.Remove(fname.Length - ".json".Length);
            names.Add(nfname);
        }

        savesDropdownList.ClearOptions();
        savesDropdownList.AddOptions(names);
    }

    public void InventoryToggle()
    {
        if (!InventoryUp)
        {
            oldInventoryPos = View.Inventory.transform.localPosition;
            View.Inventory.transform.localPosition = new Vector3(0, 1000f, 0);
            InventoryUp = true;
        }

        else
        {
            View.Inventory.transform.localPosition = oldInventoryPos;
            InventoryUp = false;
        }
    }

    public void SetHealthSlider(int val)
    {
        View.HealthSlider.value = val;
    }

    public static bool GetCancel()
    {
        return Input.GetButtonDown("Cancel");
    }

    public static bool GetSubmit()
    {
        return Input.GetButtonDown("Submit");
    }

    public static bool GetStart()
    {
        return Input.GetButtonDown("Start");
    }

    [Serializable]
    public class OnStart : UnityEvent{}

    [Serializable]
    public class OnCancel : UnityEvent{}

    [Serializable]
    public class OnSubmit : UnityEvent{}

    [Serializable]
    public class OnStartButton : UnityEvent{}
}