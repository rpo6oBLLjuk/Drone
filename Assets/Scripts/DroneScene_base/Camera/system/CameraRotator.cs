using System;
using UnityEngine;

[Serializable]
public class CameraRotator
{
    [SerializeField] private float rotateSpeed;

    public void SetCameraRotation(Transform _camera, Transform droneTransform)
    {
        Vector3 direction = droneTransform.position - _camera.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
}
