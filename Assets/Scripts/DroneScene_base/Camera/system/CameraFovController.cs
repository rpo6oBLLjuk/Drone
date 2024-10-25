using CustomInspector;
using System;
using UnityEngine;

[Serializable]
public class CameraFovController
{
    [SerializeField, AsRange(50, 100)] private Vector2 fov;
    public void SetFov(Camera camera, float speed, float maxSpeed)
    {
        camera.fieldOfView = Mathf.Lerp(fov.x, fov.y, speed / maxSpeed);
    }
}
