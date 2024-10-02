using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CheckpointService : MonoBehaviour, IDroneInputUser
{
    [Inject] public readonly AudioService audioService;
    [Inject] public readonly UIService uiService;
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
        pointsSwitcher.PointGetted();
        levelTimeRecorder.PointGetted();
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
