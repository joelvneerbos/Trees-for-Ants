using TMPro;
using UnityEngine;

public class GrowStage : MonoBehaviour
{
    private const float _refillMultiplier = -10f;

    public float stageLength;
    public float rainDepletionTime;
    public float shadeDepletionTime;
    public float sunDepletionTime;

    private float stageProgress = 0f;
    private float rainDepletion = 0f;
    private float shadeDepletion = 0f;
    private float sunDepletion = 0f;

    public GameObject nextStagePrefab;
    public GameObject deadPrefab;

    public TextMeshProUGUI label;

    public Transform raycasterOrigin;

    public bool invalidPosition = false;

    public float claimedRadius;

    private SeedlingGrowth _seedlingGrowth;
    private TreeGrowth _treeGrowth;

    void Start()
    {
        if (invalidPosition) { label.text = "<sprite name=invalid>"; }

        if (TryGetComponent<SeedlingGrowth>(out var seedlingGrowth))
        {
            _seedlingGrowth = seedlingGrowth;
        }

        if (TryGetComponent<TreeGrowth>(out var treeGrowth))
        {
            _treeGrowth = treeGrowth;
            _treeGrowth.claimedRadius = claimedRadius;
        }
    }

    void Update()
    {
        if (invalidPosition) { return; }

        stageProgress += Time.deltaTime;
        if (stageProgress >= stageLength && nextStagePrefab != null) { NextStage(); return; }

        if (_seedlingGrowth != null) { _seedlingGrowth.SetGrowth(stageProgress / stageLength); }
        if (_treeGrowth != null) { _treeGrowth.SetGrowth(stageProgress / stageLength); }

        var (rain, shade, sun) = GetCurrentState();

        rainDepletion += Time.deltaTime * (rain ? _refillMultiplier : 1f);
        rainDepletion = Mathf.Max(0f, rainDepletion);
        if (rainDepletion > rainDepletionTime) { Die(); return; }

        shadeDepletion += Time.deltaTime * (shade ? _refillMultiplier : 1f);
        shadeDepletion = Mathf.Max(0f, shadeDepletion);
        if (shadeDepletion > shadeDepletionTime) { Die(); return; }

        sunDepletion += Time.deltaTime * (sun ? _refillMultiplier : 1f);
        sunDepletion = Mathf.Max(0f, sunDepletion);
        if (sunDepletion > sunDepletionTime) { Die(); return; }

        // show one icon less if being refilled to give immediate feedback to the player
        // (e.g. when a tree has 2 shade icons, it will change to 1 icon as soon as it gets shade)
        int rainIconCount = Mathf.FloorToInt(rainDepletion * 4f / rainDepletionTime) - (rain ? 1 : 0);
        int shadeIconCount = Mathf.FloorToInt(shadeDepletion * 4f / shadeDepletionTime) - (shade ? 1 : 0);
        int sunIconCount = Mathf.FloorToInt(sunDepletion * 4f / sunDepletionTime) - (sun ? 1 : 0);

        string text = "";
        for (int i = 0; i < rainIconCount; ++i) { text += "<sprite name=rain>"; }
        for (int i = 0; i < shadeIconCount; ++i) { text += "<sprite name=clouds>"; }
        for (int i = 0; i < sunIconCount; ++i) { text += "<sprite name=sun>"; }

        label.text = text;
    }

    private (bool rain, bool shade, bool sun) GetCurrentState()
    {
        bool rain = false;
        if (Physics.Raycast(raycasterOrigin.position, Vector3.up, out var hitInfo))
        {
            rain = hitInfo.collider.gameObject.name.StartsWith("Rain Cloud");
        }

        bool shade = Physics.Raycast(raycasterOrigin.position, SunDirection.sunDirection);
        return (rain, shade, !shade);
    }

    private void NextStage()
    {
        var nextStageGameObject = Instantiate(nextStagePrefab, transform.position, Quaternion.identity);
        nextStageGameObject.GetComponent<GrowStage>().claimedRadius = claimedRadius;
        Destroy(gameObject);
    }

    private void Die()
    {
        var deadGameObject = Instantiate(deadPrefab, transform.position, Quaternion.identity);
        if (_seedlingGrowth != null) { deadGameObject.GetComponent<SeedlingGrowth>().SetGrowth(stageProgress / stageLength); }
        if (_treeGrowth != null)
        {
            var treeGrowth = deadGameObject.GetComponent<TreeGrowth>();
            treeGrowth.claimedRadius = claimedRadius;
            treeGrowth.SetGrowth(stageProgress / stageLength);

            GetComponent<FruitSpawner>().DetachAllFruits();
        }
        Destroy(gameObject);
    }
}
