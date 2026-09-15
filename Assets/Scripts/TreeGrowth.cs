using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    public Transform trunkTransform;
    public Transform leavesTransform;
    public Transform labelTransform;
    public Transform raycasterTransform;

    public float claimedRadius;

    // input should be between 0.0 (initial state) and 1.0 (fully grown)
    public void SetGrowth(float t)
    {
        float height = Mathf.Lerp(3f, claimedRadius * 140f, t);

        trunkTransform.localScale = Vector3.one * height * 0.4f;

        leavesTransform.localPosition = new Vector3(0f, 0.7f + height * 0.6f, 0f);
        leavesTransform.localScale = Vector3.one * height;

        labelTransform.localPosition = new Vector3(0f, 6.5f + height * 1.1f, 0f);

        raycasterTransform.localPosition = labelTransform.localPosition;
    }
}
