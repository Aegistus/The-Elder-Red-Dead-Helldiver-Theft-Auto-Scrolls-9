using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest
{
    public class Checkpoint
    {
        public Vector3 position;
        public string popup;
        [TextArea] public string info;
        public UnityEvent OnStart;
        public UnityEvent OnFinish;
    }

    public string questName;
    public Checkpoint[] checkpoints;

    public void StartQuest()
    {

    }


}
