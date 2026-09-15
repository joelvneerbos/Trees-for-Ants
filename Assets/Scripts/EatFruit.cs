using UnityEngine;

public class EatFruit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BeamEat>(out var beamEat))
        {
            Score.IncrementScore(beamEat.scoreType);
            Destroy(beamEat.gameObject);
        }
    }
}
