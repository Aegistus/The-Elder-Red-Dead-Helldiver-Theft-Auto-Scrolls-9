using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.PhysicsExtension;
using Physics = RotaryHeart.Lib.PhysicsExtension.Physics;
using UnityEngine.AI;
using UltEvents;

public class PlayerShouts : MonoBehaviour
{
    public UltEvent OnFirstShout;

    [SerializeField] float shoutForce = 30f;
    [SerializeField] float shoutRadius = 5f;
    [SerializeField] float cooldown = 5f;
    [SerializeField] LayerMask layers;
    [SerializeField] ParticleSystem particleEffects;
    [SerializeField] ParticleSystem shockWave;

    float shoutDelay = 1.1f;
    float cooldownTimer;
    bool usedFirstShout = false;
    public bool ShoutsUnlocked { get; set; } = false;

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
        particleEffects.Play();
        SoundManager.Instance.PlaySoundGlobal("Skyrim_Shout");
        StartCoroutine(DelayedEffect());
    }

    IEnumerator DelayedEffect()
    {
        yield return new WaitForSeconds(shoutDelay);
        Collider[] collisions = Physics.OverlapSphere(transform.position, shoutRadius, layers, drawDuration: 5f, preview: PreviewCondition.Editor);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].GetComponent<PlayerController>() || collisions[i] is MeshCollider)
            {
                continue;
            }
            Rigidbody rb = collisions[i].attachedRigidbody;
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(Random.onUnitSphere * shoutForce, ForceMode.Impulse);
            }
            var agentHealth = collisions[i].GetComponent<AgentHealth>();
            if (agentHealth)
            {
                agentHealth.Kill();
            }
            print(collisions[i].gameObject.name);
        }
        shockWave.Play();
        SoundManager.Instance.PlaySoundAtPosition("Explosion", transform.position);
        if (!usedFirstShout)
        {
            OnFirstShout?.Invoke();
            usedFirstShout = true;
        }
    }
}
