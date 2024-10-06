using UnityEngine;

public class UIService : MonoBehaviour, IDroneInputUser
{
    [SerializeField] private UIWidgetsController uiWidgetsController = new();

    public DroneInput DroneInput { get; set; }

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
}
