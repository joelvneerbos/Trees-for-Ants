using UnityEngine;

public class SunDirection : MonoBehaviour
{
    public static Vector3 sunDirection;

    void Start()
    {
        sunDirection = transform.forward * -1f;
    }
}
