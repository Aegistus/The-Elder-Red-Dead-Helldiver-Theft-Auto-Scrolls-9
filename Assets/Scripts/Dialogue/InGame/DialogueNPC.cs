using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour, IInteractable
{
    [SerializeField] InGameDialogue dialogue;

    public string Description => "Talk to NPC";

    public void Interact(GameObject interactor)
    {
        InGameDialogueUI.Instance.OpenMenu(dialogue);
    }
}
