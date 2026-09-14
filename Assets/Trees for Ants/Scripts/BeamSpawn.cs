using UnityEngine;

public class BeamSpawn : MonoBehaviour
{
    public GameObject spawnPrefab;

    public void SpawnObject(Vector3 beamPosition)
    {
        Instantiate(spawnPrefab, position: new Vector3(beamPosition.x, transform.position.y, beamPosition.z), rotation: Quaternion.identity);
    }
}
