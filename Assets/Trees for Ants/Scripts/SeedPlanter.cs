using System.Collections.Generic;
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    public GameObject claimedAreaPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlantableSeed>(out var plantableSeed))
        {
            var position = new Vector3(plantableSeed.transform.position.x, transform.position.y, plantableSeed.transform.position.z);

            var claimedArea = Instantiate(claimedAreaPrefab, position, rotation: Quaternion.identity);
            float diameter = 0.12f;
            claimedArea.transform.localScale = new Vector3(diameter, claimedArea.transform.localScale.y, diameter);

            plantableSeed.PlantSeed(position, () => RemoveClaim(claimedArea));
        }
    }

    private void RemoveClaim(GameObject claimedArea)
    {
        Destroy(claimedArea);
    }
}
