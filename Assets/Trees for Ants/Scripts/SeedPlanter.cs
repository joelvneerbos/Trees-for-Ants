using System.Collections.Generic;
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    public GameObject claimedAreaPrefab;

    private HashSet<Transform> _claimedAreaTransforms = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlantableSeed>(out var plantableSeed))
        {
            var position = new Vector3(plantableSeed.transform.position.x, transform.position.y, plantableSeed.transform.position.z);

            float? radius = GetRadius(position, plantableSeed.minRadius, plantableSeed.maxRadius);

            if (radius.HasValue)
            {
                var claimedArea = Instantiate(claimedAreaPrefab, position, rotation: Quaternion.identity);
                claimedArea.transform.localScale = new Vector3(radius.Value * 2f, claimedArea.transform.localScale.y, radius.Value * 2f);
                _claimedAreaTransforms.Add(claimedArea.transform);

                plantableSeed.PlantSeed(position, () => RemoveClaim(claimedArea));
            }
            else
            {
                plantableSeed.PlantSeedOnInvalidLocation(position);
            }
        }
    }

    // returns null if there is no valid radius larger than or equal to minRadius
    private float? GetRadius(Vector3 position, float minRadius, float maxRadius)
    {
        float entireMapRadius = transform.localScale.x * 0.5f;

        // decrease max radius if near the border of the map
        maxRadius = Mathf.Min(maxRadius, entireMapRadius - (position - transform.position).magnitude);

        // decrease max radius if near another claimed area
        foreach (var otherTransform in _claimedAreaTransforms)
        {
            var otherRadius = otherTransform.localScale.x * 0.5f;
            maxRadius = Mathf.Min(maxRadius, (position - otherTransform.position).magnitude - otherRadius);
            if (maxRadius < minRadius) { return null; }
        }

        return maxRadius >= minRadius ? maxRadius : null;
    }

    private void RemoveClaim(GameObject claimedArea)
    {
        _claimedAreaTransforms.Remove(claimedArea.transform);
        Destroy(claimedArea);
    }
}
