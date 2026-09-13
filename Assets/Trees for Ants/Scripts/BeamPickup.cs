using UnityEngine;

public class BeamPickup : MonoBehaviour
{
    public float maxSpeed;

    public Transform beamAttractor;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (beamAttractor.gameObject.activeInHierarchy)
        {
            Util.MoveRigidBodyTowardsTarget(_rigidBody, beamAttractor.position, maxSpeed);
        }
        else
        {
            enabled = false;
        }
    }
}
