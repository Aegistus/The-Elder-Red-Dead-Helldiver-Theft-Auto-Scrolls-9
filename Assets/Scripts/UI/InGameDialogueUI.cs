using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameDialogueUI : MonoBehaviour
{
    public static InGameDialogueUI Instance { get; private set; }

    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text characterName;
    [SerializeField] Transform responseParent;
    [SerializeField] GameObject responsePrefab;

    InGameDialogue currentDialogue;
    int frameIndex = -1;
    int currentResponse = 0;
    bool menuOpen = false;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        CloseMenu();
    }

    private void Update()
    {
        if (menuOpen)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentResponse = (currentResponse + 1) % currentDialogue.frames[frameIndex].responses.Length;
                UpdateHighlightedResponse();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentResponse = (currentResponse - 1) % currentDialogue.frames[frameIndex].responses.Length;
                UpdateHighlightedResponse();
            }
            if (Input.GetKey(KeyCode.E))
            {
                NextFrame();
            }
        }
    }

    public void OpenMenu(InGameDialogue dialogue)
    {
        currentDialogue = dialogue;
        frameIndex = 0;
        menu.SetActive(true);
        characterName.text = currentDialogue.characterName;
        frameIndex = -1;
        NextFrame();
        UpdateHighlightedResponse();
        menuOpen = true;
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        menuOpen = false;
    }
    public void NextFrame()
    {
        frameIndex++;
        for (int i = 0; i < responseParent.childCount; i++)
        {
            Destroy(responseParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < currentDialogue.frames[frameIndex].responses.Length; i++)
        {
            Instantiate(responsePrefab, responseParent).GetComponent<TMP_Text>().text = currentDialogue.frames[frameIndex].responses[i];
        }
        currentResponse = 0;
        UpdateHighlightedResponse();
    }

    public void UpdateHighlightedResponse()
    {
        for (int i = 0; i < responseParent.childCount; i++)
        {
            var response = responseParent.GetChild(i).GetComponent<TMP_Text>();
            response.fontStyle = FontStyles.Normal;
        }
        var highlightedResponse = responseParent.GetChild(currentResponse).GetComponent<TMP_Text>();
        highlightedResponse.fontStyle = FontStyles.Bold | FontStyles.Underline;
    }

}
