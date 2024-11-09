using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingCow : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, collision.collider.transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySoundAtPosition("Explosion", transform.position);
    }
}
