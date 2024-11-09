using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public List<Quest> currentQuests;
    [SerializeField] float questUpdateDelay = 4f;

    public void AddQuest(Quest quest)
    {
        currentQuests.Add(quest);
        QuestPopup.Instance.ShowPopup(quest.title);
        SoundManager.Instance.PlaySoundGlobal("Quest_Start");
        StartCoroutine(UpdateQuestObjective(quest.objectives[0].description));
    }

    IEnumerator UpdateQuestObjective(string update)
    {
        yield return new WaitForSeconds(questUpdateDelay);
        QuestUpdatePopup.Instance.ShowPopup(update);
    }
}
