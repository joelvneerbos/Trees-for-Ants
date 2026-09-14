using TMPro;
using UnityEngine;

public class GrowStage : MonoBehaviour
{
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

        string rain = "";
        if (Physics.Raycast(raycasterOrigin.position, Vector3.up, out var hitInfo))
        {
            if (hitInfo.collider.gameObject.name == "Rain Cloud") { rain = "<sprite name=rain>"; }
        }

        string clouds = "";
        string sun = "";
        if (Physics.Raycast(raycasterOrigin.position, SunDirection.sunDirection))
        {
            clouds = "<sprite name=clouds>";
        }
        else
        {
            sun = "<sprite name=sun>";
        }

        label.text = $"{rain}{clouds}{sun}";
    }
}
