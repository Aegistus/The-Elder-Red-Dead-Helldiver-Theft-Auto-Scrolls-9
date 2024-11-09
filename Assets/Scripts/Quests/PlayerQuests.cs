using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public List<Quest> currentQuests;
    [SerializeField] float questUpdateDelay = 4f;

    public void AddQuest(Quest quest)
    {
        quest = Instantiate(quest);
        currentQuests.Add(quest);
        QuestPopup.Instance.ShowPopup(quest.title);
        SoundManager.Instance.PlaySoundGlobal("Quest_Start");
        quest.currentObjective = quest.objectives[0];
        StartCoroutine(UpdateQuestObjective(quest, 0));
    }

    IEnumerator UpdateQuestObjective(Quest quest, int objectiveIndex)
    {
        yield return new WaitForSeconds(questUpdateDelay);
        quest.unlockedObjectives.Add(quest.objectives[objectiveIndex]);
        quest.currentObjective = quest.objectives[objectiveIndex];
        QuestUpdatePopup.Instance.ShowPopup(quest.currentObjective.description);
        var questMarker = GameObject.FindWithTag("Quest Marker");
        if (questMarker != null)
        {
            questMarker.transform.position = quest.currentObjective.position;
        }
    }
}
