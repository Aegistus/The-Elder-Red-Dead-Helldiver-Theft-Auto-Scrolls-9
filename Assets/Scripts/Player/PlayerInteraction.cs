using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    public event Action<bool, string> OnInteractStateChange;

    [SerializeField] Transform raycastOrigin;
    public LayerMask interactableLayers;
    public float interactionDistance = 1f;
    int openDoorSoundID;

    PlayerController playerController;
    AgentEquipment agentEquipment;
    Cow currentlyMilking;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        openDoorSoundID = SoundManager.Instance.GetSoundID("Car_Door_Open");
        agentEquipment = GetComponent<AgentEquipment>();
    }

    RaycastHit rayHit;
    void Update()
    {
        if (playerController.PauseInput)
        {
            return;
        }
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out rayHit, interactionDistance, interactableLayers))
        {
            IInteractable interactable = rayHit.collider.GetComponentInParent<IInteractable>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactable != null)
                {
                    interactable.Interact(gameObject);
                    if (interactable is Cow)
                    {
                        currentlyMilking = (Cow)interactable;
                    }
                }
                else
                {
                    print("No interactable");
                }
            }
            if (Input.GetKey(KeyCode.E))
            {
                if (currentlyMilking != null)
                {
                    currentlyMilking.Milk();
                }
                OnInteractStateChange?.Invoke(false, "");
            }
            if (interactable != null)
            {
                OnInteractStateChange?.Invoke(true, interactable.Description);
            }

            //print("In interaction range");
        }
        else
        {
            OnInteractStateChange?.Invoke(false, "");
            currentlyMilking = null;
        }
    }
}
