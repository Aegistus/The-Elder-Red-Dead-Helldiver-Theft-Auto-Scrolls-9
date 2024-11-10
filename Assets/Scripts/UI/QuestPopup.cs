using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestPopup : MonoBehaviour
{
    [SerializeField] TMP_Text popup;
    [SerializeField] float duration = 4f;

    public static QuestPopup Instance { get; private set; }

    Queue<string> popupQueue = new Queue<string>();

    bool showingPopup = false;
    readonly float popupDelay = 4f;
    float timer = 0;

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
        popup.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (popupQueue.Count > 0)
        {
            if (!showingPopup && timer <= 0)
            {
                ShowPopup(popupQueue.Dequeue());
            }
        }
        if (!showingPopup && timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void ShowQuestStartPopup(string questName)
    {
        popupQueue.Enqueue("Started: " + questName);
    }

    public void ShowQuestEndPopup(string questName)
    {
        popupQueue.Enqueue("Completed: " + questName);
    }

    void ShowPopup(string message)
    {
        popup.text = message;
        popup.gameObject.SetActive(true);
        showingPopup = true;
        StartCoroutine(HidePopup());
        SoundManager.Instance.PlaySoundGlobal("Quest_Start");
    }

    IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(duration);
        popup.gameObject.SetActive(false);
        timer = popupDelay;
        showingPopup = false;
    }
}
