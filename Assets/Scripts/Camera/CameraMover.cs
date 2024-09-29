using CustomInspector;
using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class CameraMover
{
    [HorizontalLine("Parameters")]
    [SerializeField] private Vector3 baseOffset;
    [SerializeField] private float offsetDistance;
    [SerializeField] private float moveDuration;


    public void SetCameraPosition(Transform _camera, Transform droneTransform)
    {
        float droneY = droneTransform.rotation.eulerAngles.y;   
        float offsetX = Mathf.Sin(droneY * Mathf.Deg2Rad) * offsetDistance;
        float offsetZ = Mathf.Cos(droneY * Mathf.Deg2Rad) * offsetDistance;

        Debug.Log(new Vector3(offsetZ, baseOffset.y, offsetZ));

        _camera.position = Vector3.Lerp(_camera.position, droneTransform.position + new Vector3(offsetX, baseOffset.y, offsetZ), moveDuration);
    }
}
