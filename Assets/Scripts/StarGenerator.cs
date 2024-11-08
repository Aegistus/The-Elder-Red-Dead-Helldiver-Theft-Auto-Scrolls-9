using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StarGenerator : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] int max = 1000;
    [SerializeField] Vector2 negativeExtents;
    [SerializeField] Vector2 positiveExtents;
    [SerializeField] float zValue = -500;

    public void GenerateStars()
    {
        for (int i = 0; i < max; i++)
        {
            Vector3 position = transform.InverseTransformPoint(new Vector3(Random.Range(negativeExtents.x, positiveExtents.x), Random.Range(negativeExtents.y, positiveExtents.y), zValue));
            position += transform.position;
            Instantiate(starPrefab, position, Quaternion.identity, transform);
        }
    }

    public void ResetStars()
    {
        while (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
