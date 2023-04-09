using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MusicReader))]
public class MusicSheet_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MusicReader musicReader = (MusicReader)target;

        if (GUILayout.Button("Create File"))
        {
            musicReader.CreateFile();
        }

    }
}
