using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] float startQuestDelay = 3f;
    [SerializeField] Quest mainQuest;
    private void Start()
    {
        StartCoroutine(StartMainQuest());
    }

    IEnumerator StartMainQuest()
    {
        yield return new WaitForSeconds(startQuestDelay);
        FindAnyObjectByType<PlayerQuests>().AddQuest(mainQuest);
    }
}
