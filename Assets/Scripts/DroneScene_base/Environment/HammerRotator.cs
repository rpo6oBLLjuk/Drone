using CustomInspector;
using DG.Tweening;
using UnityEngine;

public class HammerRotator : MonoBehaviour
{
    [SerializeField, SelfFill(true)] private Rigidbody rb;
    [SerializeField] private Vector3 rotationStart;
    [SerializeField] private Vector3 rotationEnd;
    [SerializeField] private float duration;

    private Sequence moveTween;

    private void Start()
    {
        moveTween = DOTween.Sequence();

        moveTween.Join(rb.DORotate(rotationEnd, duration)
            .SetEase(Ease.InOutSine));

        moveTween.Append(rb.DORotate(rotationStart, duration)
            .SetEase(Ease.InOutSine));

        moveTween.SetLoops(-1);
    }


    private void OnDisable()
    {
        moveTween.Kill();
    }
}
