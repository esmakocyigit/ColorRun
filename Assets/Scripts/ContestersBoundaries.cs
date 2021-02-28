using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestersBoundaries : MonoBehaviour
{
    [SerializeField]
    Transform maxTransform, minTransform;

    public Vector3 GetRandomPosition()
    {
        Vector3 randPosition = Vector3.zero;

        randPosition.x = Random.Range(minTransform.position.x, maxTransform.position.x);
        randPosition.z = Random.Range(minTransform.position.z, maxTransform.position.z);

        return randPosition;
    }
}
