using CustomInspector;
using System;
using UnityEngine;

[Serializable]
public class CameraMover
{
    [HorizontalLine("Parameters")]
    [SerializeField] private Vector3 baseOffset;
    [SerializeField] private float offsetDistance;
    [SerializeField] private float moveSpeed;

    public bool canMove = true;


    public void SetCameraPosition(Transform _camera, Transform droneTransform)
    {
        if (!canMove)
            return;

        float droneY = droneTransform.rotation.eulerAngles.y;
        float offsetX = Mathf.Sin(droneY * Mathf.Deg2Rad) * offsetDistance;
        float offsetZ = Mathf.Cos(droneY * Mathf.Deg2Rad) * offsetDistance;

        _camera.position = Vector3.Slerp(_camera.position, droneTransform.position + new Vector3(offsetX, baseOffset.y, offsetZ), moveSpeed * Time.deltaTime);
    }
}
