using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[Serializable]
public class UIWidgetsController : IDisposable
{
    [Inject] readonly DiContainer diContainer;

    public SpeedWidget speedWidget;
    public LevelTimeRecorderWidget levelTimeRecorderWidget;
    public UIOptionsController optionsController;
    public PauseWidget pauseWidget;
    public FPSWidget FPSWidget;
    public GameEndWidget gameEndWidget;


    private List<(IUIWidget[], bool)> widgetSequence = new();

    private DroneInput droneInput;

    private List<IUIWidget> widgets = new();


    public void Start(DroneInput input)
    {
        droneInput = input;

        AddListeners();

        AddWidgetsToPool();
        WidgetsStart();

        ShowWidgetGroup((new IUIWidget[] { speedWidget, levelTimeRecorderWidget, FPSWidget }, false));
    }

    public void Update()
    {
        if (droneInput == null)
        {
            Debug.LogError("Drone Input не получен");
            return;
        }

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


    public void ShowWidgetGroup((IUIWidget[], bool) showedWidgets)
    {
        widgetSequence.Add(showedWidgets);

        foreach (IUIWidget widget in showedWidgets.Item1)
        {
            widget.ShowWidget();
        }
    }

    public void HideWidgetGroup((IUIWidget[], bool) hidedWidgets)
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


    private void AddListeners()
    {
        droneInput.UI.Close.started += HideActiveWidget;
        droneInput.UI.Apply.started += ApllyWidget;

        droneInput.UI.VerticalMove.started += MoveWidgetVertical;
        droneInput.UI.SettingMenuMove.started += MoveWidgetSettingMenu;

        droneInput.UI.Options.started += OptionsStarted;
    }


    private void AddWidgetsToPool()
    {
        var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            if (typeof(IUIWidget).IsAssignableFrom(field.FieldType))
            {
                if (field.GetValue(this) is IUIWidget widget)
                {
                    widgets.Add(widget);
                }
            }
        }
    }

    private void WidgetsStart()
    {
        Debug.Log($"Widgets count: {widgets.Count}");
        foreach (IUIWidget widget in widgets)
        {
            widget.InjectDependencies(diContainer);
            widget.Start();
            widget.Widget.alpha = 0;
        }
    }


    private void HideActiveWidget(InputAction.CallbackContext context)
    {
        HideWidgetGroup(widgetSequence.Last());
    }

    private void ApllyWidget(InputAction.CallbackContext context)
    {
        widgetSequence.Last()
            .Item1
            .Last()
            .Apply();
    }

    private void MoveWidgetVertical(InputAction.CallbackContext context)
    {
        if(widgetSequence.Last().Item1.Last() is IUIContentWidget contentWidget)
        {
            contentWidget.CalculateChangeContentIndexTime(context.ReadValue<float>(), true);
        } 
    }

    private void MoveWidgetSettingMenu(InputAction.CallbackContext context)
    {
        if (widgetSequence.Last().Item1.Last() is IUITabWidget contentWidget)
        {
            contentWidget.CalculateChangeTabIndexTime(context.ReadValue<float>(), true);
        }
    }

    private void OptionsStarted(InputAction.CallbackContext context)
    {
        optionsController.Options();
    }

    public void Dispose()
    {
        droneInput.UI.Close.started -= HideActiveWidget;
        droneInput.UI.Apply.started -= ApllyWidget;

        droneInput.UI.VerticalMove.started -= MoveWidgetVertical;
        droneInput.UI.SettingMenuMove.started -= MoveWidgetSettingMenu;

        droneInput.UI.Options.started -= OptionsStarted;

        Debug.Log("Listeners Dispoced");

        foreach (IUIWidget widget in widgets)
            widget.Dispose();
    }
}
