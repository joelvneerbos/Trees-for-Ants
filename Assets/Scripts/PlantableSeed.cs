using UnityEngine;

public class PlantableSeed : MonoBehaviour
{
    public GameObject plantedSeedPrefab;

    public float minRadius;
    public float maxRadius;

    public void PlantSeed(Vector3 position, float claimedRadius, System.Action seedRemovedCallback)
    {
        var plantedSeed = PlantSeed(position);
        plantedSeed.GetComponent<GrowStage>().claimedRadius = claimedRadius;
        plantedSeed.GetComponent<RemovableSeed>().seedRemovedCallback = seedRemovedCallback;
    }

    public void PlantSeedOnInvalidLocation(Vector3 position)
    {
        var plantedSeed = PlantSeed(position);
        plantedSeed.GetComponent<GrowStage>().invalidPosition = true;
    }

    private GameObject PlantSeed(Vector3 position)
    {
        var plantedSeed = Instantiate(plantedSeedPrefab, position, rotation: Quaternion.identity);
        Destroy(gameObject);
        return plantedSeed;
    }
}
