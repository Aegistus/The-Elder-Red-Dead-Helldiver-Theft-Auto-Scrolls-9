using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.PhysicsExtension;
using Physics = RotaryHeart.Lib.PhysicsExtension.Physics;
using UnityEngine.AI;

public class PlayerShouts : MonoBehaviour
{
    [SerializeField] float shoutForce = 30f;
    [SerializeField] float shoutDistance = 10f;
    [SerializeField] float shoutRadius = 5f;
    [SerializeField] float cooldown = 5f;
    [SerializeField] LayerMask layers;
    [SerializeField] ParticleSystem particleEffects;

    float cooldownTimer;
    public bool ShoutsUnlocked { get; set; } = true;

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.V) && ShoutsUnlocked && cooldownTimer <= 0)
        {
            Shout();
            cooldownTimer = cooldown;
        }
    }

    public void Shout()
    {
        RaycastHit[] sphereHits = Physics.SphereCastAll(new Ray(transform.position, transform.forward), shoutRadius, shoutDistance, layers, drawType: CastDrawType.Minimal, drawDuration: 5f, preview: PreviewCondition.Editor);
        for (int i = 0; i < sphereHits.Length; i++)
        {
            if (sphereHits[i].collider.GetComponent<PlayerController>())
            {
                continue;
            }
            Rigidbody rb = sphereHits[i].collider.attachedRigidbody;
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(Random.onUnitSphere * shoutForce, ForceMode.Impulse);
            }
            var navAgent = sphereHits[i].collider.GetComponent<NavMeshAgent>();
            if (navAgent)
            {
                navAgent.enabled = false;
            }
            print(sphereHits[i].collider.gameObject.name);
        }
        particleEffects.Play();
    }
}
