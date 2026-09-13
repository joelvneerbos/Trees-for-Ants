using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UfoFly : MonoBehaviour
{
    public Transform leftController;
    public Transform rightController;
    public Transform targetController;

    public float targetHorizontalOffset;
    public float targetVerticalOffset;
    public float maxSpeed;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get the position at the specified horizontal offset in the forward direction of the controller, and then moved up by another offset
        var targetPosition = targetController.position
            + new Vector3(targetController.forward.x, 0f, targetController.forward.z).normalized * targetHorizontalOffset
            + new Vector3(0f, targetVerticalOffset, 0f);

        Util.MoveRigidBodyTowardsTarget(_rigidBody, targetPosition, maxSpeed);
    }

    public void FollowLeftController()
    {
        targetController = leftController;
    }

    public void FollowRightController()
    {
        targetController = rightController;
    }
}
