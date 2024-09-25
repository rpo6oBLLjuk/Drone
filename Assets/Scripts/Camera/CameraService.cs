using Cinemachine;
using CustomInspector;
using UnityEngine;

public class CameraService : MonoBehaviour
{
    public Camera Camera
    {
        get => _camera;
        set => _camera = value;
    }
    [SerializeField, ForceFill] private Camera _camera;

    [SerializeField, AsRange(50, 100)] private Vector2 fov;

    private CameraFovController _cameraFovController = new();

    public void SetCameraFov(float speed, float maxSpeed)
    {
        _cameraFovController.SetFov(_camera, speed, maxSpeed, fov.x, fov.y);
    }
}
