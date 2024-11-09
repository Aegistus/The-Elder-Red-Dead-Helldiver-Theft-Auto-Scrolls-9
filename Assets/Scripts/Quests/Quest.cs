using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    [System.Serializable]
    public class Checkpoint
    {
        public Vector3 position;
        public string description;
        public UnityEvent OnStart;
        public UnityEvent OnFinish;
    }

    public string title;
    [TextArea] public string description;
    public Checkpoint[] checkpoints;

}
