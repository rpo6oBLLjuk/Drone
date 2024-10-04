using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIWidgetsController
{
    public UISpeedWidget speedWidget = new();
    public LevelTimeRecorderWidget levelTimeRecorderWidget = new();
    public UIOptions uiOptions = new();

    private List<IUIWidget> widgets = new();

    private DroneInput droneInput;

    public void Start(DroneInput input)
    {
        droneInput = input;

        widgets.Add(speedWidget);
        (speedWidget as IUIWidget).ShowWidget();

        widgets.Add(levelTimeRecorderWidget);
        (levelTimeRecorderWidget as IUIWidget).ShowWidget();

        widgets.Add(uiOptions);
        (uiOptions as IUIWidget).HideWidget();
        uiOptions.Start();
    }

    public void OptionsButton()
    {
        if (uiOptions.Widget.alpha == 1)
        {
            HideOneWidget<UIOptions>(() => Time.timeScale = 1);
            droneInput.UI.Disable();
            droneInput.Drone.Enable();
        }
        else
        {
            Time.timeScale = 0;
            ShowOneWidget<UIOptions>(() => Time.timeScale = 0);
            droneInput.UI.Enable();
            droneInput.Drone.Disable();
        }
    }

    private void ShowOneWidget<T>(Action action = null)
    {
        foreach (var widget in widgets)
        {
            if (widget is T)
                widget.ShowWidget(action);
            else
                widget.HideWidget();
        }
    }

    //До добавления более чем двух групп виджетов
    private void HideOneWidget<T>(Action action = null)
    {
        foreach (var widget in widgets)
        {
            if (widget is T)
                widget.HideWidget(action);
            else
                widget.ShowWidget();
        }
    }
}
