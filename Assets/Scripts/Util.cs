using UnityEngine;

public static class Util
{
    // Should only be called from FixedUpdate()
    public static void MoveRigidBodyTowardsTarget(Rigidbody rigidbody, Vector3 targetPosition, float maxSpeed)
    {
        var positionDifference = targetPosition - rigidbody.position;

        // Only go at maximum speed if the target is far away, to make sure we do not move past the target
        var desiredVelocity = positionDifference.magnitude <= maxSpeed * Time.fixedDeltaTime
            ? positionDifference / Time.fixedDeltaTime
            : positionDifference.normalized * maxSpeed;

        var velocityChange = desiredVelocity - rigidbody.linearVelocity;
        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
