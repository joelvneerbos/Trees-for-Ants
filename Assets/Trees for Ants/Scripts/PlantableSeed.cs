using UnityEngine;

public class PlantableSeed : MonoBehaviour
{
    public GameObject plantedSeedPrefab;

    public void PlantSeed(Vector3 position, System.Action seedRemovedCallback)
    {
        var plantedSeed = Instantiate(plantedSeedPrefab, position, rotation: Quaternion.identity);
        plantedSeed.GetComponent<RemovableSeed>().seedRemovedCallback = seedRemovedCallback;
        Destroy(gameObject);
    }
}
