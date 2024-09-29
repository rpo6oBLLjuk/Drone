using CustomInspector;
using UnityEngine;

public class CameraService : MonoBehaviour
{
    public Camera Camera
    {
        get => _camera;
    }
    [SerializeField, ForceFill] private Camera _camera;
    [SerializeField] private Transform droneTransform;

    [SerializeField] private CameraFovController _cameraFovController = new();
    [SerializeField] private CameraMover _cameraMover = new();
    [SerializeField] private CameraRotator _cameraRotator = new();

    public void SetCameraFov(float speed, float maxSpeed)
    {
        _cameraFovController.SetFov(_camera, speed, maxSpeed);
    }

    public void LateUpdate()
    {
        _cameraMover.SetCameraPosition(Camera.transform, droneTransform);
        _cameraRotator.SetCameraRotation(Camera.transform, droneTransform);
    }
}
