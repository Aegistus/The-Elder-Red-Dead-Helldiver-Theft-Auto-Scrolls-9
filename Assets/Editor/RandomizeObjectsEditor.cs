using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomizeObjects))]
public class RandomizeObjectsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var randomizeObjects = (RandomizeObjects)target;
        // record undo operation
        Transform[] children = new Transform[randomizeObjects.transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = randomizeObjects.transform.GetChild(i);
        }
        Undo.RecordObjects(children, "Randomize children");

        if (GUILayout.Button("Randomize"))
        {
            if (Application.isEditor)
            {
                randomizeObjects.Randomize();
            }
        }
    }
}
