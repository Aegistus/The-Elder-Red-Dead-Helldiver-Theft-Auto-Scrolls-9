using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestPopup : MonoBehaviour
{
    [SerializeField] TMP_Text popup;
    [SerializeField] float duration = 4f;

    public static QuestPopup Instance { get; private set; }

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

    public void ShowQuestStartPopup(string questName)
    {
        popup.text = "Started: " + questName;
        popup.gameObject.SetActive(true);
        StartCoroutine(HidePopup());
    }

    public void ShowQuestEndPopup(string questName)
    {
        popup.text = "Completed: " + questName;
        popup.gameObject.SetActive(true);
        StartCoroutine(HidePopup());
    }

    IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(duration);
        popup.gameObject.SetActive(false);
    }
}
