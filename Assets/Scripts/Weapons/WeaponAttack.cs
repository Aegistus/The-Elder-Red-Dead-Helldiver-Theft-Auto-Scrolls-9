using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static AgentEquipment;

public abstract class WeaponAttack : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public UnityEvent OnRecoil;

    public Vector3 HoldOffset => holdOffset;

    [SerializeField] protected Vector3 holdOffset;
    [SerializeField] protected float damageMin = 10f;
    [SerializeField] protected float damageMax = 20f;
    public CameraShake.Properties camShakeProperties;

    public DamageSource Source { get; set; }

    public string Description => "Pickup Weapon";

    public abstract void BeginAttack();
    public abstract void DuringAttack();
    public abstract void EndAttack();

    public void Interact(GameObject interactor)
    {
        var agentEquipment = interactor.GetComponent<AgentEquipment>();
        agentEquipment.PickupWeapon(gameObject);
    }
}
