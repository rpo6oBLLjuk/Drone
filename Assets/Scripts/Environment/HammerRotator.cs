using CustomInspector;
using DG.Tweening;
using UnityEngine;

public class HammerRotator : MonoBehaviour
{
    [SerializeField, SelfFill(true)] private Rigidbody rb;
    [SerializeField] private Vector3 rotationStart;
    [SerializeField] private Vector3 rotationEnd;
    [SerializeField] private float duration;

    private Sequence moveSequence;

    private void Start()
    {
        rb.rotation = Quaternion.Euler(rotationStart);

        moveSequence = DOTween.Sequence();

        moveSequence.Insert(0, rb.DORotate(rotationEnd, duration / 2)
            .SetEase(Ease.InOutSine));

        moveSequence.Insert(1, rb.DORotate(rotationStart, duration / 2).
                SetEase(Ease.InOutSine));

        moveSequence.SetLoops(-1);
    }

    private void OnDisable()
    {
        moveSequence.Kill();
    }
}
