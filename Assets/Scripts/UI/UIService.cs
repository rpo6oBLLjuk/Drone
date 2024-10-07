using UnityEngine;

public class UIService : MonoBehaviour, IDroneInputUser
{
    [SerializeField] private UIWidgetsController uiWidgetsController = new();

    public DroneInput DroneInput { get; set; }

    private void OnEnable()
    {
        GameStateController.GameStart += Start;
        GameStateController.GameEnd += GameEnd;

        Debug.Log("Listeners Added");
    }

    private void OnDisable()
    {
        GameStateController.GameStart -= Start;
        GameStateController.GameEnd -= GameEnd;
    }

    private void Start()
    {
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

    private void GameEnd(bool value)
    {
        uiWidgetsController.optionsController.canBeShow = false;
        uiWidgetsController.ShowWidgetGroup((new IUIWidget[] {uiWidgetsController.gameEndWidget}, false));

        Debug.Log("Game ended");
    }
}
