using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSize : MonoBehaviour
{
    [SerializeField] float size = 1f;
    private void Update()
    {
        if (Camera.main != null)
        {
            var dist = Vector3.Distance(transform.position, Camera.main.transform.position);
            transform.localScale = dist * size * Vector3.one;
        }
    }
}
