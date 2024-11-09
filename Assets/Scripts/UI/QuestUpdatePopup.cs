using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUpdatePopup : MonoBehaviour
{
    [SerializeField] TMP_Text popup;
    [SerializeField] float duration = 4f;

    public static QuestUpdatePopup Instance { get; private set; }

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

    public void ShowPopup(string questName)
    {
        popup.text = questName;
        popup.gameObject.SetActive(true);
        StartCoroutine(HidePopup());
    }

    IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(duration);
        popup.gameObject.SetActive(false);
    }
}
