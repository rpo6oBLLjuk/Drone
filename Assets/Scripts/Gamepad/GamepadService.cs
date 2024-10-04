using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadService : MonoBehaviour
{
    [SerializeField] private DroneInput droneInput;
    public Gamepad Gamepad
    {
        get => Gamepad.current;
    }

    [SerializeField] private GamepadVibrationController gamepadVibrationController = new();

    private void Awake()
    {
        droneInput = new();
        droneInput.Enable();

        gamepadVibrationController.gamepadService = this;

        FindDroneInputUsers();
    }

    public void SetVibration() => gamepadVibrationController.SetVibration();

    public void SetVibration(float strength, float time) => gamepadVibrationController.SetVibration(strength, time);

    private void FindDroneInputUsers()
    {
        foreach (IDroneInputUser user in FindObjectsOfType<MonoBehaviour>().Where((thisClass) => thisClass is IDroneInputUser).Cast<IDroneInputUser>())
        {
            user.DroneInput = droneInput;
        }
    }
}
