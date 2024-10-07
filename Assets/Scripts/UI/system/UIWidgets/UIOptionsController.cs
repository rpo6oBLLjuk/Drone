using System;
using UnityEngine;

[Serializable]
public class UIOptionsController : IUIContentWidget
{
    public DroneInput droneInput;//не лучшая практика

    public override void ShowWidget()
    {
        base.ShowWidget();

        if (canBeShow)
        {
            droneInput.Drone.Disable();

            Time.timeScale = 0;
        }
    }

    public override void HideWidget()
    {
        base.HideWidget();

        droneInput.Drone.Enable();

        Time.timeScale = 1;
    }
}
