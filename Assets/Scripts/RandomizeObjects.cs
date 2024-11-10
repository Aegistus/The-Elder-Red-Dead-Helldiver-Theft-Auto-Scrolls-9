using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Randomly rotates children around y-axis and adjusts scale slightly. Good for randomizing terrain
/// </summary>
public class RandomizeObjects : MonoBehaviour
{
    [SerializeField] float minScale = .9f;
    [SerializeField] float maxScale = 1.1f;

    public void Randomize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.eulerAngles = new(child.eulerAngles.x, Random.Range(0, 360), child.eulerAngles.z);
            float scaleFactor = Random.Range(minScale, maxScale);
            child.localScale *= scaleFactor;
        }
    }
}
