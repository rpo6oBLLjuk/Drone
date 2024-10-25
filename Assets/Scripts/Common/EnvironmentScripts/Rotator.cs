using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationTime = 1;

    private Tween rotationTween;

    private void OnEnable()
    {
        StartRotating();
    }

    private void OnDisable()
    {
        StopRotating();
    }

    private void StartRotating()
    {
        rotationTween = transform.DORotate(rotationAxis * 360f, rotationTime, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    private void StopRotating()
    {
        if (rotationTween != null)
        {
            rotationTween.Kill();
        }
    }
}
