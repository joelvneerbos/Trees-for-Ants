using UnityEngine;

public class EatFruit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BeamEat>(out var beamEat))
        {
            Destroy(beamEat.gameObject);
        }
    }
}
