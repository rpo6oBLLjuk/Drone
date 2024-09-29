using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class CameraRotator
{
    [SerializeField] private float rotateDuration;

    public void SetCameraRotation(Transform _camera, Transform droneTransform)
    {
        Vector3 direction = droneTransform.position - _camera.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        _camera.transform.DORotateQuaternion(rotation, rotateDuration).SetEase(Ease.InOutSine);
    }
}
