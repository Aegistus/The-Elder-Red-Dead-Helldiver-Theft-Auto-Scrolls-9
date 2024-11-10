using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameDialogueUI : MonoBehaviour
{
    public static InGameDialogueUI Instance { get; private set; }

    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] Transform responseParent;
    [SerializeField] GameObject responsePrefab;

    InGameDialogue currentDialogue;
    int frameIndex = -1;
    int currentResponse = 0;
    bool menuOpen = false;
    bool needToUpdateHighlight = false;
    DialogueNPC npc;

    readonly float inputDelay = .5f;
    float timer = 0f;

    GameObject questMarker;
    bool aborted = false;

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
            if (needToUpdateHighlight)
            {
                UpdateHighlightedResponse();
                needToUpdateHighlight = false;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                currentResponse = currentResponse == 0 ? currentDialogue.frames[frameIndex].responses.Length - 1 : currentResponse - 1;
                UpdateHighlightedResponse();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentResponse = (currentResponse + 1) % currentDialogue.frames[frameIndex].responses.Length;
                UpdateHighlightedResponse();
            }
            if (Input.GetKeyDown(KeyCode.E) && timer <= 0)
            {
                NextFrame();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                aborted = true;
                CloseMenu();
            }
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void OpenMenu(InGameDialogue dialogue, DialogueNPC npc)
    {
        this.npc = npc;
        currentDialogue = dialogue;
        frameIndex = 0;
        menu.SetActive(true);
        characterName.text = currentDialogue.characterName;
        frameIndex = -1;
        NextFrame();
        FindAnyObjectByType<PlayerController>().PauseInput = true;
        SoundManager.Instance.PlaySoundGlobal("Dialogue_Theme");
        menuOpen = true;
        timer = inputDelay;
        questMarker = GameObject.FindGameObjectWithTag("Quest Marker");
        questMarker.SetActive(false);
    }

    public void CloseMenu()
    {
        if (npc)
        {
            npc.CompleteDialogue(aborted);
        }
        menu.SetActive(false);
        StartCoroutine(ReactivateInput());
        SoundManager.Instance.StopPlayingGlobal("Dialogue_Theme");
        menuOpen = false;
        questMarker?.SetActive(true);
    }

    IEnumerator ReactivateInput()
    {
        yield return new WaitForSeconds(.5f);
        FindAnyObjectByType<PlayerController>().PauseInput = false;
    }
    public void NextFrame()
    {
        frameIndex++;
        if (frameIndex >= currentDialogue.frames.Length)
        {
            aborted = false;
            CloseMenu();
            return;
        }
        dialogueText.text = currentDialogue.frames[frameIndex].characterDialogue;
        for (int i = 0; i < responseParent.childCount; i++)
        {
            Destroy(responseParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < currentDialogue.frames[frameIndex].responses.Length; i++)
        {
            Instantiate(responsePrefab, responseParent).GetComponent<TMP_Text>().text = currentDialogue.frames[frameIndex].responses[i];
        }
        currentResponse = 0;
        needToUpdateHighlight = true;
        timer = inputDelay;
    }

    public void UpdateHighlightedResponse()
    {
        for (int i = 0; i < responseParent.childCount; i++)
        {
            var response = responseParent.GetChild(i).GetComponent<TMP_Text>();
            response.fontStyle = FontStyles.Normal;
        }
        var highlightedResponse = responseParent.GetChild(currentResponse).GetComponent<TMP_Text>();
        highlightedResponse.fontStyle = FontStyles.Underline;
    }

}
