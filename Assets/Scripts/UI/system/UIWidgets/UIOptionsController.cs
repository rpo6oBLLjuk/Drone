using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class UIOptionsController : IUIContentWidget
{
    public override void Start()
    {
        base.Start();

        uiService.DroneInput.UI.Options.started += OptionsPressed;

    }

    public override void Dispose()
    {
        base.Dispose();


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

    private void OptionsPressed(InputAction.CallbackContext context)
    {
        if (Widget.alpha != 0)
            uiService.uiWidgetsController.ShowWidgetGroup((new IUIWidget[] { this }, true));
    }
}
