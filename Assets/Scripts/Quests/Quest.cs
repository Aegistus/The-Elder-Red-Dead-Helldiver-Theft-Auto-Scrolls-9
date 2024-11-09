using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    [System.Serializable]
    public class Objective
    {
        public Vector3 position;
        public string description;
        public UnityEvent OnStart;
        public UnityEvent OnFinish;
    }

    public string title;
    [TextArea] public string description;
    public Objective[] objectives;
    
    [HideInInspector] public Objective[] unlockedObjectives;

}
