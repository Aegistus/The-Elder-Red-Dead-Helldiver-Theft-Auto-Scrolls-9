using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestEnum
{
    MainQuest, MilkDrinker
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<Quest> currentQuests;
    [SerializeField] float questUpdateDelay = 4f;

    [SerializeField] float startQuestDelay = 3f;
    [SerializeField] Quest mainQuest;
    [SerializeField] List<Quest> sideQuests;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        AddQuestWithDelay(QuestEnum.MainQuest, 3);
    }
    public void AddQuest(Quest quest)
    {
        currentQuests.Add(quest);
        QuestPopup.Instance.ShowQuestStartPopup(quest.title);
        quest.currentObjective = quest.objectives[0];
        UpdateQuestObjective(quest.questEnum, 0);
    }

    public void AddQuest(QuestEnum questEnum)
    {
        Quest quest = sideQuests.Find(q => q.questEnum == questEnum);
        if (quest == null)
        {
            quest = mainQuest;
        }
        AddQuest(quest);
    }

    public void AddQuestWithDelay(QuestEnum questEnum, float delay)
    {
        StartCoroutine(QuestDelay(questEnum, delay));
    }

    IEnumerator QuestDelay(QuestEnum questEnum, float delay)
    {
        yield return new WaitForSeconds(delay);
        AddQuest(questEnum);
    }
    public void UpdateQuestObjective(QuestEnum questEnum, int newObjectiveIndex)
    {
        Quest quest = currentQuests.Find(q => q.questEnum == questEnum);
        if (quest == null || quest.currentObjectiveInd >= newObjectiveIndex)
        {
            return;
        }
        quest.currentObjectiveInd = newObjectiveIndex;
        quest.currentObjective.OnFinish.Invoke();
        StartCoroutine(UpdateDelay(quest, newObjectiveIndex));
    }

    public void FinishQuest(QuestEnum questEnum)
    {
        Quest quest = currentQuests.Find(q => q.questEnum == questEnum);
        quest.currentObjective?.OnFinish?.Invoke();
        if (quest == null)
        {
            return;
        }
        currentQuests.Remove(quest);
        QuestPopup.Instance.ShowQuestEndPopup(quest.title);
        quest.OnQuestComplete?.Invoke();
    }

    IEnumerator UpdateDelay(Quest quest, int objectiveIndex)
    {
        yield return new WaitForSeconds(questUpdateDelay);
        quest.unlockedObjectives.Add(quest.objectives[objectiveIndex]);
        quest.currentObjective = quest.objectives[objectiveIndex];
        quest.currentObjective.OnStart.Invoke();
        QuestUpdatePopup.Instance.ShowPopup(quest.currentObjective.description);
        SoundManager.Instance.PlaySoundGlobal("Quest_New_Objective");
        var questMarker = GameObject.FindWithTag("Quest Marker");
        if (questMarker != null)
        {
            questMarker.transform.position = quest.currentObjective.location.position + quest.currentObjective.offset;
        }
    }

    //IEnumerator StartMainQuest()
    //{
    //    yield return new WaitForSeconds(startQuestDelay);
    //    AddQuest(mainQuest);
    //    // testing
    //    //AddQuest(QuestEnum.MilkDrinker);
    //}
}
