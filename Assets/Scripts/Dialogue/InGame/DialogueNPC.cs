using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour, IInteractable
{
    [SerializeField] InGameDialogue dialogue;
    [SerializeField] Camera dialogueCamera;

    public string Description => "Talk to NPC";

    GameObject mainCam;

    public void Interact(GameObject interactor)
    {
        InGameDialogueUI.Instance.OpenMenu(dialogue, this);
        mainCam = Camera.main.gameObject;
        mainCam.SetActive(false);
        dialogueCamera.gameObject.SetActive(true);
    }

    public void DeactivateCamera()
    {
        mainCam.SetActive(true);
        dialogueCamera.gameObject.SetActive(false);
    }
}
