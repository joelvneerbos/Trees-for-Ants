using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;

    public Transform[] spawnLocations;

    public Transform leavesTransform;

    private float _spawnProgress = 0f;

    private GameObject[] spawnedFruits;

    private void Awake()
    {
        spawnedFruits = new GameObject[spawnLocations.Length];
    }

    void Start()
    {
        leavesTransform.transform.localEulerAngles = new Vector3(0f, Random.Range(0f, 360f), 0f);
    }

    void Update()
    {
        for (int i = 0; i < spawnedFruits.Length; ++i)
        {
            if (spawnedFruits[i] != null)
            {
                if (!spawnedFruits[i].GetComponent<Rigidbody>().isKinematic)
                {
                    // fruit has been picked up, clear its spot
                    spawnedFruits[i] = null;
                }
                else
                {
                    spawnedFruits[i].transform.position = spawnLocations[i].position;
                }
            }
        }

        _spawnProgress += (leavesTransform.localScale.x * leavesTransform.localScale.x) * Time.deltaTime * (1 / 700f);
        
        if (_spawnProgress > 1f)
        {
            _spawnProgress -= 1f;
            TrySpawnFruit();
        }
    }

    private void TrySpawnFruit()
    {
        for (int i = 0; i < spawnLocations.Length; ++i)
        {
            if (spawnedFruits[i] == null)
            {
                spawnedFruits[i] = Instantiate(fruitPrefab, spawnLocations[i].position, Quaternion.identity);
                return;
            }
        }
    }

    public void DetachAllFruits()
    {
        for (int i = 0; i < spawnedFruits.Length; ++i)
        {
            if (spawnedFruits[i] != null)
            {
                spawnedFruits[i].GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
