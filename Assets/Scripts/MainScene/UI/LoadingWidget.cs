using CustomInspector;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWidget : MonoBehaviour
{
    [SerializeField, SelfFill(true)] private Image loadingImage;
    [SerializeField, Unit("* 360 deg/s")] private float rotateSpeed;
    [SerializeField, FixedValues(-1, 1)] private int rotateMultiplier;

    private Tween tween;

    private void Start()
    {
        DisableWidget();
    }

    public void EnableWidget()
    {
        loadingImage.enabled = true;

        loadingImage.transform.localRotation = Quaternion.identity;

        tween = loadingImage.transform.DORotate(new Vector3(0, 0, 360) * rotateMultiplier, 1 / rotateSpeed, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    public void DisableWidget()
    {
        loadingImage.enabled = false;
        tween.Kill();
    }
}
