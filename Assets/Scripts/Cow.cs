using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

public class Cow : MonoBehaviour, IInteractable
{
    public UltEvent OnMilkedToCompletion;
    public float MilkMax => milkMax;
    public float MilkRemaining => milkRemaining;

    public string Description => "Milk";

    [SerializeField] MeshRenderer rend;
    [SerializeField] float milkMax;
    [SerializeField] float milkSpeed;

    float milkRemaining;
    bool calledEvent = false;

    private void Start()
    {
        milkRemaining = milkMax;
    }

    public void Milk()
    {
        float amount = Time.deltaTime * milkSpeed;
        if (amount >= milkRemaining)
        {
            amount = milkRemaining;
        }
        GameManager.Instance.ReduceMilkDebt(amount);
        milkRemaining -= amount;
        Color color = rend.material.color;
        color.r = milkRemaining / milkMax;
        color.g = milkRemaining / milkMax;
        color.b = milkRemaining / milkMax;
        rend.material.color = color;
        if (milkRemaining <= 0 && !calledEvent)
        {
            OnMilkedToCompletion?.Invoke();
            calledEvent = true;
        }
    }

    public void Interact(GameObject interactor)
    {
        Milk();
    }
}
