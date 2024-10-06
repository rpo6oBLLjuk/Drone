using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class UIWidgetsController
{
    public UISpeedWidget speedWidget = new();
    public LevelTimeRecorderWidget levelTimeRecorderWidget = new();
    public UIOptionsController optionsController = new();
    public UIPauseWidget pauseWidget = new();

    private List<IUIWidget> widgets = new();

    private List<IUIWidget[]> widgetSequence = new();

    private DroneInput droneInput;

    public void Start(DroneInput input)
    {
        droneInput = input;

        AddWidgetsToPool();
        AddListeners();

        optionsController.Start();
    }

    public void Update()
    {
        if (droneInput.UI.VerticalMove.IsPressed())
        {
            optionsController.ContentMovement(droneInput.UI.VerticalMove.ReadValue<float>());
        }

        if (droneInput.UI.SettingMenuMove.IsPressed())
        {
            optionsController.TabMovement(droneInput.UI.SettingMenuMove.ReadValue<float>());
        }
    }

    public void OptionsButton()
    {
        if (optionsController.Widget.alpha == 1)
        {
            HideWidgetGroup(new IUIWidget[] { levelTimeRecorderWidget, optionsController });

            droneInput.UI.Disable();
            droneInput.Drone.Enable();
        }
        else
        {
            ShowWidgetGroup(new IUIWidget[] { optionsController });

            droneInput.UI.Enable();
            droneInput.Drone.Disable();
        }
    }

    private void ShowWidgetGroup(IUIWidget[] showedWidgets)
    {
        widgetSequence.Add(showedWidgets);

        foreach (IUIWidget widget in showedWidgets)
        {
            widget.ShowWidget();
        }
    }

    private void HideWidgetGroup(IUIWidget[] hidedWidgets)
    {
        foreach (IUIWidget widget in hidedWidgets)
        {
            widget.HideWidget();
        }

        widgetSequence.Remove(hidedWidgets);
    }


    private void AddWidgetsToPool()
    {
        widgets.Add(speedWidget);
        speedWidget.Widget.alpha = 0;

        widgets.Add(levelTimeRecorderWidget);
        levelTimeRecorderWidget.Widget.alpha = 0;

        ShowWidgetGroup(new IUIWidget[] { speedWidget, levelTimeRecorderWidget });

        widgets.Add(optionsController);
        optionsController.Widget.alpha = 0;
    }

    private void AddListeners()
    {
        droneInput.UI.Quit.started += (InputAction.CallbackContext context) => HideWidgetGroup(widgetSequence.Last());

        droneInput.UI.Options.started += (InputAction.CallbackContext context) => OptionsButton();
    }
}
