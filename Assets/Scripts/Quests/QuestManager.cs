using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<Quest> currentQuests;
    [SerializeField] float questUpdateDelay = 4f;

    [SerializeField] float startQuestDelay = 3f;
    [SerializeField] Quest mainQuest;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartCoroutine(StartMainQuest());
    }
    public void AddQuest(Quest quest)
    {
        quest = Instantiate(quest);
        currentQuests.Add(quest);
        QuestPopup.Instance.ShowPopup(quest.title);
        SoundManager.Instance.PlaySoundGlobal("Quest_Start");
        quest.currentObjective = quest.objectives[0];
        UpdateQuestObjective(quest, 0);
    }

    public void UpdateQuestObjective(Quest quest, int newObjectiveIndex)
    {
        StartCoroutine(UpdateDelay(quest, newObjectiveIndex));
    }

    IEnumerator UpdateDelay(Quest quest, int objectiveIndex)
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

    IEnumerator StartMainQuest()
    {
        yield return new WaitForSeconds(startQuestDelay);
        AddQuest(mainQuest);
    }
}
