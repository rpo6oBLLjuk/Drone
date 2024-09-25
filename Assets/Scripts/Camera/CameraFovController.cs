using UnityEngine;

public class CameraFovController
{
    public void SetFov(Camera camera, float speed, float maxSpeed, float minFov, float maxFov)
    {
        camera.fieldOfView = Mathf.Lerp(minFov, maxFov, speed / maxSpeed);
    }
}
