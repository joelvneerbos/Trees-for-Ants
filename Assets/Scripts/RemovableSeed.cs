using System;
using UnityEngine;

public class RemovableSeed : MonoBehaviour
{
    public GameObject seedPrefab;
    public float seedSpawnOffset;

    public Action seedRemovedCallback;

    public void RemoveSeed()
    {
        Instantiate(seedPrefab, position: transform.position + new Vector3(0f, seedSpawnOffset, 0f), rotation: Quaternion.identity);
        Destroy(gameObject);
        seedRemovedCallback?.Invoke();
    }
}
