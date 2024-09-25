using DG.Tweening;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform DroneTransform;
    [SerializeField] private float rotateSpeed;

    private void LateUpdate()
    {
        Vector3 direction = DroneTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        _camera.transform.DORotateQuaternion(rotation, rotateSpeed).SetEase(Ease.InOutSine);

    }
}
