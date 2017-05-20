using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Linq;

//Original by DYLAN ENGELMAN http://jupiterlighthousestudio.com/custom-inspectors-unity/
//Altered by Brecht Lecluyse http://www.brechtos.com

[CustomPropertyDrawer(typeof(StatStringAttribute))]
public class StatSelectorPropertyDrawer : PropertyDrawer
{
    private List<string> statnames;

    private void OnEnable()
    {
        statnames = new List<string>();
        var stats = Resources.FindObjectsOfTypeAll<Stat>().ToList();
        foreach(var s in stats)
            statnames.Add(s.name);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            var attrib = attribute as StatStringAttribute;

            if (attrib != null)
            {
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            }
            else
            {
                //generate the taglist + custom tags
                var statList = new List<string>() {};
                statList.AddRange(Resources.FindObjectsOfTypeAll<Stat>().Select(n=>n.name));
                var propertyString = property.stringValue;
                var index = -1;
                if (propertyString == "")
                    index = 0; //first index is the special <notag> entry
                else
                {
                    for (var i = 1; i < statList.Count; i++)
                        if (statList[i] == propertyString)
                        {
                            index = i;
                            break;
                        }
                }

                //Draw the popup box with the current selected index
                index = EditorGUI.Popup(position, label.text, index, statList.ToArray());

                //Adjust the actual string value of the property based on the selection
                if (index == 0)
                    property.stringValue = "";
                else if (index >= 1)
                    property.stringValue = statList[index];
                else
                    property.stringValue = "";
            }

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}