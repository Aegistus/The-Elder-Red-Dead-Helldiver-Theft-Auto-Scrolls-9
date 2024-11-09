using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Cinematic Dialogue")]
public class CinematicDialogue : ScriptableObject
{
    [System.Serializable]
    public class Frame
    { 
        public string characterName;
        public string text;
    }

    public Frame[] frames;

}
