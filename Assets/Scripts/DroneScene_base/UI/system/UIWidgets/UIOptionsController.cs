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
        if (canBeShow)
        {
            base.ShowWidget();

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

    protected override void QuitComplete()
    {
        base.QuitComplete();

        uiService.ShowPopup("Quit?", () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        , PopupType.okCancel);
    }
}
