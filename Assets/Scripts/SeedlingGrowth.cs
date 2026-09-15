using UnityEngine;

public class SeedlingGrowth : MonoBehaviour
{
    public Transform shootTransform;
    public Transform labelTransform;
    public Transform raycasterTransform;

    // input should be between 0.0 (initial state) and 1.0 (fully grown)
    public void SetGrowth(float t)
    {
        float height = Mathf.Lerp(1f, 3f, t);

        shootTransform.localPosition = new Vector3(0f, 1f + height * 0.5f, 0f);
        shootTransform.localScale = new Vector3(1f, height * 0.5f, 1f);

        labelTransform.localPosition = new Vector3(0f, 6.5f + height, 0f);

        raycasterTransform.localPosition = labelTransform.localPosition;
    }
}
