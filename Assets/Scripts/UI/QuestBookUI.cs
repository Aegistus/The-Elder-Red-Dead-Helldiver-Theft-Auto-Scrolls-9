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

    PlayerQuests playerQuests;

    private void Start()
    {
        playerQuests = FindAnyObjectByType<PlayerQuests>();
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
    }

    public void CloseBook()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        QuestBookOpen = false;
    }

    public void UpdateBook()
    {
        questTitle.text = playerQuests.currentQuests[0].title;
        currentQuestTitle.text = playerQuests.currentQuests[0].title;
        currentQuestDescription.text = playerQuests.currentQuests[0].description;
        for (int i = 0; i < questObjectiveParent.childCount; i++)
        {
            Destroy(questObjectiveParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < playerQuests.currentQuests[0].checkpoints.Length; i++)
        {
            var objectiveUI = Instantiate(questObjectivePrefab, questObjectiveParent);
            objectiveUI.GetComponent<TMP_Text>().text = playerQuests.currentQuests[0].checkpoints[i].description;
        }
    }
}
