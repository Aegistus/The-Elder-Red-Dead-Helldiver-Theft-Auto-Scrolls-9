using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBookUI : MonoBehaviour
{
    [SerializeField] TMP_Text questTitle;
    [SerializeField] TMP_Text currentQuestTitle;
    [SerializeField] TMP_Text currentQuestDescription;
    [SerializeField] Transform questObjectiveParent;
    [SerializeField] GameObject questObjectivePrefab;

    public bool QuestBookOpen { get; private set; }

    QuestManager questManager;

    private void Start()
    {
        questManager = QuestManager.Instance;
        CloseBook();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (QuestBookOpen)
            {
                CloseBook();
            }
            else
            {
                OpenBook();
            }
        }
    }

    public void OpenBook()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        UpdateBook();
        QuestBookOpen = true;
        Time.timeScale = 0;
    }

    public void CloseBook()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        QuestBookOpen = false;
        Time.timeScale = 1;
    }

    public void UpdateBook()
    {
        if (questManager.currentQuests.Count == 0)
        {
            return;
        }
        questTitle.text = questManager.currentQuests[0].title;
        currentQuestTitle.text = questManager.currentQuests[0].title;
        currentQuestDescription.text = questManager.currentQuests[0].description;
        for (int i = 0; i < questObjectiveParent.childCount; i++)
        {
            Destroy(questObjectiveParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < questManager.currentQuests[0].unlockedObjectives.Count; i++)
        {
            var objectiveUI = Instantiate(questObjectivePrefab, questObjectiveParent);
            objectiveUI.GetComponent<TMP_Text>().text = questManager.currentQuests[0].unlockedObjectives[i].description;
        }
    }
}
