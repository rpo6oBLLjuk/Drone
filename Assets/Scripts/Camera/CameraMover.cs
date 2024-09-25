using CustomInspector;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [HorizontalLine("Components")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform DroneTransform;

    [HorizontalLine("Parameters")]
    [SerializeField] private Vector3 baseOffset;
    [SerializeField] private float offsetDistance;


    private void Update()
    {
        SetCameraPosition(offsetDistance);
    }

    private void SetCameraPosition(float zOffset)
    {
        float droneY = DroneTransform.rotation.eulerAngles.y;
        float offsetX = Mathf.Sin(droneY * Mathf.Deg2Rad) * offsetDistance;
        float offsetZ = Mathf.Cos(droneY * Mathf.Deg2Rad) * offsetDistance;

        Debug.Log(new Vector3(offsetZ, baseOffset.y, offsetZ));

        _camera.position = DroneTransform.position + new Vector3(offsetX, baseOffset.y, offsetZ);
    }
}
