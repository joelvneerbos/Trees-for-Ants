using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform _mainCameraTransform;

    void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(new Vector3(_mainCameraTransform.position.x, transform.position.y, _mainCameraTransform.position.z), Vector3.up);
    }
}
