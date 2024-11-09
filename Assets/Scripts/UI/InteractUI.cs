using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUI : MonoBehaviour
{
    [SerializeField] GameObject uiElement;
    [SerializeField] TMP_Text descriptionText;

    PlayerInteraction interaction;

    private void Start()
    {
        uiElement.SetActive(false);
        interaction = FindObjectOfType<PlayerInteraction>();
        interaction.OnInteractStateChange += Interaction_OnInteractStateChange;
    }

    private void Interaction_OnInteractStateChange(bool value, string description)
    {
        uiElement.SetActive(value);
        descriptionText.text = description;
    }

    private void OnDestroy()
    {
        interaction.OnInteractStateChange -= Interaction_OnInteractStateChange;
    }
}
