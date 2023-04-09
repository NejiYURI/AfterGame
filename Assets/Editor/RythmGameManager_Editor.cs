using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RythmGameManager))]
public class RythmGameManager_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RythmGameManager rgM = (RythmGameManager)target;

        //if (GUILayout.Button("Trim Data"))
        //{
        //    rgM.TrimData();
        //}

        if (GUILayout.Button("Convert Data"))
        {
            rgM.ConvertData();
        }
    }
}
