using UnityEngine;
using UnityEngine.InputSystem;

public class UIService : MonoBehaviour, IDroneInputUser
{
    [SerializeField] private UIWidgetsController uiWidgetsController;

    public DroneInput DroneInput { get; set; }

    private void Start()
    {
        uiWidgetsController.Start(DroneInput);

        DroneInput.UI.Options.started += (InputAction.CallbackContext context) => uiWidgetsController.OptionsButton();

        DroneInput.UI.MoveRightSettingMenu.started += (InputAction.CallbackContext context) => uiWidgetsController.uiOptions.ChangeActiveMenu(1);
        DroneInput.UI.MoveLeftSettingMenu.started += (InputAction.CallbackContext context) => uiWidgetsController.uiOptions.ChangeActiveMenu(-1);

    }

    void Update()
    {
        if (DroneInput.UI.Move.IsPressed())
        {
            uiWidgetsController.uiOptions.MoveScrollRect(DroneInput.UI.Move.ReadValue<Vector2>());
        }
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
