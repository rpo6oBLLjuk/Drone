using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UISpeedWidget
{
    [SerializeField] private Image image;

    public void SetCurrentSpeed(float speed, float maxSpeed)
    {
        image.fillAmount = speed / maxSpeed;
    }
}
