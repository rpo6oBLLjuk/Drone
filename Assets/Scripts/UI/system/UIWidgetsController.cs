using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

[Serializable]
public class UIWidgetsController
{
    public UISpeedWidget speedWidget = new();
    public LevelTimeRecorderWidget levelTimeRecorderWidget = new();
    public UIOptionsController optionsController = new();
    public UIPauseWidget pauseWidget = new();
    public FPSWidget FPSWidget = new();

    private List<(IUIWidget[], bool)> widgetSequence = new();

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

        FPSWidget.Update();
    }

    private void ShowWidgetGroup((IUIWidget[], bool) showedWidgets)
    {
        widgetSequence.Add(showedWidgets);

        foreach (IUIWidget widget in showedWidgets.Item1)
        {
            widget.ShowWidget();
        }
    }

    private void HideWidgetGroup((IUIWidget[], bool) hidedWidgets)
    {
        if (hidedWidgets.Item2)
        {
            foreach (IUIWidget widget in hidedWidgets.Item1)
            {
                widget.HideWidget();
            }

            widgetSequence.Remove(hidedWidgets);
        }
    }


    private void AddWidgetsToPool()
    {
        speedWidget.Widget.alpha = 0;

        levelTimeRecorderWidget.Widget.alpha = 0;

        FPSWidget.Widget.alpha = 0;


        ShowWidgetGroup((new IUIWidget[] { speedWidget, levelTimeRecorderWidget, FPSWidget }, false));

        optionsController.Widget.alpha = 0;
        optionsController.droneInput = droneInput;
    }

    private void AddListeners()
    {
        droneInput.UI.Quit.started += (InputAction.CallbackContext context) => HideWidgetGroup(widgetSequence.Last());

        droneInput.UI.Options.started += (InputAction.CallbackContext context) =>
        {
            if (optionsController.Widget.alpha != 1)
                ShowWidgetGroup((new IUIWidget[] { optionsController }, true));
        };

        droneInput.UI.VerticalMove.started += (InputAction.CallbackContext context) => optionsController.CalculateChangeContentIndexTime(context.ReadValue<float>(), true);
        droneInput.UI.SettingMenuMove.started += (InputAction.CallbackContext context) => optionsController.CalculateChangeTabIndexTime(context.ReadValue<float>(), true);
    }
}
