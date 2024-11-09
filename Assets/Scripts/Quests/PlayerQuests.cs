using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public List<Quest> currentQuests;

    public void AddQuest(Quest quest)
    {
        currentQuests.Add(quest);
        quest.StartQuest();
    }
}
