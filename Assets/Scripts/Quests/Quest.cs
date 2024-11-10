using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UltEvents;

[System.Serializable]
public class Quest
{
    [System.Serializable]
    public class Objective
    {
        public string description;
        public Transform location;
        public Vector3 offset;
        public UltEvent OnStart;
        public UltEvent OnFinish;
    }

    public string title;
    [TextArea] public string description;
    public Objective[] objectives;
    public QuestEnum questEnum;
    public UltEvent OnQuestComplete;

    [HideInInspector] public int currentObjectiveInd = -1;
    [HideInInspector] public Objective currentObjective;
    [HideInInspector] public List<Objective> unlockedObjectives = new();

}
