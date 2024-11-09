using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AgentHealth))]
public class AgentHealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var health = (AgentHealth)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Kill"))
        {
            if (Application.isPlaying)
            {
                health.Kill();
            }
        }
    }
}
