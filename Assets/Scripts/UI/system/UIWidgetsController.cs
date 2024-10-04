using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[Serializable]
public class UIWidgetsController
{
    public UISpeedWidget speedWidget = new();
    public LevelTimeRecorderWidget levelTimeRecorderWidget = new();
    public UIOptionsController uiOptionsController = new();

    private List<IUIWidget> widgets = new();

    private List<IUIWidget[]> widgetSequence;

    private DroneInput droneInput;

    public void Start(DroneInput input)
    {
        droneInput = input;

        AddWidgetsToPool();
        AddListeners();

        uiOptionsController.Start();
    }

    public void OptionsButton()
    {
        if (uiOptionsController.Widget.alpha == 1)
        {
            HideWidgetGroup(new IUIWidget[] { levelTimeRecorderWidget, uiOptionsController });

            droneInput.UI.Disable();
            droneInput.Drone.Enable();
        }
        else
        {
            Time.timeScale = 0;

            ShowWidgetGroup(new IUIWidget[] { uiOptionsController });

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
        (speedWidget as IUIWidget).Widget.alpha = 0;

        widgets.Add(levelTimeRecorderWidget);
        (levelTimeRecorderWidget as IUIWidget).Widget.alpha = 0;

        widgets.Add(uiOptionsController);
        (uiOptionsController as IUIWidget).Widget.alpha = 0;
    }

    private void AddListeners()
    {
        droneInput.UI.Quit.started += (InputAction.CallbackContext context) => HideWidgetGroup(widgetSequence.Last());

        droneInput.UI.Options.started += (InputAction.CallbackContext context) => OptionsButton();

        droneInput.UI.SettingMenuMove.started += (InputAction.CallbackContext context) => uiOptionsController.ChangeActiveMenu(context.ReadValue<float>());
    }
}
