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
    public FPSWidget FPSWidget;
    public LevelTimeRecorderWidget levelTimeRecorderWidget;
    public UIOptionsController optionsController;
    public PauseWidget pauseWidget;
    public PopupWidget popupWidget;
    public GameEndWidget gameEndWidget;

    /// <summary>
    /// Последовательность открытых виджетов, bool - может ли группа виджетов быть закрыта через Close
    /// </summary>
    public List<(IUIWidget, bool)> widgetSequence = new();

    private DroneInput droneInput;

    private List<IUIWidget> widgets = new();


    public void Start(DroneInput input)
    {
        droneInput = input;

        AddListeners();

        AddWidgetsToPool();
        WidgetsStart();

        ShowWidget((speedWidget, false));
        ShowWidget((levelTimeRecorderWidget, false));
        ShowWidget((FPSWidget, false));
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


    public void ShowWidget((IUIWidget, bool) showedWidget)
    {
        if (showedWidget.Item1.canBeShow)
        {
            showedWidget.Item1.ShowWidget();

            widgetSequence.Add(showedWidget);
        }
    }

    public void HideWidget((IUIWidget, bool) hidedWidget)
    {
        if (hidedWidget.Item2)
        {
            hidedWidget.Item1.HideWidget();

            widgetSequence.Remove(hidedWidget);
        }
    }

    public void GameEnd(bool value)
    {
        optionsController.canBeShow = false;

        gameEndWidget.SetLevelState(value);
        ShowWidget((gameEndWidget, false));
    }

    #region Start methods
    /// <summary>
    /// Метод для сохранения всех возможных виджетов в список.
    /// Основан на рефлексии, что позволяет получить в него все возможные поля данного класса, являющиеся IUIWidget без ручного заполнения
    /// </summary>
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

    /// <summary>
    /// Метод для старта виджетов, прокидывает зависимости, отключает виджеты, активирует их Start-метод
    /// </summary>
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
    #endregion

    #region Listenets
    private void Aplly(InputAction.CallbackContext context)
    {
        widgetSequence.Last()
            .Item1
            .Apply();
    }

    public void Close(InputAction.CallbackContext context)
    {
        (IUIWidget, bool) widget = widgetSequence.Last();
        if (widget.Item2)
            HideWidget(widget);
        else
            widget.Item1.Cancel();
    }

    private void MoveWidgetVertical(InputAction.CallbackContext context)
    {
        if (widgetSequence.Last().Item1 is IUIContentWidget contentWidget)
        {
            contentWidget.CalculateChangeContentIndexTime(context.ReadValue<float>(), true);
        }
    }

    private void MoveWidgetSettingMenu(InputAction.CallbackContext context)
    {
        if (widgetSequence.Last().Item1 is IUITabWidget contentWidget)
        {
            contentWidget.CalculateChangeTabIndexTime(context.ReadValue<float>(), true);
        }
    }

    private void OptionsStarted(InputAction.CallbackContext context)
    {
        optionsController.Options(); //Реализация неверна, в норме принимать данный параметр должен текущий виджет. Однако, ввиду того что текущим виджетом может быть что угодно, настройки нечему открыть. Будет исправлено по добавлении PauseMenu
    }

    private void QuitStarted(InputAction.CallbackContext context)
    {
        widgetSequence.Last()
            .Item1
            .QuitStart();
    }

    private void QuitPaused(InputAction.CallbackContext context)
    {
        widgetSequence.Last()
            .Item1
            .QuitPaused();
    }
    #endregion

    private void AddListeners()
    {
        droneInput.UI.Close.started += Close;
        droneInput.UI.Apply.started += Aplly;

        droneInput.UI.VerticalMove.started += MoveWidgetVertical;
        droneInput.UI.SettingMenuMove.started += MoveWidgetSettingMenu;

        droneInput.UI.Options.started += OptionsStarted;

        droneInput.UI.Quit.started += QuitStarted;
        droneInput.UI.Quit.canceled += QuitPaused;
    }

    public void Dispose()
    {
        droneInput.UI.Close.started -= Close;
        droneInput.UI.Apply.started -= Aplly;

        droneInput.UI.VerticalMove.started -= MoveWidgetVertical;
        droneInput.UI.SettingMenuMove.started -= MoveWidgetSettingMenu;

        droneInput.UI.Options.started -= OptionsStarted;

        droneInput.UI.Quit.started -= QuitStarted;
        droneInput.UI.Quit.canceled -= QuitPaused;

        Debug.Log("Listeners Dispoced");

        foreach (IUIWidget widget in widgets)
            widget.Dispose();
    }
}
