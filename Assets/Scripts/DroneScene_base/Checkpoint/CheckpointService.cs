using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CheckpointService : MonoBehaviour, IDroneInputUser
{
    [Inject] public readonly AudioService audioService;
    [Inject] public readonly UIService uiService;
    [Inject] public readonly GamepadService gamepadService;

    public Action<float> CheckpointsComplete;

    public DroneInput DroneInput { get; set; }

    [SerializeField] private PointsSwitcher pointsSwitcher = new();
    [SerializeField] private LevelTimeRecorder levelTimeRecorder = new();

    private bool inputStarted = false;

    private void Start()
    {
        pointsSwitcher.Start(this);
        DroneInput.Drone.Throttle.started += InputActivateHandler;
    }

    public void PointGetted()
    {
        if (GameStateController.GameEnded)
            return;

        pointsSwitcher.PointGetted();
        levelTimeRecorder.PointGetted();

        gamepadService.SetVibration();
    }

    private void InputActivateHandler(InputAction.CallbackContext context)
    {
        if (!inputStarted)
        {
            inputStarted = true;
            levelTimeRecorder.Start(this);
        }
    }

    private void OnDisable()
    {
        DroneInput.Drone.Throttle.started -= InputActivateHandler;
    }
}
