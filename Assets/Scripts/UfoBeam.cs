using UnityEngine;

public class UfoBeam : MonoBehaviour
{
    public GameObject beamTrigger;
    public Transform beamAttractor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BeamPickup>(out var beamPickup))
        {
            beamPickup.beamAttractor = beamAttractor;
            beamPickup.enabled = true;
        }

        if (other.TryGetComponent<BeamSpawn>(out var beamSpawn))
        {
            beamSpawn.SpawnObject(beamTrigger.transform.position);
        }

        if (other.TryGetComponent<RemovableSeed>(out var removableSeed))
        {
            removableSeed.RemoveSeed();
        }
    }
}
