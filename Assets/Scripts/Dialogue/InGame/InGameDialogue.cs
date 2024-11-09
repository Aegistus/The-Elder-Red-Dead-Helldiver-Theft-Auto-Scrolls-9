using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InGame Dialogue", menuName = "In-Game Dialogue")]
public class InGameDialogue : ScriptableObject
{
    public string characterName;
    public Frame[] frames;

    [System.Serializable]
    public class Frame
    {
        [TextArea] public string characterDialogue;
        public string[] responses;
    }
}
