using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldFluxDensityData))]
public class FieldFluxDensityEditor : Editor
{
    FieldFluxDensityLoader loader;
    string path = "";

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("CSV File");
        EditorGUILayout.BeginHorizontal();
        path = EditorGUILayout.TextField(path);

        if (GUILayout.Button("Open"))
        {
            path = EditorUtility.OpenFilePanel("Read CSV File", "", "csv");
        }

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Import CSV"))
        {
            loader = new FieldFluxDensityLoader(path);
        }
    }
}
