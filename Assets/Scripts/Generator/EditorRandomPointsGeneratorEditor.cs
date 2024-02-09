using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorRandomPointsGenerator))]
public class EditorRandomPointsGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorRandomPointsGenerator generator = (EditorRandomPointsGenerator)target;

        if (GUILayout.Button("Generate Random Points"))
        {
            generator.GenerateRandomPoints();
        }
    }
}

