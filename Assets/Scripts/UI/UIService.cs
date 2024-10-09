using System;
using UnityEngine;
using Zenject;

public class UIService : MonoBehaviour, IDroneInputUser
{
    [Inject] readonly DiContainer diContainer;
    [SerializeField] public UIWidgetsController uiWidgetsController;

    public DroneInput DroneInput { get; set; }

    private void OnEnable()
    {
        GameStateController.GameStart += GameStart;
        GameStateController.GameEnd += GameEnd;
    }

    private void OnDestroy()
    {
        GameStateController.GameStart -= GameStart;
        GameStateController.GameEnd -= GameEnd;

        uiWidgetsController.Dispose();
    }

    private void GameStart()
    {
        diContainer.Inject(uiWidgetsController);
        uiWidgetsController.Start(DroneInput);
    }

    void Update()
    {
        uiWidgetsController.Update();
    }

    public void SetCurrentSpeed(float speed, float maxSpeed)
    {
        uiWidgetsController.speedWidget.SetCurrentSpeed(speed, maxSpeed);
    }

    public void AddTime(float time, int checkpointNumber)
    {
        uiWidgetsController.levelTimeRecorderWidget.AddTime(time, checkpointNumber);
    }

    public void ShowPopup(string popupText, Action action, PopupType type)
    {
        uiWidgetsController.popupWidget.ShowWidget(popupText, action, type);
    }

    private void GameEnd(bool value)
    {
        //Bad
        uiWidgetsController.optionsController.canBeShow = false;

        uiWidgetsController.gameEndWidget.SetLevelState(value);
        uiWidgetsController.ShowWidgetGroup((new IUIWidget[] {uiWidgetsController.gameEndWidget}, false));
    }
}
