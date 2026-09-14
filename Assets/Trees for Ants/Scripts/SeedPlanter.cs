using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlantableSeed>(out var plantableSeed))
        {
            plantableSeed.PlantSeed(transform.position.y);
        }
    }
}
