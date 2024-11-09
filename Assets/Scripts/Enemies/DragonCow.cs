using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCow : MonoBehaviour
{
    [SerializeField] GameObject milkableCowPrefab;

    AgentHealth health;

    private void Start()
    {
        health = GetComponent<AgentHealth>();
        health.OnAgentDeath += Health_OnAgentDeath;
    }

    private void Health_OnAgentDeath()
    {
        Instantiate(milkableCowPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
