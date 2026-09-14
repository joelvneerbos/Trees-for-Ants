using TMPro;
using UnityEngine;

public class GrowStage : MonoBehaviour
{
    private const float _refillMultiplier = -5f;

    public float stageLength;
    public float rainDepletionTime;
    public float shadowDepletionTime;
    public float sunDepletionTime;

    private float stageProgress = 0f;
    private float rainDepletion = 0f;
    private float shadowDepletion = 0f;
    private float sunDepletion = 0f;

    public GameObject nextStagePrefab;
    public GameObject deadPrefab;

    public TextMeshProUGUI label;

    public Transform raycasterOrigin;

    public bool invalidPosition = false;

    void Start()
    {
        if (invalidPosition) { label.text = "<sprite name=invalid>"; }
    }

    void Update()
    {
        if (invalidPosition) { return; }

        stageProgress += Time.deltaTime;
        if (stageProgress >= stageLength) { NextStage(); return; }

        var (rain, shadow, sun) = GetCurrentState();

        rainDepletion += Time.deltaTime * (rain ? _refillMultiplier : 1f);
        rainDepletion = Mathf.Max(0f, rainDepletion);
        if (rainDepletion > rainDepletionTime) { Die(); return; }

        shadowDepletion += Time.deltaTime * (shadow ? _refillMultiplier : 1f);
        shadowDepletion = Mathf.Max(0f, shadowDepletion);
        if (shadowDepletion > shadowDepletionTime) { Die(); return; }

        sunDepletion += Time.deltaTime * (sun ? _refillMultiplier : 1f);
        sunDepletion = Mathf.Max(0f, sunDepletion);
        if (sunDepletion > sunDepletionTime) { Die(); return; }

        int rainIconCount = Mathf.FloorToInt(rainDepletion * 4f / rainDepletionTime) - (rain ? 1 : 0);
        int shadowIconCount = Mathf.FloorToInt(shadowDepletion * 4f / shadowDepletionTime) - (shadow ? 1 : 0);
        int sunIconCount = Mathf.FloorToInt(sunDepletion * 4f / sunDepletionTime) - (sun ? 1 : 0);

        string text = "";
        for (int i = 0; i < rainIconCount; ++i) { text += "<sprite name=rain>"; }
        for (int i = 0; i < shadowIconCount; ++i) { text += "<sprite name=clouds>"; }
        for (int i = 0; i < sunIconCount; ++i) { text += "<sprite name=sun>"; }

        label.text = text;
    }

    private (bool rain, bool shadow, bool sun) GetCurrentState()
    {
        bool rain = false;
        if (Physics.Raycast(raycasterOrigin.position, Vector3.up, out var hitInfo))
        {
            rain = hitInfo.collider.gameObject.name == "Rain Cloud";
        }

        bool shadow = Physics.Raycast(raycasterOrigin.position, SunDirection.sunDirection);
        return (rain, shadow, !shadow);
    }

    private void NextStage()
    {
        Destroy(gameObject);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
