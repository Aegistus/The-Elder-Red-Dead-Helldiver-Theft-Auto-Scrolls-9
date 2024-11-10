using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class DialogueNPC : MonoBehaviour, IInteractable
{
    [SerializeField] InGameDialogue dialogue;
    [SerializeField] Camera dialogueCamera;
    public UltEvent OnDialogueComplete;

    public string Description => "Talk to NPC";

    GameObject mainCam;

    public void Interact(GameObject interactor)
    {
        InGameDialogueUI.Instance.OpenMenu(dialogue, this);
        mainCam = Camera.main.gameObject;
        mainCam.SetActive(false);
        dialogueCamera.gameObject.SetActive(true);
    }

    public void CompleteDialogue(bool aborted)
    {
        mainCam.SetActive(true);
        dialogueCamera.gameObject.SetActive(false);
        if (!aborted)
        {
            OnDialogueComplete.Invoke();
        }
    }
}
