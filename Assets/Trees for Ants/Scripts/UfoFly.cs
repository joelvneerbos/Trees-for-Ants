using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UfoFly : MonoBehaviour
{
    public Transform targetController;
    public float targetHorizontalOffset = 0.1f;
    public float targetVerticalOffset = 0.1f;
    public float maxSpeed = 2f;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // get the position at the specified horizontal offset in the forward direction of the controller, and then moved up by another offset
        var targetPosition = targetController.position
            + new Vector3(targetController.forward.x, 0f, targetController.forward.z).normalized * targetHorizontalOffset
            + new Vector3(0f, targetVerticalOffset, 0f);

        var positionDifference = targetPosition - _rigidBody.transform.position;

        // only go at maximum speed if the target is far away, to make sure the UFO does not move past its target
        var desiredVelocity = positionDifference.magnitude <= maxSpeed * Time.fixedDeltaTime
            ? positionDifference / Time.fixedDeltaTime
            : positionDifference.normalized * maxSpeed;

        var velocityDifference = desiredVelocity - _rigidBody.linearVelocity;
        _rigidBody.AddForce(velocityDifference, ForceMode.VelocityChange);
    }
}
