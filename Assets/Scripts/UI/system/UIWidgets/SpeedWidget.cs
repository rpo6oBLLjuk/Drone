using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SpeedWidget : IUIWidget
{
    [SerializeField] private Image image;

    public void SetCurrentSpeed(float speed, float maxSpeed)
    {
        image.fillAmount = speed / maxSpeed;
    }
}
