using TMPro;
using UnityEngine;

public class GrowStage : MonoBehaviour
{
    public TextMeshProUGUI label;

    public bool invalidPosition = false;

    void Start()
    {
        if (invalidPosition) { label.text = "<sprite name=invalid>"; }
    }

    void Update()
    {
        
    }
}
