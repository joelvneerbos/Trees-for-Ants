using UnityEngine;

public class PlantableSeed : MonoBehaviour
{
    public GameObject plantedSeedPrefab;

    public void PlantSeed(float yPosition)
    {
        Instantiate(plantedSeedPrefab, position: new Vector3(transform.position.x, yPosition, transform.position.z), rotation: Quaternion.identity);
        Destroy(gameObject);
    }
}
