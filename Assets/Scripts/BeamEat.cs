using UnityEngine;

public class BeamEat : MonoBehaviour
{
    public float maxSpeed;

    public Transform eatAttractor;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.isKinematic = false;
    }

    void FixedUpdate()
    {
        Util.MoveRigidBodyTowardsTarget(_rigidBody, eatAttractor.position, maxSpeed);
    }
}
