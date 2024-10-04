using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UISpeedWidget : IUIWidget
{
    public CanvasGroup Widget
    {
        get => widget;
        set => widget = value;
    }
    [SerializeField] private CanvasGroup widget;

    [SerializeField] private Image image;


    public void SetCurrentSpeed(float speed, float maxSpeed)
    {
        image.fillAmount = speed / maxSpeed;
    }
}
