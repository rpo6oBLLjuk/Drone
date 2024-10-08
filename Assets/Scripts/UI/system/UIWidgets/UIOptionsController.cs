using System;
using UnityEngine;

[Serializable]
public class UIOptionsController : IUIContentWidget
{
    public override void Start()
    {
        base.Start();
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    public override void Update()
    {
        base.Update();


    }


    public override void ShowWidget()
    {
        base.ShowWidget();

        if (canBeShow)
        {
            uiService.DroneInput.Drone.Disable();

            Time.timeScale = 0;
        }
    }

    public override void HideWidget()
    {
        base.HideWidget();

        uiService.DroneInput.Drone.Enable();

        Time.timeScale = 1;
    }

    public override void Options()
    {
        if (Widget.alpha == 0)
            uiService.uiWidgetsController.ShowWidgetGroup((new IUIWidget[] { this }, true));
    }

    public override void QuitStart()
    {
        base.QuitStart();


    }
}
